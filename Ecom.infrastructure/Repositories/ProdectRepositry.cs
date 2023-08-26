using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositories
{
    public class ProdectRepositry : GenericReposetroy<Prodect>, IProdectRepositry
    {
        public ProdectRepositry(ApplicationDbContext context) : base(context)
        {
        }
    }
}
