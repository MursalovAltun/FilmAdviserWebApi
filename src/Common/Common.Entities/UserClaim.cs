using System;

namespace Common.Entities
{
    public class UserClaim : BaseEntity
    {
        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}