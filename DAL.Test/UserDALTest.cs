using System;
using Xunit;

namespace DAL.Test
{
    public class UserDALTest : UserDAL
    {
        [Theory]
        [InlineData("khanhdrag9", "123456")]
        public void CheckUserAndPass_Test_True(string user, string pass)
        {
            Assert.True(CheckUserAndPass(user, pass));
        }

        [Theory]
        [InlineData("khanh123", "123456")]
        [InlineData("khanhdrag9", "1234")]
        [InlineData("khanh1234", "12")]
        public void CheckUserAndPass_Test_False(string user, string pass)
        {
            Assert.False(CheckUserAndPass(user, pass));
        }
        [Fact]
        public void GetUserById_Test()
        {
            //check True
            int ID = 1;
            Assert.NotNull(GetUserById(ID));
            //check False
            ID = 100;
            Assert.Null(GetUserById(ID));
            ID = -1;
            Assert.Null(GetUserById(ID));
        }

        [Fact]
        public void GetUserByUsername_Test()
        {
            //check True
            string username = "khanhdrag9";
            Assert.NotNull(GetUserByUsername(username));
            //check false
            username = "khanh123";
            Assert.Null(GetUserByUsername(username));
        }
        
    }
}