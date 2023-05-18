namespace Ergenekon.API.Other
{
    public class JwtSettings
    {
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public string[] Issuers { get; set; }
        public string[] Audiences { get; set; }
        public string SecretKey { get; set; }
    }
}
