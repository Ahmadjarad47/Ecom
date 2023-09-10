using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepoetroy CategoryRepoetroy { get;  }
        public IProdectRepositry ProdectRepositry { get;  }
        public IBraketRepositry BraketRepositry { get; }
    }
}
