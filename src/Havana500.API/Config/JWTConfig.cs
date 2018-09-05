using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Havana500.Config
{
    public class JWTConfig
    {
        public string ValidIssuer => "http://localhost:5000";

        public string ValidAudience => "http://localhost:5000/resources";

        public string Secret => "Havana500_Secret";

        public string ClientId => "angular_client";

        public List<Claim> DefaultClaims => new List<Claim>()
        {
            new Claim("idp", "local"),
            new Claim("client_id", ClientId)
        };
    }
}
