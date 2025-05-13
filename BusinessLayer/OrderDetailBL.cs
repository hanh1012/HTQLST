using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TransferObject;
using DataLayer;
using System.Data.SqlClient;

namespace BusinessLayer
{
    public class OrderDetailBL
    {
        private OrderDetailDL orderDetailDL;

        public OrderDetailBL()
        {
            orderDetailDL = new OrderDetailDL();
        }


        public void Add(OrderDetailDTO orderDetail)
        {
            if (orderDetail.ProdId <= 0 || orderDetail.Quantity <= 0)
                throw new Exception("Thông tin chi tiết đơn hàng không hợp lệ");
            orderDetailDL.Add(orderDetail);
        }

        public DataTable GetTopProducts(int month, int year)
        {
            DataTable dt = new DataTable();
            using (SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SMMSD.mdf;Integrated Security=True"))
            {
                Con.Open();
                string query = @"
            SELECT p.ProdName, SUM(od.Quantity) as TotalSold
            FROM OrderDetailsTbl od
            JOIN ProductsTbl p ON od.ProdId = p.ProdId
            JOIN BillsTbl b ON od.BillId = b.BillId
            WHERE MONTH(CAST(b.BillDate AS DATE)) = @Month AND YEAR(CAST(b.BillDate AS DATE)) = @Year
            GROUP BY p.ProdName
            ORDER BY TotalSold DESC";
                SqlDataAdapter sda = new SqlDataAdapter(query, Con);
                sda.SelectCommand.Parameters.AddWithValue("@Month", month);
                sda.SelectCommand.Parameters.AddWithValue("@Year", year);
                sda.Fill(dt);
                Con.Close();
            }
            return dt;
        }

    }
}