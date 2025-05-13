using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TransferObject;

namespace DataLayer
{
    public class BillDL : DataProvider
    {
        public void Add(BillDTO bill)
        {
            string sql = "INSERT INTO BillsTbl VALUES(" + bill.BillId + ",'" + bill.SellerName + "','" + bill.BillDate + "'," + bill.TotalAmount + ")";
            ExecuteNonQuery(sql);
        }

        public DataTable GetAll()
        {
            string sql = "SELECT * FROM BillsTbl";
            return ExecuteQuery(sql);
        }
    }
}
