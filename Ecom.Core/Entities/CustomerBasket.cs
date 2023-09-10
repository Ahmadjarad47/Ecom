using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket(string id)
        {
            Id = id;
        }
        public CustomerBasket()
        {
            
        }
        public string Id { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new();
    }
}
