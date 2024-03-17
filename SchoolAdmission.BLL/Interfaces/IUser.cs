using SchoolAdmission.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmission.BLL.Interfaces
{
    public interface IUser
    {
        UsersDTO login(string email, string password);
        void register(UsersDTO entity);
        UsersDTO getUserByEmail(String email);
        IEnumerable<UsersDTO> getAll();
    }
}
