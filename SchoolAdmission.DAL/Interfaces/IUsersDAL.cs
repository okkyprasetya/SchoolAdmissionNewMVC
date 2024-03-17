using SchoolAdmission.DAL.BOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.DAL.Interfaces
{
    public interface IUsersDAL
    {
        UserBO login(string email, string password);
        void register(UserBO entity);
        UserBO getUserByEmail(String email);
        IEnumerable<UserBO> getAll();
    }
}
