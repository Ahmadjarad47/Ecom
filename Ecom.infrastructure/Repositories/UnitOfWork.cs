using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using Microsoft.Extensions.FileProviders;
using StackExchange.Redis;

namespace Ecom.infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly IConnectionMultiplexer multiplexer;
        private readonly ApplicationDbContext context;
        
        private readonly IFileProvider formatProvider;
        private readonly IMapper mapper;

        public ICategoryRepoetroy CategoryRepoetroy { get; }

        public IProdectRepositry ProdectRepositry { get; }

        public IBraketRepositry BraketRepositry { get; }

        public UnitOfWork(IConnectionMultiplexer multiplexer,ApplicationDbContext context,IFileProvider formatProvider,IMapper mapper, IBraketRepositry BraketRepositry)
        {
            this.multiplexer = multiplexer;
            this.context = context;
          
            this.formatProvider = formatProvider;
            this.mapper = mapper;
            CategoryRepoetroy = new CategoryRepositories(context);
            ProdectRepositry=new ProdectRepositry(context, fileProvider: formatProvider,mapper );
            this.BraketRepositry = new BasketRepositry(multiplexer);
        }
    }
}
