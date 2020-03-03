using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;

namespace Common.Services.Infrastructure.Services
{
    public interface IUserService
    {
        Task<UserDTO> GetById(Guid id);
        Task<UserDTO> GetByName(string username);
        Task<UserDTO> GetByEmail(string email);
        Task<bool> Delete(Guid id);
        Task<UserDTO> Edit(UserDTO dto);
        Task<byte[]> GetUserPhoto(Guid userId);

        Task<IEnumerable<UserDTO>> GetAll();
    }
}