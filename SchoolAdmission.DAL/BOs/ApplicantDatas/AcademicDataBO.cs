using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolAdmission.DAL.BOs.Users;

namespace SchoolAdmission.DAL.BOs.ApplicantDatas
{
    public class AcademicDataBO
    {
        public int UADataID { get; set; }
        public int RaportSummaries { get; set; }
        public string? RaportDocument { get; set; }
        public bool isVerified { get; set; }
    }
}
