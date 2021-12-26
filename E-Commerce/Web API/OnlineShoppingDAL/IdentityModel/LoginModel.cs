﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingDAL.IdentityModel
{
    public class LoginModel
    {

        [Required(ErrorMessage = "UserName Required")]
        public string UserName { get; set; }



        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }
    }
}
