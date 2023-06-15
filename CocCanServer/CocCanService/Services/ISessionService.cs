using CocCanService.Services.Imp;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services
{
    public interface ISessionService
    {
        Task<ServiceResponse<List<Session>>>
            GetAllSessionsWithStatusAsync(string search, int from, int to, string filter, string orderBy, bool ascending);
    }
}
