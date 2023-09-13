using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Dto
{
    public record AddressDTO(
       [Required]
     string FirstName,
         [Required]
     string LastName ,
           [Required]
     string street   ,
             [Required]
     string city     ,
               [Required]
     string state    ,
                 [Required]
     string zipcode  
    
    );
    
}
