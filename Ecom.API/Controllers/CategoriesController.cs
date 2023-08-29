using Ecom.Core.Dto;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork ofWork;

        public CategoriesController(IUnitOfWork ofWork)
        {
            this.ofWork = ofWork;
        }
        [HttpGet("get-all-category")]
        public async Task<IActionResult> get()
        {
            var allCate=await ofWork.CategoryRepoetroy.GetAllAsync();
           
            if (allCate is not null)
            {
                var res = allCate.Select(x => new ListCategoryDTO
                {
                    Id= x.Id,
                    Name = x.Name,
                    Description = x.Description,
                }).ToList(); return Ok(res);
            }
             
            
          return BadRequest("Not Found");
        }
        [HttpGet("get-category-by-id/{id}")]
        public async Task<IActionResult>GetById(int id)
        {
            var category=await ofWork.CategoryRepoetroy.GetAsync(id);
            if (category is null)
            {
                return BadRequest($"Not found the id= [{id}]");
            }
            var res = new ListCategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };
            return Ok(res);
        }
        [HttpPost("add-new-category")]
        public async Task<IActionResult> postCategory(CategoryDTO categoryDto)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var newcategory = new Category
                    {
                        Name = categoryDto.Name,
                        Description = categoryDto.Description,
                    };
                    await ofWork.CategoryRepoetroy.AddAsync(newcategory);
                    return Ok(categoryDto);
                }
                return BadRequest("not valid");
            }
            catch(Exception)
            {

                return BadRequest(categoryDto);
            }
        }
        [HttpPut("update-exiting-category-by-id/{id}")]
        public async Task<IActionResult>put(int id, CategoryDTO categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Category exitingCategory = await ofWork.CategoryRepoetroy.GetAsync(id);
                    if (exitingCategory is not null)
                    {
                        exitingCategory.Name = categoryDto.Name;
                        exitingCategory.Description=categoryDto.Description;
                        await ofWork.CategoryRepoetroy.UpdateAsync(id,exitingCategory);
                        return Ok(categoryDto);
                    }
                }
                return BadRequest("Category not found by your Id");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete-category-id/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Category exitingCategory = await ofWork.CategoryRepoetroy.GetAsync(id);
                    if (exitingCategory is not null)
                    {
                        
                        await ofWork.CategoryRepoetroy.DeleteAsync(id);
                        return Ok($"Done !");
                    }
                }
                return BadRequest($"Category not found by your Id{id}");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
