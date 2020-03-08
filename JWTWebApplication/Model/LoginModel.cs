using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebApplication.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage ="UserName is Required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PassWord is Required.")]
        public string PassWord { get; set; }


    }
}
