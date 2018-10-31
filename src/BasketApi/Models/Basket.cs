using System.Collections.Generic;

namespace BasketApi.Models
{
    public class Basket
    {
        public string Id { get; set; }
        public IDictionary<string, Item> Items { get; set; }
    }
}