using System.ComponentModel.DataAnnotations;

namespace Ecom.Core.Dto
{
    public class CategoryDTO
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class ListCategoryDTO:CategoryDTO
    {
        public int Id { get; set; }
    }
}
