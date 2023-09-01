using System.ComponentModel.DataAnnotations;

namespace Ecom.Core.Dto
{
    public record CategoryDTO
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
    /*    public record ListCategoryDTO : CategoryDTO
        {
          public  int Id;
        }*/
    public record ListCategoryDTO : CategoryDTO
    {
        public int Id { get; set; }
    }
    /*   public record CategoryDTO
       {
           [Required]
           string Name;

           string Description;
       }*/
}
