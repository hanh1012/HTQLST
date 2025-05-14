using System;
using System.Data;
using System.Data.SqlClient;
using TransferObject;
using DataLayer;

namespace BusinessLayer
{
    public class BillBL
    {
        private BillDL billDL;
        private DataProvider dataProvider;

        public BillBL()
        {
            billDL = new BillDL();
            dataProvider = new DataProvider();
        }

        public void Add(BillDTO bill)
        {
            if (string.IsNullOrEmpty(bill.SellerName) || string.IsNullOrEmpty(bill.BillDate) || bill.TotalAmount <= 0)
                throw new Exception("Thông tin hóa đơn không hợp lệ");
            billDL.Add(bill);
        }

        public DataTable GetAll()
        {
            return billDL.GetAll();
        }

        public DataTable GetRevenueByMonth(int month, int year)
        {
            string query = @"SELECT BillId, SellerName, CAST(BillDate AS DATE) as BillDate, TotalAmount
                    FROM BillsTbl
                    WHERE MONTH(CAST(BillDate AS DATE)) = @Month AND YEAR(CAST(BillDate AS DATE)) = @Year";
            return new DataProvider().ExecuteQuery(query, CommandType.Text,
                new SqlParameter("@Month", month),
                new SqlParameter("@Year", year));
        }
    }
}