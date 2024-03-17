using SchoolAdmission.BLL.DTOs.GeneralObjectDTO;
using SchoolAdmission.BLL.DTOs;

namespace SchoolAdmissionNewMVC.Models
{
    public class VerificatorViewModel
    {
        public int VerificatorID { get; set; }
        public string? Position { get; set; }
        public string? SKNumber { get; set; }
        public RoleDTO? Role { get; set; }
        public UsersDTO? User { get; set; }
    }
}
