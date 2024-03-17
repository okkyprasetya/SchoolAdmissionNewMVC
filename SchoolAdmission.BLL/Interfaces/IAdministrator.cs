using SchoolAdmission.BLL.DTOs.ApplicantDTO;
using SchoolAdmission.BLL.DTOs.VerificatorDTO;
using SchoolAdmission.DAL.BOs.ApplicantDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.BLL.Interfaces
{
    public interface IAdministrator:IVerificator,IUser
    {
        void addVerificator(CreateVerificatorDTO entity);
        IEnumerable<VerificatorDTO> getAllVerificator();
        VerificatorDTO getVerificator(int verID);
        void deleteVerificator(int UserID);
        void updateVerificator(UpdateVerificatorDTO entity);
        void deleteApplicantData(int UserID);
        AcademicDataDTO getAcademicDataByID(int UGDataID);
        PersonalDataDTO getPersonalDataByID(int UGDataID);
        List<AchievementRecordsDTO> getAchievementRecordsById(int UGDataID);
    }
}
