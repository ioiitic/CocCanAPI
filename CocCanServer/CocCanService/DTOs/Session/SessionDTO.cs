using CocCanService.DTOs.Category;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.DTOs.Session
{
    public class SessionDTO
    {
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }
        public Guid TimeSlotId { get; set; }
        public Guid LocationId { get; set; }
        public Guid MenuId { get; set; }
    }
}
