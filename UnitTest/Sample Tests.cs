using APISample.Convertors;
using APISample.DbEntities;
using APISample.Services;
using Moq;

using NUnit.Framework;
using System.Collections.Generic;
using UnitTest;

namespace Tests
{
    public class Tests
    {
        ISampleDbContext dbContext;
        User user;
        List<User> users;

        [SetUp]
        public void Setup()
        {
            users = new List<User>();
            users.Add(new User
            {
                UserId = 1,
                FirstName = "Venkatesh"
            });
            var myDbMoq = new Mock<ISampleDbContext>();
            myDbMoq.Setup(p => p.Users).Returns(SampleDbContextMock.GetQueryableMockDbSet(users));
            dbContext = myDbMoq.Object;
        }
        [Test]
        public void AddUser()
        {
            user = new User() { UserId = 0, FirstName = "Venkatesh" };
            var serice = new UserService(dbContext);
            var data = serice.AddUser(user.ToUserAPIModel());
            Assert.NotNull(user);
        }
        [Test]
        public void EditUser()
        {
            user = new User() { UserId = 1, FirstName = "Venkatesh" };
            var service = new UserService(dbContext);
            var data = service.EditUser(user.ToUserAPIModel());
            Assert.NotNull(user);
        }

       [Test]
        public void UserGetValidate()
        {
            user = new User() { UserId = 1, FirstName = "Venkatesh" };
            var service = new UserService(dbContext);
            var data = service.GetAllUsers();
            Assert.AreEqual(user.UserId, data[0].UserId);
            Assert.AreEqual(user.FirstName, data[0].FirstName);
        }

        [Test]
        public void UserDeleteValidate()
        {
            var service = new UserService(dbContext);
            var data = service.DeleteUser(1);
            Assert.AreEqual(data, true);
        }



    }
}