namespace Common.DTO.AuthDTO
{
    public class Token
    {
        /// <summary>
        /// Describes when the AccessToken will be expired
        /// </summary>
        public double ExpiresIn { get; set; }

        /// <summary>
        /// The token that gives access to the protected endpoints
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The token that can be used to refresh the AccessToken
        /// </summary>
        public string RefreshToken { get; set; }
    }
}