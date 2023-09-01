using AutoMapper;
using Ecom.API.Error;
using Ecom.API.Helper;
using Ecom.Core.Dto;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Core.Sharing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdectsController : ControllerBase
    {
        private readonly IUnitOfWork ofWork;
        private readonly IMapper mapper;

        public ProdectsController(IUnitOfWork ofWork,IMapper mapper)
        {
            this.ofWork = ofWork;
            this.mapper = mapper;
        }
        [HttpGet("get-All-Prodect")]



        public async Task<ActionResult> get([FromQuery] ProdcetParam prodcetParam)
        {
         IEnumerable<ProdectDTO> prodect=await ofWork.ProdectRepositry.GetAllAsync(prodcetParam);

            

            IReadOnlyList<ProdectDTO> res =
                mapper.Map<IReadOnlyList<ProdectDTO>>(prodect);
            int total = res.Count;
            return Ok(new Pagination<ProdectDTO>(prodcetParam.pageNumber
                ,prodcetParam.pageSize
                        ,total,res));
        }



        [HttpGet("get-prodect-by-id/{id}")]
        [ProducesResponseType(typeof(ProdectDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseComonentResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetId(int id)
        {
            Prodect src=await ofWork.ProdectRepositry.GetByIdAsync(id,x=>x.Category);
            ProdectDTO result=mapper.Map<ProdectDTO>(src);
            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound($"this id= {id}  was not found ! !");
        }
        [HttpPost("add-new-prodect")]
        public async Task<ActionResult> post([FromForm]CreateProdectDTO  dTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (dTO is not null)
                    {

                        var res = await ofWork.ProdectRepositry.AddAsync(dTO);
                        return res ? Ok("the prodect is added") : BadRequest("something went wrong!!");
                       /**/
                    }

                }
                return BadRequest("is null or something went error");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-prodect")]
        public async Task<ActionResult> putProdect([FromForm]UpdateProdct updateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (updateDto is not null)
                    {

                        var res = await ofWork.ProdectRepositry.UpdateAsync(updateDto);
                        return res ? Ok("the prodect is updated") : BadRequest("something went wrong!!");
                        /**/
                    }

                }
                return BadRequest("is null or something went error");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
