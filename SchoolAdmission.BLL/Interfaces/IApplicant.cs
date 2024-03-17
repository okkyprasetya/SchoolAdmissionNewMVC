using SchoolAdmission.BLL.DTOs.ApplicantDTO;
using SchoolAdmission.BLL.DTOs.GeneralObjectDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.BLL.Interfaces
{
    public interface IApplicant
    {
        void finalizeData(int uid);
        void completeApplicantGeneralData(ApplicantsDTO entity);
        void completeApplicantPersonalData(PersonalDataDTO entity);
        void updateApplicantPersonalData(PersonalDataDTO entity);
        void completeApplicantAcademicData(AcademicDataDTO entity);
        void updateApplicantAcademicData(AcademicDataDTO entity);
        void addAchievementRecord(AchievementRecordsDTO entity);
        void deleteAchievementRecord(int achievementID);
        bool UpdateUserProfilePhoto(int userId, string photoPath);
        List<ScholarshipDataDTO> generateScholarship();
        ApplicantsDTO getApplicantData(string email);
        AcademicDataDTO getAcademicData(string email);
        List<AchievementRecordsDTO> getAchievementRecord(string email);
        PersonalDataDTO getPersonalData(string email);
    }
}
