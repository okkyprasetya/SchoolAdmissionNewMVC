using SchoolAdmission.DAL.BOs.ApplicantDatas;
using SchoolAdmission.DAL.BOs.GeneralObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.DAL.BOs.Users
{
    public class ApplicantBO
    {
        public int UGDataID { get; set; }
        public int UserID { get; set; }
        public string? NIS { get; set; }
        public string? DateBirth { get; set; }
        public bool isScholarship { get; set; }
        public ScholarshipDataBO? Scholarship { get; set; }
        public int countVerif { get; set; }
        public bool isFinal { get; set; }
        public AcademicDataBO? AcademicData { get; set; }
        public AchievementRecordsBO? AchievementRecords { get; set; } = null;
        public PersonalDataBO? PersonalData { get; set; }
    }
}
