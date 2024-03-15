using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.DAL.BOs.ApplicantDatas
{
    public class RankBO
    {
        public int Rank {  get; set; }
        public int RegistrationID { get; set; }
        public string? Name { get; set; }
        public int TotalScore { get; set; }
    }
}
