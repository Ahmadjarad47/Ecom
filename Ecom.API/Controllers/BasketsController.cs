using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public BasketsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("get-basket-item/{Id}")]
        public async Task<IActionResult> GetBasket(string Id)
        {
            var basket = await unitOfWork.BraketRepositry.GetCustomerBasket(Id);

            return Ok(basket ?? new CustomerBasket());

        }
        [HttpPost("update-basket")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetUpdateAsyncOfBasket(CustomerBasket customer)
        {
            var basket = await unitOfWork.BraketRepositry.UpdateBasketAsync(customer);
            return Ok();
        }
        [HttpDelete("delete-basket-item/{Id}")]
        public async Task<IActionResult> DeleteBasket(string Id)
        {
            return Ok(await unitOfWork.BraketRepositry.DeleteBasketAsync(Id));
        }



    }
}
