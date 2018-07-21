using System;
using BL;
using DAL;
using Persistence;
using Xunit;

namespace BL.Test
{
    public class BillBL_Test
    {
        
        [Fact]
        public void TestCreateBillBL()
        {
            //Pass
            BillBL billBL = new BillBL();
            Bill bill = new Bill()
            {
                App = ApplicationDAL.GetApplicationById(6),
                User = UserDAL.GetUserById(2),
                UnitPrice = ApplicationDAL.GetApplicationById(6).Price
            };
            Assert.True(billBL.CreateBill(bill));

            //fail
            //this app not exist!
            Bill bill2 = new Bill()
            {
                App = ApplicationDAL.GetApplicationById(10000),
                User = UserDAL.GetUserById(2),
                UnitPrice = ApplicationDAL.GetApplicationById(1).Price
            };
            Assert.False(billBL.CreateBill(bill2));

            //this user not exists!
            bill = new Bill()
            {
                App = ApplicationDAL.GetApplicationById(8),
                User = UserDAL.GetUserById(200000),
                UnitPrice = ApplicationDAL.GetApplicationById(8).Price
            };
            Assert.False(billBL.CreateBill(bill));

            //this user has bought this app!
            bill = new Bill()
            {
                App = ApplicationDAL.GetApplicationById(1),
                User = UserDAL.GetUserById(1),
                UnitPrice = ApplicationDAL.GetApplicationById(1).Price
            };
            Assert.False(billBL.CreateBill(bill));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestGetListBillByUserID_Pass(int id)
        {
            BillBL billbl = new BillBL();
            //pass
            Assert.NotEmpty(billbl.GetListBillByUserID(id));
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(1000000)]
        public void TestGetListBillByUserID_Fail(int id)
        {
            BillBL billbl = new BillBL();
            Assert.Empty(billbl.GetListBillByUserID(id));
        }
    }
}
