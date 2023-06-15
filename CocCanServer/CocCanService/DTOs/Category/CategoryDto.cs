using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.DTOs.Category
{
    public class CategoryDTO
    {
        [Required(ErrorMessage = "[Id] field is required!")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "[Name] field is required!")]
        [MaxLength(40, ErrorMessage = "[Name] field is 40 characters max length!")]
        public string Name { get; set; }
    }
}
