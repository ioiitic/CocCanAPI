using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.DTOs.Store
{
    public class UpdateStoreDTO
    {
        [Required(AllowEmptyStrings = true)]
        [MaxLength(100, ErrorMessage = "[Name] field in StoreDTO is 100 characters max length!")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = true)]
        [MaxLength(100, ErrorMessage = "[Image] field in StoreDTO is 100 characters max length!")]
        public string Image { get; set; }
    }
}
