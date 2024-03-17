using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.BLL.DTOs.ApplicantDTO
{
    public class AcademicDataDTO
    {
        public int UADataID { get; set; }
        public int RaportSummaries { get; set; }
        public string? RaportDocument { get; set; }
        public bool isVerified { get; set; }
    }
}
