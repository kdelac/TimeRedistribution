﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedAppCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(string userName)
        {
            UserName = userName;
        }
    }
}
