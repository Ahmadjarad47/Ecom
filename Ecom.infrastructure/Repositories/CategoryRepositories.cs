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
    public class CategoryRepositories : GenericReposetroy<Category>, ICategoryRepoetroy
    {
        public CategoryRepositories(ApplicationDbContext context) : base(context)
        {
        }
    }
}
