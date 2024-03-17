using SchoolAdmission.BLL.DTOs.GeneralObjectDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.BLL.DTOs.VerificatorDTO
{
    public class VerificatorDTO
    {
        public int VerificatorID { get; set; }
        public string? Position { get; set; }
        public string? SKNumber { get; set; }
        public RoleDTO? Role { get; set; }
        public UsersDTO? User { get; set; }
    }
}
