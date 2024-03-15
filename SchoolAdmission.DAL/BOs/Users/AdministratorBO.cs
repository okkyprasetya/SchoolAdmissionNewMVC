using SchoolAdmission.DAL.BOs.GeneralObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.DAL.BOs.Users
{
    public class AdministratorBO
    {
        public int AdminID { get; set; }
        public RoleBO? Role { get; set; }
    }
}
