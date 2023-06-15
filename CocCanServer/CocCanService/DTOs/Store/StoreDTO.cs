using CocCanService.DTOs.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.DTOs.Store
{
    public class StoreDTO
    {
        [Required(ErrorMessage="[Id] field is required!")]
        public Guid Id { get; set; }

        [Required(ErrorMessage="[Name] field is required!")]
        [MaxLength(40,ErrorMessage="[Name] field is 40 characters max length!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "[Image] field is required!")]
        [MaxLength(40, ErrorMessage = "[Image] field is 40 characters max length!")]
        public string Image { get; set; }

        [Required(ErrorMessage = "[Products] field is required!")]
        public virtual ICollection<ProductDTO> Products { get; set; }
    }
}
