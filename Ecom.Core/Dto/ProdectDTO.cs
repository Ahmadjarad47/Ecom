using Ecom.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Core.Dto
{

    public class BaseProdect
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(1,9999,ErrorMessage ="price limited by {0} and {1}")]
        [RegularExpression(@"[0-9]*\.?[0-9]+",ErrorMessage ="{0} Must be Number !")]
        public decimal Price { get; set; }


    }
    public class ProdectDTO:BaseProdect
    {
        public int Id { get; set; }
      

        public string CategoryName { get; set; }
        public string Picture { get; set; }
    }

    public class CreateProdectDTO:BaseProdect
    {
        public int CategoryId { get; set; }
        public IFormFile Image { get; set; }
    }
    public class UpdateProdct:BaseProdect
    {
        public int Id { get; set; }
        public string OldImage { get; set; }
        public int CategoryId { get; set; }
        public IFormFile Image { get; set; }
    }
}
