using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSale.Persistence.Validators.Users
{
    public class UserSecurityUpdate
    {
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ReturnNewPassword { get; set; } = string.Empty;
    }
}
