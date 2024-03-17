using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.BLL.DTOs.ApplicantDTO
{
    public class PersonalDataDTO
    {
        public int UPDataID { get; set; }
        public string? FatherName { get; set; }
        public string? FatherAddress { get; set; }
        public string? FatherJob { get; set; }
        public int FatherSalary { get; set; }
        public string? MotherName { get; set; }
        public string? MotherAddress { get; set; }
        public string? MotherJob { get; set; }
        public int MotherSalary { get; set; }
        public int SiblingsNumber { get; set; }
        public string? Hobi { get; set; }
        public string? KKDocument { get; set; }
        public string? BirthDocument { get; set; }
        public bool isVerified { get; set; }
    }
}
