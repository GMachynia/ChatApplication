using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityAndAuthorizationServer.Models;

namespace IdentityAndAuthorizationServer.Models
{
    public class AuthenticationContext: DbContext
    {
        public AuthenticationContext(DbContextOptions options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set;}
        public DbSet<PublicChatMessage> PublicChatMessages { get; set; }
    }
}
