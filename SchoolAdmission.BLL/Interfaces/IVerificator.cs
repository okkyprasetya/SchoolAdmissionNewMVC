using SchoolAdmission.BLL.DTOs.ApplicantDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.BLL.Interfaces
{
    public interface IVerificator
    {
        IEnumerable<ApplicantsDTO> GetAllApplicants();
        ApplicantsDTO getApplicantByID(int id);
        void completeVerificatorData(int verificatorID, string position, string SKNumber);
        void verifyAcademicData(int verificatorID, int UGDataID);
        void verifyAchievementRecord(int verificatorID, int UGDataID);
        void verifyPersonalData(int verificatorID, int UGDataID);
        void finalizeLeaderboard(int quota);
        void AssignBills();
        List<RankDTO> GetRank();
    }
}
