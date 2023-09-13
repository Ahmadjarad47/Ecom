using Ecom.Core.Dto;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositories
{
    public class BasketRepositry : IBraketRepositry
    {
        private readonly IDatabase _database;
        public BasketRepositry(IConnectionMultiplexer Redis)
        {
            _database = Redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            var check=await _database.KeyExistsAsync(basketId);
            if (check)
              return await _database.KeyDeleteAsync(basketId);
            return false;
        }

        public async Task<CustomerBasket> GetCustomerBasket(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);
            if (!string.IsNullOrEmpty(data))
            {
                return JsonSerializer.Deserialize<CustomerBasket>(data);
            }
            return null;
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
        {   
            var data = await _database.StringSetAsync(customerBasket.Id, 
                JsonSerializer.Serialize(customerBasket), TimeSpan.FromDays(2));
            if (!data)
            {
                return null;
            }
            return await GetCustomerBasket(customerBasket.Id);
        }
    }
}
