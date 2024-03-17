using SchoolAdmission.BLL.DTOs.ApplicantDTO;
using SchoolAdmission.BLL.Interfaces;
using SchoolAdmission.DAL;
using SchoolAdmission.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.BLL
{
    public class VerificatorBLL : IVerificator
    {
        private readonly IVerificatorsDAL _verificator;

        public VerificatorBLL ()
        {
            _verificator = new VerificatorDAL();
        }
        public void AssignBills()
        {
            throw new NotImplementedException();
        }

        public void completeVerificatorData(int verificatorID, string position, string SKNumber)
        {
            throw new NotImplementedException();
        }

        public void finalizeLeaderboard(int quota)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicantsDTO> GetAllApplicants()
        {
            throw new NotImplementedException();
        }

        public ApplicantsDTO getApplicantByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<RankDTO> GetRank()
        {
            throw new NotImplementedException();
        }

        public void verifyAcademicData(int verificatorID, int UGDataID)
        {
            throw new NotImplementedException();
        }

        public void verifyAchievementRecord(int verificatorID, int UGDataID)
        {
            throw new NotImplementedException();
        }

        public void verifyPersonalData(int verificatorID, int UGDataID)
        {
            throw new NotImplementedException();
        }
    }
}
