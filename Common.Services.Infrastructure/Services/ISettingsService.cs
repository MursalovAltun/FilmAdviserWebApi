using System;
using System.Threading.Tasks;
using Common.DTO;

namespace Common.Services.Infrastructure.Services
{
    public interface ISettingsService
    {
        Task<SettingsDTO> GetById(Guid id);

        Task<SettingsDTO> Edit(SettingsDTO settings);
    }
}