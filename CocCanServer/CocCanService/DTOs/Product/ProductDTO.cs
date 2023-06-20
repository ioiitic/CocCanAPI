using CocCanService.DTOs.Category;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.DTOs.Product
{
    public class ProductDTO
    {
        [Required(ErrorMessage = "[Id] field is required!")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "[Name] field is required!")]
        [MaxLength(40, ErrorMessage = "[Name] field is 40 characters max length!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "[Image] field is required!")]
        [MaxLength(40, ErrorMessage = "[Image] field is 40 characters max length!")]
        public string Image { get; set; }

        [Required(ErrorMessage = "[Categories] field is required!")]
        public virtual ICollection<CategoryDTO> Categories { get; set; }
    }
}
