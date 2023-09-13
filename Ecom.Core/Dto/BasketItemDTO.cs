using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Dto
{
    public class BasketItemDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [Range(1,double.MaxValue)]
        public string picture { get; set; }
        [Range(1,11)]
        [Required]
        public int Quntity { get; set; }
        [Required]
        public string Category { get; set; }
    }
}
