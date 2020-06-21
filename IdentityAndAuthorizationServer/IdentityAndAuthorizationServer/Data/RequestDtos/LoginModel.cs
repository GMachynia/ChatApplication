using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAndAuthorizationServer.Models
{
    public class LoginModel
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "The {0} field is mandatory.")]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "The {0} field is mandatory.")]
        public string Password { get; set; }
    }
}
