using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace UserManagement.Application.Repositories
{
    public  class RedisCacheService
    {

        private readonly IDistributedCache _cache;
        public RedisCacheService( IDistributedCache cache)
        {
            _cache = cache;


        }


        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(30)
            };

            var json = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, json, options);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var json = await _cache.GetStringAsync(key);
            return json is not null ? JsonSerializer.Deserialize<T>(json) : default;
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        /// <summary>
        /// Check if a key exists in cache
        /// </summary>
        public async Task<bool> ExistsAsync(string key)
        {
            try
            {
                var value = await _cache.GetStringAsync(key);
                return value != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Remove multiple cache entries safely
        /// </summary>
        public async Task RemoveMultipleAsync(params string[] keys)
        {
            var tasks = keys.Select(key => _cache.RemoveAsync(key));
            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Set cache with error handling
        /// </summary>
        public async Task<bool> TrySetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            try
            {
                await SetAsync(key, value, expiry);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Redis TrySet] Failed to cache key '{key}': {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Get cache with error handling
        /// </summary>
        public async Task<(T value, bool success)> TryGetAsync<T>(string key)
        {
            try
            {
                var result = await GetAsync<T>(key);
                return (result, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Redis TryGet] Failed to get key '{key}': {ex.Message}");
                return (default(T), false);
            }
        }

        /// <summary>
        /// Remove cache with error handling
        /// </summary>
        public async Task<bool> TryRemoveAsync(string key)
        {
            try
            {
                await RemoveAsync(key);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Redis TryRemove] Failed to remove key '{key}': {ex.Message}");
                return false;
            }
        }
    }
}
