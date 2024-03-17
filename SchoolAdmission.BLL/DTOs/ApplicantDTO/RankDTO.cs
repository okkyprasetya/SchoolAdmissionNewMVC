using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.BLL.DTOs.ApplicantDTO
{
    public class RankDTO
    {
        public int Rank { get; set; }
        public int RegistrationID { get; set; }
        public string? Name { get; set; }
        public int TotalScore { get; set; }

        public string status { get; set; }
        public UsersDTO Users { get; set; }
    }
}
