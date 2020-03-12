using System;

namespace Common.DTO
{
    public class TimeZoneInfoDTO
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }
        public string StandardName { get; set; }
        public string DaylightName { get; set; }
        public TimeSpan BaseUtcOffset { get; set; }
        public bool SupportsDaylightSavingTime { get; set; }

        public TimeSpan UtcOffset
        {
            get
            {
                var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(Id);
                return timeZoneInfo.GetUtcOffset(DateTime.UtcNow);
            }
        }

        public TimeZoneInfoDTO() { }

        public TimeZoneInfoDTO(TimeZoneInfo timeZoneInfo)
        {
            SetTimeZone(timeZoneInfo);
        }

        public TimeZoneInfoDTO(string timeZoneId)
        {
            SetTimeZone(TimeZoneInfo.FindSystemTimeZoneById(timeZoneId));
        }

        private void SetTimeZone(TimeZoneInfo timeZoneInfo)
        {
            Id = timeZoneInfo.Id;
            DisplayName = timeZoneInfo.DisplayName;
            StandardName = timeZoneInfo.StandardName;
            DaylightName = timeZoneInfo.DaylightName;
            BaseUtcOffset = timeZoneInfo.BaseUtcOffset;
            SupportsDaylightSavingTime = timeZoneInfo.SupportsDaylightSavingTime;
        }
    }
}