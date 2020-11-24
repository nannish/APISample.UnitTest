using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APISample.APIModels;
using APISample.DbEntities;
using APISample.Convertors;

namespace APISample.Services
{
    public class UserService : IUserService
    {
        private ISampleDbContext _dbContext;
        public UserService(ISampleDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public UserApiModel AddUser(UserApiModel model)
        {
            var bsModel = this._dbContext.Users.SingleOrDefault(c => c.UserId == model.UserId);
            if (bsModel == null)
            {
                bsModel = model.ToUserBSModel();
                _dbContext.Add(bsModel);
                _dbContext.SaveChanges();
                return bsModel.ToUserAPIModel();
            }
            return null;
        }

        public UserApiModel EditUser(UserApiModel model)
        {
            var bsModel = this._dbContext.Users.SingleOrDefault(c => c.UserId == model.UserId);
            if (bsModel != null)
            {
                bsModel.FirstName = model.FirstName;
                bsModel.LastName = model.LastName;
                bsModel.City = model.City;
                bsModel.Phone = model.Phone;
                _dbContext.SaveChanges();
                return bsModel.ToUserAPIModel();
            }
            return null;
        }


        public bool DeleteUser(long id)
        {
            var bsModel = this._dbContext.Users.SingleOrDefault(c => c.UserId == id);
            if (bsModel != null)
            {
                bsModel.Deleted = 1;
                _dbContext.SaveChanges();
            }
            return true;
        }

        public IList<UserApiModel> GetAllUsers()
        {
            IList<UserApiModel> users = new List<UserApiModel>();
            var data = this._dbContext.Users.Where(n => n.Deleted == 0).OrderBy(n => n.LastName);
            if (data != null)
            {
                foreach (User model in data)
                {
                    users.Add(model.ToUserAPIModel());
                }
            }
            return users;
        }
    }
}
