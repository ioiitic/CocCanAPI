using AutoMapper;
using CocCanService.DTOs.Session;
using CocCanService.DTOs.Store;
using CocCanService.DTOs.Session;
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

        public async Task<ServiceResponse<SessionDTO>> CreateSessionAsync(CreateSessionDTO createSessionDTO)
        {
            ServiceResponse<SessionDTO> _response = new();
            try
            {
                var _newSession = _mapper.Map<Session>(createSessionDTO);
                if (!await _SessionRepo.CreateSessionAsync(_newSession))
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Some error occur in Category Repository when trying to create store!");
                    _response.Data = null;
                    return _response;
                }

                _response.Status = true;
                _response.Title = "Created";
                _response.Data = _mapper.Map<SessionDTO>(_newSession);
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

        public async Task<ServiceResponse<List<SessionDTO>>> GetAllSessionsAsync(string filter)
        {
            ServiceResponse<List<SessionDTO>> _response = new ServiceResponse<List<SessionDTO>>();
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

                var _SessionListDTO = new List<SessionDTO>();

                foreach (var item in _SessionList)
                {
                    _SessionListDTO.Add(_mapper.Map<SessionDTO>(item));
                }

                _response.Status = true;
                _response.Title = "Got all Sessions";
                _response.Data = _SessionListDTO;
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

        public async Task<ServiceResponse<SessionDTO>> GetSessionByGUIDAsync(Guid id)
        {
            ServiceResponse<SessionDTO> _response = new();

            try
            {

                var _Session = await _SessionRepo.GetSessionByGUIDAsync(id);

                if (_Session == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                var _SessionDto = _mapper.Map<SessionDTO>(_Session);

                _response.Status = true;
                _response.Title = "Got Session";
                _response.Data = _SessionDto;


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

        public Task<ServiceResponse<bool>> HardDeleteSessionAsync(SessionDTO SessionDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<string>> SoftDeleteSessionAsync(Guid id)
        {
            ServiceResponse<string> _response = new();
            try
            {
                var _existingSession = await _SessionRepo.GetSessionByGUIDAsync(id);
                if (_existingSession == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                if (!await _SessionRepo.SoftDeleteSessionAsync(id))
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Some error occur in Store Repository when trying to delete category!");
                    _response.Data = null;
                    return _response;
                }


                _response.Status = true;
                _response.Title = "Deleted Session";
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

        public async Task<ServiceResponse<SessionDTO>> UpdateSessionAsync(SessionDTO SessionDTO)
        {
            ServiceResponse<SessionDTO> _response = new();
            try
            {
                var _existingSession = await _SessionRepo.GetSessionByGUIDAsync(SessionDTO.Id)
                    ;
                if (_existingSession == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                _existingSession = _mapper.Map<Session>(SessionDTO);

                if (!await _SessionRepo.UpdateSessionAsync(_existingSession))
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Some error occur in Store Repository when trying to update category!");
                    _response.Data = null;
                    return _response;
                }

                _response.Status = true;
                _response.Title = "Updated category";
                _response.Data = _mapper.Map<SessionDTO>(_existingSession);
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
