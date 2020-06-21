using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAndAuthorizationServer.Models
{
    public class ApplicationSettings
    {
        public string JWT_Secret { get; set; }
        public string ClientAngular_URL { get; set; }
    }
}
