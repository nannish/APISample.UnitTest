using APISample.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APISample.Services
{
    public interface IUserService
    {
        UserApiModel AddUser(UserApiModel model);
        UserApiModel EditUser(UserApiModel model);
        bool DeleteUser(long id);
        IList<UserApiModel> GetAllUsers();
    }
}
