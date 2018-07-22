using Xunit;
using MySql.Data.MySqlClient;
using DAL;

namespace DAL.Test
{
    public class PaymentDALTest
    {
        [Fact]
        void GetPaymentById_Test()
        {
            //Pass
            Assert.NotEmpty(PaymentDAL.GetPaymentByUserId(1));

            //Fail
            Assert.Empty(PaymentDAL.GetPaymentByUserId(1000000));
            Assert.Empty(PaymentDAL.GetPaymentByUserId(-5));
        } 
    }
}