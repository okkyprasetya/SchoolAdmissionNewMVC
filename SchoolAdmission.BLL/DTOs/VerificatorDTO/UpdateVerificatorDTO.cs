using SchoolAdmission.BLL.DTOs.GeneralObjectDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.BLL.DTOs.VerificatorDTO
{
    public class UpdateVerificatorDTO
    {
        public int VerificatorID { get; set; }
        public string? Position { get; set; }
        public string? SKNumber { get; set; }
        public UsersDTO Users { get; set; }
    }
}
