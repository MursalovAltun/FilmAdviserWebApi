namespace Common.DTO.AuthDTO
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string NormalizedEmail => this.Email.Normalize().ToUpperInvariant();
    }
}