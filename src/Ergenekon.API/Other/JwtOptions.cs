﻿namespace Ergenekon.API.Other
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
