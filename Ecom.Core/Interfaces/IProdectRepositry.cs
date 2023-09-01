using Ecom.Core.Dto;
using Ecom.Core.Entities;
using Ecom.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface IProdectRepositry:IGenericRepositrie<Prodect>
    {
        Task<bool> AddAsync(CreateProdectDTO prodectDTO);
        Task<bool> UpdateAsync(UpdateProdct updateProdct);
        Task<IEnumerable<ProdectDTO>> GetAllAsync(ProdcetParam prodcetParam);
    }
}
