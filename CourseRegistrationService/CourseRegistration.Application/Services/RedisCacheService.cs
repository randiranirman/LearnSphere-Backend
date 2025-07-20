using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CourseRegistration.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace CourseRegistration.Application.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<RedisCacheService> _logger;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly JsonSerializerOptions _jsonOptions;
        
        public RedisCacheService(
            IDistributedCache distributedCache, 
            ILogger<RedisCacheService> logger,
            IConnectionMultiplexer connectionMultiplexer)
        {
            _distributedCache = distributedCache;
            _logger = logger;
            _connectionMultiplexer = connectionMultiplexer;
            
            // Configure JSON serialization options to handle EF entities
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = false
            };
        }

        public async Task<T?> GetAsync<T>(string key) where T : class
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                _logger.LogWarning("Cache key is null or empty for GetAsync<{Type}>", typeof(T).Name);
                return null;
            }

            try
            {
                _logger.LogDebug("Getting cache entry for key: {Key}", key);
                
                var cachedData = await _distributedCache.GetStringAsync(key);
                if (string.IsNullOrEmpty(cachedData))
                {
                    _logger.LogDebug("Cache miss for key: {Key}", key);
                    return null;
                }

                var result = JsonSerializer.Deserialize<T>(cachedData, _jsonOptions);
                _logger.LogDebug("Cache hit for key: {Key}", key);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cache entry for key: {Key}", key);
                return null;
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null) where T : class
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                _logger.LogWarning("Cache key is null or empty for SetAsync<{Type}>", typeof(T).Name);
                return;
            }

            if (value == null)
            {
                _logger.LogWarning("Value is null for cache key: {Key}", key);
                return;
            }

            try
            {
                _logger.LogDebug("Setting cache entry for key: {Key}", key);
                
                var serializedData = JsonSerializer.Serialize(value, _jsonOptions);
                var options = new DistributedCacheEntryOptions();
                
                var expirationTime = expiry ?? TimeSpan.FromMinutes(30);
                options.SetAbsoluteExpiration(expirationTime);

                await _distributedCache.SetStringAsync(key, serializedData, options);
                
                _logger.LogDebug("Successfully cached entry for key: {Key} with expiration: {Expiry}", 
                    key, expirationTime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting cache entry for key: {Key}", key);
                // Don't throw to prevent cache failures from breaking the application
            }
        }

        public async Task RemoveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                _logger.LogWarning("Cache key is null or empty for RemoveAsync");
                return;
            }

            try
            {
                _logger.LogDebug("Removing cache entry for key: {Key}", key);
                await _distributedCache.RemoveAsync(key);
                _logger.LogDebug("Successfully removed cache entry for key: {Key}", key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing cache entry for key: {Key}", key);
            }
        }

        public async Task RemoveByPatternAsync(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
            {
                _logger.LogWarning("Pattern is null or empty for RemoveByPatternAsync");
                return;
            }

            try
            {
                _logger.LogDebug("Removing cache entries by pattern: {Pattern}", pattern);
                
                var database = _connectionMultiplexer.GetDatabase();
                var server = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints().First());
                
                // Use Redis pattern matching to find keys
                var keys = server.Keys(pattern: pattern).ToArray();
                
                if (keys.Length > 0)
                {
                    await database.KeyDeleteAsync(keys);
                    _logger.LogDebug("Successfully removed {Count} cache entries by pattern: {Pattern}", 
                        keys.Length, pattern);
                }
                else
                {
                    _logger.LogDebug("No cache entries found for pattern: {Pattern}", pattern);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing cache entries by pattern: {Pattern}", pattern);
            }
        }

        // Add a method to check Redis connection status
        public bool IsConnected()
        {
            try
            {
                return _connectionMultiplexer.IsConnected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking Redis connection status");
                return false;
            }
        }

        // Add a method to get all keys (for debugging)
        public async Task<string[]> GetAllKeysAsync(string pattern = "*")
        {
            try
            {
                var server = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints().First());
                var keys = server.Keys(pattern: pattern).Select(key => (string)key).ToArray();
                
                _logger.LogDebug("Found {Count} keys matching pattern: {Pattern}", keys.Length, pattern);
                return keys;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting keys with pattern: {Pattern}", pattern);
                return Array.Empty<string>();
            }
        }
    }
}
