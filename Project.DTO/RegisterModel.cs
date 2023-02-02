using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DTO
{
    public class RegisterModel
    {
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Mobile { get; set; }
        public string? SmsVerificationCode { get; set; }
    }
}
