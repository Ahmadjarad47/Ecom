using Ecom.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface IBraketRepositry
    {
        Task<CustomerBasket> GetCustomerBasket(string basketId);
        Task<CustomerBasket> UpdateBasketAsync( CustomerBasket customerBasket);
        Task<bool> DeleteBasketAsync(string basketId);  
    }
}
