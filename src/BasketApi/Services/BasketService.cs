using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using BasketApi.Models;

namespace BasketApi.Services
{
    public class BasketService
    {
        private static readonly IDictionary<string, Basket> Baskets = new ConcurrentDictionary<string, Basket>();

        public Basket Create()
        {
            var createdBasket = new Basket
            {
                Id = Guid.NewGuid().ToString("N"),
                Items = new Dictionary<string, Item>()
            };
            Baskets.Add(createdBasket.Id, createdBasket);
            return createdBasket;
        }

        public Basket Get(string id)
        {
            return Baskets.ContainsKey(id) ? Baskets[id] : null;
        }

        public void AddItem(string basketId, string itemId, Item item)
        {
            var basket = Get(basketId);
            if (basket == null || string.IsNullOrWhiteSpace(itemId) || itemId != item?.Id)
            {
                throw new ArgumentException();
            }

            if (basket.Items.ContainsKey(itemId))
            {
                basket.Items[itemId] = item;
            }
            else
            {
                basket.Items.Add(itemId, item);
            }
        }
    }
}