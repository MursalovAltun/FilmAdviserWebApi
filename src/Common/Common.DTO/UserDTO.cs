using System;
using System.Collections.Generic;

namespace Common.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public int? Age { get; set; }

        public string TimeZoneId { get; set; }

        public TimeZoneInfoDTO TimeZone => TimeZoneId != null ? new TimeZoneInfoDTO(TimeZoneId) : new TimeZoneInfoDTO("Eastern Standard Time");

        public AddressDTO Address { get; set; }

        public SettingsDTO Settings { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}