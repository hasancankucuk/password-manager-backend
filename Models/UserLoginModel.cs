﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace password_manager_backend.Models
{
    public class UserLoginModel
    {
        public int Id {get;set;}
        public int UserId { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
