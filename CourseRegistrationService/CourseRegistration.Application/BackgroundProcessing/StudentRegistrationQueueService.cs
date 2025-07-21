using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CourseRegistration.Application.BackgroundProcessing
{
    public class StudentRegistrationQueueService : BackgroundService
    {
        private readonly ConcurrentQueue<RegistrationQueueItem> _queue;
        private readonly SemaphoreSlim _signal;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<StudentRegistrationQueueService> _logger;
        private readonly int _maxConcurrentProcessing = 10; // Handle up to 10 registrations simultaneously

        public StudentRegistrationQueueService(
            IServiceProvider serviceProvider,
            ILogger<StudentRegistrationQueueService> logger)
        {
            _queue = new ConcurrentQueue<RegistrationQueueItem>();
            _signal = new SemaphoreSlim(0);
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public void Enqueue(RegistrationQueueItem queueItem)
        {
            if (queueItem?.RequestDto == null || queueItem.ResponseTcs == null)
            {
                throw new ArgumentException("Queue item must have RequestDto and ResponseTcs");
            }

            _queue.Enqueue(queueItem);
            _signal.Release();
            
            _logger.LogInformation($"Enqueued registration for student {queueItem.RequestDto.StudentId}. Queue size: {_queue.Count}");
        }

        public int GetQueueCount() => _queue.Count;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("StudentRegistrationQueueService started");

            var tasks = new List<Task>();

            // Start multiple processing tasks for concurrent processing
            for (int i = 0; i < _maxConcurrentProcessing; i++)
            {
                tasks.Add(ProcessQueueAsync(stoppingToken));
            }

            await Task.WhenAll(tasks);
        }

        private async Task ProcessQueueAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await _signal.WaitAsync(cancellationToken);

                    if (_queue.TryDequeue(out var queueItem))
                    {
                        _logger.LogInformation($"Processing registration for student {queueItem.RequestDto.StudentId}");
                        
                        await ProcessRegistrationAsync(queueItem);
                    }
                }
                catch (OperationCanceledException)
                {
                    // Expected when cancellation is requested
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in queue processing loop");
                }
            }
        }

        private async Task ProcessRegistrationAsync(RegistrationQueueItem queueItem)
        {
            try
            {
                // Create a scope for dependency injection
                using var scope = _serviceProvider.CreateScope();
                var registrationService = scope.ServiceProvider.GetRequiredService<IStudentRegistrationService>();

                // Process the registration using the existing service
                var result = await registrationService.RegisterStudentAsync(queueItem.RequestDto);

                // Set the result back to the TaskCompletionSource
                queueItem.ResponseTcs.SetResult(result);

                _logger.LogInformation($"Successfully processed registration for student {queueItem.RequestDto.StudentId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to process registration for student {queueItem.RequestDto.StudentId}");
                
                // Create error response
                var errorResponse = new StudentRegistrationResponseDto
                {
                    StudentId = queueItem.RequestDto.StudentId,
                    ClassId = queueItem.RequestDto.ClassId,
                    IsSuccess = false
                };
                errorResponse.Errors.Add($"Registration processing failed: {ex.Message}");
                
                queueItem.ResponseTcs.SetResult(errorResponse);
            }
        }

        public override void Dispose()
        {
            _signal?.Dispose();
            base.Dispose();
        }
    }
}
