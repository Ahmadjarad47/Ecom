using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Dto
{
    public record RegisterDTO
    ([Required]
    [EmailAddress]
        string Email,
        [Required]
        string UserName,
        [Required]
        [MinLength(5,ErrorMessage ="less charecter 5")]
        string DisplayName,
        [Required]
        [RegularExpression(@"(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$"
,ErrorMessage ="It expects atleast 1 small-case letter, 1 Capital letter, 1 digit, 1 special character and the length should be between 6-10 characters. The sequence of the characters is not important")]
        string Password
        );
}
