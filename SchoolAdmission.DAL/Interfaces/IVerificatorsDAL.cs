using SchoolAdmission.DAL.BOs.ApplicantDatas;
using SchoolAdmission.DAL.BOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SchoolAdmission.DAL.Interfaces
{
    public interface IVerificatorsDAL
    {
        IEnumerable<ApplicantBO> GetAllApplicants();
        ApplicantBO getApplicantByID(int id);
        void completeVerificatorData(int verificatorID, string position, string SKNumber);
        void verifyAcademicData(int verificatorID, int UGDataID);
        void verifyAchievementRecord(int verificatorID, int UGDataID);
        void verifyPersonalData(int verificatorID, int UGDataID);
        void finalizeLeaderboard(int quota);
        void AssignBills();
        List<RankBO> GetRank();
    }
}
