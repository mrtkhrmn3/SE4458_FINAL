using MedicineService.DTOs;
using MedicineService.Entities;
using MedicineService.Repositories;
using StackExchange.Redis;
using System.Text.Json;

namespace MedicineService.Services
{
    public class MedicinesService : IMedicineService
    {
        private readonly IMedicineRepository _repository;
        private readonly IDatabase _redisCache;
        private const string CacheKeyPrefix = "medicines:";
        private const string CacheKeySet = "medicines:keys";

        public MedicinesService(IMedicineRepository repository, IConnectionMultiplexer redis)
        {
            _repository = repository;
            _redisCache = redis.GetDatabase();
        }

        public async Task<List<Medicine>> SearchMedicinesAsync(string query)
        {
            var cacheKey = $"{CacheKeyPrefix}{query}";
            var cachedData = await _redisCache.StringGetAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                Console.WriteLine($"Cache hit for key: {cacheKey}");
                return JsonSerializer.Deserialize<List<Medicine>>(cachedData);
            }

            Console.WriteLine($"Cache miss for key: {cacheKey}");
            var medicines = (await _repository.GetMedicinesAsync())
                .Where(m => m.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (medicines.Any())
            {
                await _redisCache.StringSetAsync(cacheKey, JsonSerializer.Serialize(medicines), TimeSpan.FromMinutes(30));
                await _redisCache.SetAddAsync(CacheKeySet, cacheKey); 
            }

            return medicines;
        }

        public async Task UpdateMedicinesAsync(List<UpdateMedicineDTO> medicines)
        {
            var updatedList = medicines.Select(m => new Medicine
            {
                Name = m.Name,
                Price = m.Price,
                UpdatedAt = DateTime.UtcNow
            }).ToList();

            await _repository.AddOrUpdateMedicinesAsync(updatedList);

            await InvalidateCacheAsync();
        }

        private async Task InvalidateCacheAsync()
        {
            var keys = await _redisCache.SetMembersAsync(CacheKeySet);

            foreach (var key in keys)
            {
                await _redisCache.KeyDeleteAsync(key.ToString());  
            }

            await _redisCache.KeyDeleteAsync(CacheKeySet);

            Console.WriteLine("Cache invalidated successfully.");
        }

    }
}
