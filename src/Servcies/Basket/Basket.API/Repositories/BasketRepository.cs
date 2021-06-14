using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository:IBasketRepository
    {

        private readonly IDistributedCache _redisCashe;

        public BasketRepository(IDistributedCache rediscache)
        {
            _redisCashe = rediscache;
        }

        public async Task DeleteBasket(string username)
        {
            await _redisCashe.RemoveAsync(username);
        }

        public async Task<ShoppingCart> GetBasket(string username)
        {
            var basket = await _redisCashe.GetStringAsync(username);

            if (basket == null)
            {
                return null;
            }

            else
                return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingbasket)
        {
              await _redisCashe.SetStringAsync(shoppingbasket.UserName,JsonConvert.SerializeObject(shoppingbasket));

            return await GetBasket(shoppingbasket.UserName);
        }
    }
}
