using SchoolAdmission.DAL.BOs;
using SchoolAdmission.DAL.BOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.DAL.Interfaces
{
    public interface IAdministrator
    {
        void addVerificator(VerificatorBO entity);
        IEnumerable<VerificatorBO> getAllVerificator();
        VerificatorBO getVerificator(int verID);
        void deleteVerificator(int verificatorId);
        void updateVerificator(VerificatorBO entity);
        void deleteApplicantData(int UserId);
    }
}
