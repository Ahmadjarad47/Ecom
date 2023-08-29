using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        
        private readonly IFileProvider formatProvider;
        private readonly IMapper mapper;

        public ICategoryRepoetroy CategoryRepoetroy { get; }

        public IProdectRepositry ProdectRepositry { get; }
        public UnitOfWork(ApplicationDbContext context,IFileProvider formatProvider,IMapper mapper)
        {
            this.context = context;
          
            this.formatProvider = formatProvider;
            this.mapper = mapper;
            CategoryRepoetroy = new CategoryRepositories(context);
            ProdectRepositry=new ProdectRepositry(context, fileProvider: formatProvider,mapper );
        }
    }
}
