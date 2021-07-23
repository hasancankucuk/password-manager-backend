using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace password_manager_backend.Models
{
    public class UserInfoModel
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string userPassword { get; set; }
        
    }
}
