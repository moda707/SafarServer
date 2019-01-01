﻿namespace SafarApi.Core
{
    public class JwtOptions
    {
        public string Issuer { get; set; }

        public string Key { get; set; }

        public int ExpiresInSecond { get; set; }
    }
}
