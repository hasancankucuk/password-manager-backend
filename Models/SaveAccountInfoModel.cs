using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace password_manager_backend.Models
{
    public class SaveAccountInfoModel
    {
        public int Id { get; set; }
        public string savedUsername { get; set; }
        public string savedPassword { get; set; }
        public string savedUrl { get; set; }
        public int UserId { get; set; }
    }
}
