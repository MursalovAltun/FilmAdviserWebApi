using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories;
using Common.Services.Infrastructure.Services;

namespace Common.Services
{
    public class UserService<TUser> : BaseService, IUserService where TUser : User, new()
    {
        protected readonly IUserRepository<TUser> userRepository;
        protected readonly IUserPhotoRepository userPhotoRepository;
        private readonly IMapper _mapper;

        public UserService(ICurrentContextProvider contextProvider, IUserRepository<TUser> userRepository,
            IUserPhotoRepository userPhotoRepository, IMapper mapper) : base(contextProvider)
        {
            this.userRepository = userRepository;
            this.userPhotoRepository = userPhotoRepository;
            this._mapper = mapper;
        }

        public async Task<bool> Delete(Guid id)
        {
            await userRepository.Delete(id, Session);
            return true;
        }

        public async Task<UserDTO> Edit(UserDTO dto)
        {
            var user = this._mapper.Map<TUser>(dto);
            await userRepository.Edit(user, Session);
            return this._mapper.Map<UserDTO>(user);
        }

        public async Task<byte[]> GetUserPhoto(Guid userId)
        {
            var photoContent = await userPhotoRepository.Get(userId, Session);
            return photoContent?.Image;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var users = await userRepository.GetAll(Session);
            return this._mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetById(Guid id)
        {
            var user = await userRepository.Get(id, Session);
            return this._mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetByName(string username)
        {
            var user = await userRepository.GetByUserName(username.Normalize().ToUpperInvariant(), Session);
            return this._mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetByEmail(string email)
        {
            var user = await userRepository.GetByEmail(email.Normalize().ToUpperInvariant(), Session);
            return this._mapper.Map<UserDTO>(user);
        }
    }
}