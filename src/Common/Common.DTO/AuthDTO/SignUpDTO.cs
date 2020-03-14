namespace Common.DTO.AuthDTO
{
    public class SignUpDTO : LoginDTO
    {
        public string FullName { get; set; }

        public string ConfirmPassword { get; set; }

        public string TimeZoneId { get; set; }

        public string NormalizedFullName => this.FullName.Normalize().ToUpperInvariant();
    }
}