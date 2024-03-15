using SchoolAdmission.DAL.BOs.ApplicantDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.DAL.BOs.NewStudentDatas
{
    public class NewStudentBO
    {
        public int studentID {  get; set; }
        public RegistrationDataBO? RegistrationData { get; set; }
    }
}
