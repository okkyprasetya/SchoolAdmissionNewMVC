using SchoolAdmission.DAL.BOs.GeneralObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.DAL.BOs.Users
{
    public class VerificatorBO
    {
        public int VerificatorID { get; set; }
        public string? Position { get; set; }
        public string? SKNumber { get; set; }
        public RoleBO? Role { get; set; }
        public UserBO? User { get; set; }
    }
}
