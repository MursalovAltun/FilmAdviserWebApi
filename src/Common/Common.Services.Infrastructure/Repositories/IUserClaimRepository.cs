using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities;
using Common.Entities.System;

namespace Common.Services.Infrastructure.Repositories
{
    public interface IUserClaimRepository<TUserClaim> where TUserClaim : UserClaim
    {
        Task<IList<TUserClaim>> GetByUserId(Guid userId, ContextSession session);
        Task Delete(Guid userId, string claimType, string claimValue, ContextSession session);
        Task<TUserClaim> Add(TUserClaim userClaim, ContextSession session);
        Task<IList<TUserClaim>> EditMany(IList<TUserClaim> userClaims, ContextSession session);
        Task<IList<TUserClaim>> GetList(Guid userId, string claimType, string claimValue, ContextSession session);
    }
}