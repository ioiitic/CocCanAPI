using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.DTOs.Store
{
    public class CreateStoreDTO
    {
        [Required(ErrorMessage = "[Name] field in Store is required!")]
        [MaxLength(40, ErrorMessage = "[Name] field in Store is 40 characters max length!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "[Image] field in Store is required!")]
        [MaxLength(40, ErrorMessage = "[Image] field in Store is 40 characters max length!")]
        public string Image { get; set; }
    }
}
