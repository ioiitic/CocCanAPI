using AutoMapper;
using Repository.Entities;
using Repository.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services.Imp
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _SessionRepo;
        private readonly IMapper _mapper;

        public SessionService(ISessionRepository SessionRepo, IMapper mapper)
        {
            this._SessionRepo = SessionRepo;
            this._mapper = mapper;
        }

        public async Task<ServiceResponse<List<Session>>> GetAllSessionsWithStatusAsync(string filter)
        {
            ServiceResponse<List<Session>> _response = new ServiceResponse<List<Session>>();
            try
            {
                Dictionary<string, List<string>> _filter = null;
                try
                {
                    if (filter != null)
                        _filter = System.Text.Json.JsonSerializer
                            .Deserialize<Dictionary<string, List<string>>>(filter);
                }
                catch
                {
                    var raw = System.Text.Json.JsonSerializer
                        .Deserialize<Dictionary<string, string>>(filter);
                    _filter = new Dictionary<string, List<string>>();
                    foreach (var item in raw)
                        _filter.Add(item.Key, new List<string>() { item.Value });
                }

                var _SessionList = await _SessionRepo
                    .GetAllSessionsWithStatusAsync(_filter);


                _response.Status = true;
                _response.Title = "Got all Sessions";
                _response.Data = _SessionList.ToList();
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Title = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Data = null;
            }
            return _response;
        }
    }
}
