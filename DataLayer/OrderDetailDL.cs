using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TransferObject;

namespace DataLayer
{
    public class OrderDetailDL : DataProvider
    {
        public void Add(OrderDetailDTO orderDetail)
        {
            string sql = $"INSERT INTO OrderDetailsTbl VALUES({orderDetail.OrderId}, {orderDetail.BillId}, {orderDetail.ProdId}, {orderDetail.Quantity})";
            ExecuteNonQuery(sql);
        }

        public DataTable GetTopProducts(int month, int year)
        {
            string sql = $"SELECT p.ProdName, SUM(od.Quantity) as TotalQuantity " +
                        $"FROM OrderDetailsTbl od " +
                        $"JOIN ProductsTbl p ON od.ProdId = p.ProdId " +
                        $"JOIN BillsTbl b ON od.BillId = b.BillId " +
                        $"WHERE MONTH(CAST(b.BillDate AS DATE)) = {month} AND YEAR(CAST(b.BillDate AS DATE)) = {year} " +
                        $"GROUP BY p.ProdName " +
                        $"ORDER BY TotalQuantity DESC";
            return ExecuteQuery(sql);
        }
    }
}