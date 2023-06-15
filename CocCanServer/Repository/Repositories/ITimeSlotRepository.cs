using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repositories
{
    public interface ITimeSlotRepository
    {
        Task<ICollection<TimeSlot>>
            GetAllTimeSlotWithStatusAsync(string search, int from, int to, string filter, string orderBy, bool ascending);
        Task<bool> CreateTimeSlotAsync(TimeSlot timeSlot);
        Task<bool> UpdateTimeSlotAsync(TimeSlot timeSlot);
        Task<bool> SoftDeleteTimeSlotAsync(Guid id);
        Task<TimeSlot> GetTimeSlotByGUIDAsync(Guid id);
    }
}
