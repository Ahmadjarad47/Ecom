using Ecom.API.Error;
using Ecom.infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadRequestController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public BadRequestController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("not-found")]
        public ActionResult GetNotFound()
        {
            var prodect = context.Prodects.Find(4000);
            if (prodect is null )
            {
                return NotFound(new BaseComonentResponse(404));
            }
            return Ok(prodect);
        }
        [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
            var prodect = context.Prodects.Find(4000);
            prodect.Name= "";
            return Ok(prodect);
        }
        [HttpGet("bad-request/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new BaseComonentResponse(400));
        }
    }
}
