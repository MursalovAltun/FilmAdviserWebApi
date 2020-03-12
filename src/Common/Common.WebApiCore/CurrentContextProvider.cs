using Common.Entities.System;
using Common.Services.Infrastructure.Services;
using Common.WebApiCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Common.WebApiCore
{
    public class CurrentContextProvider : ICurrentContextProvider
    {
        private readonly IHttpContextAccessor _accessor;
        public CurrentContextProvider(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public ContextSession GetCurrentContext()
        {
            if (_accessor?.HttpContext?.User != null && _accessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var currentUserId = _accessor.HttpContext.User.GetUserId();

                if (!string.IsNullOrEmpty(currentUserId.ToString()))
                {
                    return new ContextSession { UserId = currentUserId };
                }
            }

            return new ContextSession();
        }
    }
}