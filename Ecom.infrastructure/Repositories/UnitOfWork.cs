using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
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

        public ICategoryRepoetroy CategoryRepoetroy { get; }

        public IProdectRepositry ProdectRepositry { get; }
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            CategoryRepoetroy = new CategoryRepositories(context);
            ProdectRepositry=new ProdectRepositry(context);
        }
    }
}
