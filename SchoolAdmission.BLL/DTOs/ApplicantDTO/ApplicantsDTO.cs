using SchoolAdmission.BLL.DTOs.GeneralObjectDTO;
using SchoolAdmission.DAL.BOs.ApplicantDatas;
using SchoolAdmission.DAL.BOs.GeneralObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.BLL.DTOs.ApplicantDTO
{
    public class ApplicantsDTO
    {
        public int UGDataID { get; set; }
        public UsersDTO Users { get; set; }
        public string? NIS { get; set; }
        //public string? DateBirth { get; set; }
        public bool isScholarship { get; set; }
        public ScholarshipDataDTO? Scholarship { get; set; }
        public int countVerif { get; set; }
        public bool isFinal { get; set; }
        public AcademicDataDTO? AcademicData { get; set; }
        public List<AchievementRecordsDTO>? AchievementRecords { get; set; } = null;
        public PersonalDataDTO? PersonalData { get; set; }
    }
}
