using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAndAuthorizationServer.Models
{
    [Table("PublicConversation")]
    public class PublicChatMessage
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(450)")]
        public string UserId { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
