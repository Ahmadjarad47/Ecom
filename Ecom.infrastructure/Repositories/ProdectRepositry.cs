using AutoMapper;
using Ecom.Core.Dto;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Core.Sharing;
using Ecom.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositories
{
    public class ProdectRepositry : GenericReposetroy<Prodect>, IProdectRepositry
    {
        private readonly ApplicationDbContext context;
        private readonly IFileProvider fileProvider;
        private readonly IMapper mapper;
       

        public ProdectRepositry(ApplicationDbContext context,IFileProvider fileProvider,IMapper mapper
           ) : base(context)
        {
            this.context = context;
            this.fileProvider = fileProvider;
            this.mapper = mapper;
           
        }

        public async Task<IEnumerable<ProdectDTO>>GetAllAsync(ProdcetParam prodcetParam)
        {

            var allProdect = await context.Prodects.AsNoTracking()
                .Include(m => m.Category)
                .ToListAsync();
            // paging

            if (!string.IsNullOrEmpty(prodcetParam.serach))
            {
                allProdect=allProdect.Where(x=>x.Name.Contains(prodcetParam.serach)||
                x.Description.Contains(prodcetParam.serach)).ToList();
            }
                //search by categoryId
            if (prodcetParam.categoryId.HasValue)
            {
                allProdect = allProdect.Where(m => m.CategoryId.
                Equals(prodcetParam.categoryId.Value)).ToList();
            }
            if (!string.IsNullOrEmpty(prodcetParam.sort))
            {
                allProdect = prodcetParam.sort switch
                {
                    "PriceAsync" => allProdect.OrderBy(m => m.Price).ToList(),
                    "PriceDesc" => allProdect.OrderByDescending(m => m.Price).ToList(),
                    _ => allProdect.OrderBy(m => m.Name).ToList(),
                };
            }  
            allProdect = allProdect.Skip((prodcetParam.pageSize) * (prodcetParam.pageNumber - 1))
                .Take(prodcetParam.pageSize).ToList();

            List<ProdectDTO> result=mapper.Map<List<ProdectDTO>>(allProdect);

            return result;
        }

        public async Task<bool> AddAsync(CreateProdectDTO prodectDTO)
        {
            if (prodectDTO.Image is not null)
            {
                var prodctNames = DateTime.Now.ToFileTime().ToString()+prodectDTO.Image.FileName;
                var prodctName = prodctNames.Replace(' ','_');
                if (!Directory.Exists("wwwroot" + "/images/prodects"))
                {
                    Directory.CreateDirectory("wwwroot" + "/images/prodects");
                }
                var src = "/images/prodects" + prodctName;
                var info = fileProvider.GetFileInfo(src);
                var root = info.PhysicalPath;
                using (FileStream stream=new(root,FileMode.Create))
                {
                   await  prodectDTO.Image.CopyToAsync(stream);

                }
                var res = mapper.Map<Prodect>(prodectDTO);
                res.PictuerProdect = src;
                await context.Prodects.AddAsync(res);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool>UpdateAsync(UpdateProdct updateProdct)
        {
            var current=await context.Prodects.FindAsync(updateProdct.Id);
            if (current is not null)
            {
 var src = "";

                if (updateProdct.Image is not null)
                {
                   
                    var prodctNames = DateTime.Now.ToFileTime().ToString() + updateProdct.Image.FileName;
                    var prodctName = prodctNames.Replace(' ', '_');
                    if (!Directory.Exists("wwwroot" + "/images/prodects"))
                    {
                        Directory.CreateDirectory("wwwroot" + "/images/prodects");
                    }
                    src = "/images/prodects" + prodctName;
                    var info = fileProvider.GetFileInfo(src);
                    var root = info.PhysicalPath;
                    using (FileStream stream = new(root, FileMode.Create))
                    {
                        await updateProdct.Image.CopyToAsync(stream);

                    }
                }
                if (!string.IsNullOrEmpty(current.PictuerProdect))
                {
                    var pichinfo = fileProvider.GetFileInfo(current.PictuerProdect);
                    var rootpath = pichinfo.PhysicalPath;
                    System.IO.File.Delete(rootpath);
                }
                var res = mapper.Map<Prodect>(updateProdct);
                context.Prodects.Remove(current);
                res.PictuerProdect=src;
                context.Prodects.Update(res);
                await context.SaveChangesAsync();
              

                return true;
            }
            return false;
        }
    }
}
