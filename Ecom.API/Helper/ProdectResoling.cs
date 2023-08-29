using AutoMapper;
using AutoMapper.Execution;
using Ecom.Core.Dto;
using Ecom.Core.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Ecom.API.Helper
{
    public class ProdectResoling : IValueResolver<Prodect, ProdectDTO, string>
    {
        private readonly IConfiguration configuration;

        public ProdectResoling(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Prodect source, ProdectDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictuerProdect))
            {
                return $"{configuration["APIURL"]}/{source.PictuerProdect}";
            }
            return null;
        }
    }
}
