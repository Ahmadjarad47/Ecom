using AutoMapper;
using Ecom.Core.Dto;
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
        private readonly IMapper mapper;

        public BasketsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet("get-basket-item/{Id}")]
        public async Task<IActionResult> GetBasket(string Id)
        {
            var basket = await unitOfWork.BraketRepositry.GetCustomerBasket(Id);

            return Ok(basket ?? new CustomerBasket());

        }
        [HttpPost("update-basket")]
        
        public async Task<IActionResult> GetUpdateAsyncOfBasket(CustomerBasketDTO customer)
        {
            var map= this.mapper.Map<CustomerBasketDTO, CustomerBasket>(customer);
            var basket = await unitOfWork.BraketRepositry.UpdateBasketAsync(map);
            return Ok(basket);
        }
        [HttpDelete("delete-basket-item/{Id}")]
        public async Task<IActionResult> DeleteBasket(string Id)
        {
            return Ok(await unitOfWork.BraketRepositry.DeleteBasketAsync(Id));
        }



    }
}
