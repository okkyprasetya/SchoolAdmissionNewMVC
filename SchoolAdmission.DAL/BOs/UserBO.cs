using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SchoolAdmission.DAL.BOs.GeneralObject;

namespace SchoolAdmission.DAL.BOs
{
    public class UserBO
    {
        public int UserID { get; set; }
        public String? UserEmail { get; set; }
        public String? Password { get; set; }
        public String? FirstName { get; set; }
        public String? MiddleName { get; set; }
        public String? LastName { get; set; }
        public String? CreatedAt { get; set; }
        public String? UpdatedAt { get; set; }
        public RoleBO? Role { get; set; }
    }
}
