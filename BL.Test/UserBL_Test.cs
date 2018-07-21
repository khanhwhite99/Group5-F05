using System;
using Xunit;
using DAL;
using Persistence;

namespace BL.Test
{
    public class UserBL_Test
    {
        [Theory]
        [InlineData("khanhdrag9")]
        [InlineData("tungf05")]
        public void GetUserByUserName_Test_Pass(string username)
        {
            UserBL userbl = new UserBL();
            Assert.NotNull(userbl.GetUserByUserName(username));
        }
        [Theory]
        [InlineData("absd")]
        [InlineData("")]
        public void GetUserByUserName_Test_Fail(string username)
        {
            UserBL userbl = new UserBL();
            Assert.Null(userbl.GetUserByUserName(username));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetUserByID_Test_Pass(int userid)
        {
            UserBL userbl = new UserBL();
            Assert.NotNull(userbl.GetUserByID(userid));
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(100000)]
        public void GetUserByID_Test_Fail(int userid)
        {
            UserBL userbl = new UserBL();
            Assert.Null(userbl.GetUserByID(userid));
        }

        [Theory]
        [InlineData("khanhdrag9", "123456")]
        public void CheckExistUserAndPass_Pass(string user, string pass)
        {
            UserBL userbl = new UserBL();
            Assert.True(userbl.CheckExistUserAndPass(user, pass));
        }
        [Theory]
        [InlineData("khanhdraf8", "123456")]
        [InlineData("khanhdrag9", "1a5")]
        [InlineData("d dad ", "123456")]
        [InlineData("khanhdrag9", "123 456")]
        public void CheckExistUserAndPass_Fail(string user, string pass)
        {
            UserBL userbl = new UserBL();
            Assert.False(userbl.CheckExistUserAndPass(user, pass));
        }

        [Theory]
        [InlineData(1)]
        public void GetApplicationBoughtByUserID_Pass(int id)
        {
            UserBL userbl = new UserBL();
            Assert.NotEmpty(userbl.GetApplicationBoughtByUserID(id));
        }
        [Theory]
        [InlineData(-5)]
        [InlineData(100000)]
        public void GetApplicationBoughtByUserID_Fail(int id)
        {
            UserBL userbl = new UserBL();
            Assert.Empty(userbl.GetApplicationBoughtByUserID(id));
        }
    }
}