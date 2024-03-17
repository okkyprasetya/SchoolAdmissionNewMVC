using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.BLL.DTOs.ApplicantDTO
{
    public class AchievementRecordsDTO
    {
        public int AchievementID { get; set; }
        public int UGDataID { get; set; }
        public string? Title { get; set; }
        public string? Level { get; set; }
        public string? Description { get; set; }
        public string? AchievementDocument { get; set; }
        public int AchievementScore { get; set; }
        public bool isVerified { get; set; }
        public ApplicantsDTO? Applicant { get; set; }
    }
}
