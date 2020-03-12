namespace Common.DTO.AuthDTO
{
    public class Token
    {
        /// <summary>
        /// Describes when the AccessToken will be expired
        /// </summary>
        public double Expires_in { get; set; }

        /// <summary>
        /// The token that gives access to the protected endpoints
        /// </summary>
        public string Access_token { get; set; }

        /// <summary>
        /// The token that can be used to refresh the AccessToken
        /// </summary>
        public string Refresh_token { get; set; }
    }
}