using SchoolAdmission.DAL.BOs;
using SchoolAdmission.DAL.BOs.ApplicantDatas;
using SchoolAdmission.DAL.BOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.DAL.Interfaces
{
    public interface IAdministratorDAL:IVerificatorsDAL,IUsersDAL
    {
        void addVerificator(VerificatorBO entity);
        IEnumerable<VerificatorBO> getAllVerificator();
        VerificatorBO getVerificator(int verID);
        void deleteVerificator(int UserID);
        void updateVerificator(VerificatorBO entity);
        void deleteApplicantData(int UserID);
        AcademicDataBO getAcademicDataByID(int UGDataID);
        PersonalDataBO getPersonalDataByID(int UGDataID);
        List<AchievementRecordsBO> GetAchievementRecords(int UGDataID);

    }
}
