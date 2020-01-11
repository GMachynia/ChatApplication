using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAndAuthorizationServer.Models
{
    public class ChatMessageModel
    {
        [DisplayName("sender")]
        public string Sender { get; set; }
   
        [DisplayName("message")]
        public string Message { get; set; }
        [DisplayName("date")]
        public DateTime Date { get; set; }
    }
}
