using SchoolAdmission.DAL.BOs.GeneralObject;
using SchoolAdmission.DAL.BOs.NewStudentDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.DAL.BOs.ApplicantDatas
{
    public class StudentBillBO
    {
        public NewStudentBO? newStudent {  get; set; }
        public BillBO? newBill { get; set; }
        public bool? isPaid { get; set; }
    }
}
