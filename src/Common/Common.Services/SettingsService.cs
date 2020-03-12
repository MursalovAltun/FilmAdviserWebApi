using System;
using System.Threading.Tasks;
using AutoMapper;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories;
using Common.Services.Infrastructure.Services;

namespace Common.Services
{
    public class SettingsService : BaseService, ISettingsService
    {
        protected readonly ISettingsRepository settingsRepository;
        private readonly IMapper _mapper;

        public SettingsService(ICurrentContextProvider contextProvider, ISettingsRepository settingsRepository,
                               IMapper mapper) : base(contextProvider)
        {
            this.settingsRepository = settingsRepository;
            this._mapper = mapper;
        }

        public async Task<SettingsDTO> Edit(SettingsDTO dto)
        {
            var settings = this._mapper.Map<Settings>(dto);
            await this.settingsRepository.Edit(settings, Session);
            return this._mapper.Map<SettingsDTO>(settings);
        }

        public async Task<SettingsDTO> GetById(Guid id)
        {
            var user = await settingsRepository.Get(id, Session);
            return this._mapper.Map<SettingsDTO>(user);
        }
    }
}