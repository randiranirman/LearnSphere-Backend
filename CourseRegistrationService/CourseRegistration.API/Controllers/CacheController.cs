using CourseRegistration.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseRegistration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<CacheController> _logger;

        public CacheController(ICacheService cacheService, ILogger<CacheController> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        [HttpGet("status")]
        public IActionResult GetCacheStatus()
        {
            try
            {
                var isConnected = _cacheService.IsConnected();
                return Ok(new { IsConnected = isConnected, Message = isConnected ? "Redis is connected" : "Redis is not connected" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking cache status");
                return StatusCode(500, new { Error = "Failed to check cache status", Details = ex.Message });
            }
        }

        [HttpGet("keys")]
        public async Task<IActionResult> GetAllKeys([FromQuery] string pattern = "*")
        {
            try
            {
                var keys = await _cacheService.GetAllKeysAsync(pattern);
                return Ok(new { Pattern = pattern, Keys = keys, Count = keys.Length });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cache keys with pattern: {Pattern}", pattern);
                return StatusCode(500, new { Error = "Failed to get cache keys", Details = ex.Message });
            }
        }

        [HttpPost("test")]
        public async Task<IActionResult> TestCache([FromBody] TestCacheRequest request)
        {
            try
            {
                // Test setting cache
                await _cacheService.SetAsync(request.Key, new { Data = request.Value, Timestamp = DateTime.UtcNow }, TimeSpan.FromMinutes(5));
                
                // Test getting cache
                var cachedData = await _cacheService.GetAsync<dynamic>(request.Key);
                
                return Ok(new 
                { 
                    Message = "Cache test completed",
                    SetKey = request.Key,
                    SetValue = request.Value,
                    RetrievedData = cachedData
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error testing cache");
                return StatusCode(500, new { Error = "Cache test failed", Details = ex.Message });
            }
        }

        [HttpDelete("clear/{key}")]
        public async Task<IActionResult> ClearCache(string key)
        {
            try
            {
                await _cacheService.RemoveAsync(key);
                return Ok(new { Message = $"Cache cleared for key: {key}" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing cache for key: {Key}", key);
                return StatusCode(500, new { Error = "Failed to clear cache", Details = ex.Message });
            }
        }

        [HttpDelete("clear-pattern/{pattern}")]
        public async Task<IActionResult> ClearCacheByPattern(string pattern)
        {
            try
            {
                await _cacheService.RemoveByPatternAsync(pattern);
                return Ok(new { Message = $"Cache cleared for pattern: {pattern}" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing cache for pattern: {Pattern}", pattern);
                return StatusCode(500, new { Error = "Failed to clear cache by pattern", Details = ex.Message });
            }
        }
    }

    public class TestCacheRequest
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
