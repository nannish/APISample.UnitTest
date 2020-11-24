using APISample.APIModels;
using APISample.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APISample.Convertors
{
    public static class UserModelConvertors
    {
        public static User ToUserBSModel(this UserApiModel apiModel)
        {
            return new User()
            {
                FirstName = apiModel.FirstName,
                LastName = apiModel.LastName,
                Phone = apiModel.Phone,
                City = apiModel.City,
                UserId = apiModel.UserId
            };
        }

        public static UserApiModel ToUserAPIModel(this User bsModel)
        {
            return new UserApiModel()
            {
                FirstName = bsModel.FirstName,
                LastName = bsModel.LastName,
                Phone = bsModel.Phone,
                City = bsModel.City,
                UserId = bsModel.UserId
            };
        }
    }
}
