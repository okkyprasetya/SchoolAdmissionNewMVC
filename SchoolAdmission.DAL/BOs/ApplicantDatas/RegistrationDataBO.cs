using SchoolAdmission.DAL.BOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.DAL.BOs.ApplicantDatas
{
    public class RegistrationDataBO
    {
        public int RegistrationID { get; set; }
        public String? Status { get; set; }
        public int TotalScore {  get; set; }
        public UserBO? User { get; set; }
        public VerificatorBO? Verificator { get; set; }
    }
}
