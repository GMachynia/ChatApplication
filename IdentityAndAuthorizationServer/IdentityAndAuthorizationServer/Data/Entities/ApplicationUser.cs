using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAndAuthorizationServer.Models
{
    [Table("Users")]
    public class ApplicationUser: IdentityUser
    {
        [Column(TypeName= "nvarchar(150)")]
        public string FullName { get; set; }
        [ForeignKey("UserId")]
        public virtual ICollection<PublicChatMessage> Messages { get; set; }
    
    }
}
