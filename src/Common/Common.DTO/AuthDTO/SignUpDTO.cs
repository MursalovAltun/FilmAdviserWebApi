namespace Common.DTO.AuthDTO
{
    public class SignUpDTO : LoginDTO
    {
        public string FullName { get; set; }

        public string ConfirmPassword { get; set; }

        private string _timeZoneId;

        public string TimeZoneId
        {
            get => !string.IsNullOrEmpty(this._timeZoneId) ? this._timeZoneId : "Eastern Standard Time";
            set => this._timeZoneId = value;
        }

        public string NormalizedFullName => this.FullName.Normalize().ToUpperInvariant();
    }
}