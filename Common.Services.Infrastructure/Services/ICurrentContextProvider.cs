using Common.Entities.System;

namespace Common.Services.Infrastructure.Services
{
    public interface ICurrentContextProvider
    {
        ContextSession GetCurrentContext();
    }
}