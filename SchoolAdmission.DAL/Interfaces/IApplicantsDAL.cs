using SchoolAdmission.DAL.BOs.ApplicantDatas;
using SchoolAdmission.DAL.BOs.GeneralObject;
using SchoolAdmission.DAL.BOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.DAL.Interfaces
{
    public interface IApplicantsDAL:IUsersDAL
    {
        void finalizeData(int uid);
        void completeApplicantGeneralData(ApplicantBO entity);
        void completeApplicantPersonalData(PersonalDataBO entity);
        void updateApplicantPersonalData(PersonalDataBO entity);
        void completeApplicantAcademicData(AcademicDataBO entity);
        void updateApplicantAcademicData(AcademicDataBO entity);
        void addAchievementRecord(AchievementRecordsBO entity);
        void deleteAchievementRecord(int achievementID);
        bool UpdateUserProfilePhoto(int userId, string photoPath);
        List<ScholarshipDataBO> generateScholarship();
        ApplicantBO getApplicantData(string email);
        AcademicDataBO getAcademicData(string email);
        List<AchievementRecordsBO> getAchievementRecord(string email);
        PersonalDataBO getPersonalData(string email);

    }
}
