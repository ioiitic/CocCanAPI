﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.DTOs.Menu
{
    public class MenuDTO
    {
        [Required(ErrorMessage = "[Id] field is required!")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "[Name] field is required!")]
        [MaxLength(100, ErrorMessage = "[Name] field is 100 characters max length!")]
        public string Name { get; set; }
    }
}
