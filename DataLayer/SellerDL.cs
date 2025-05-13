using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using System.Data;
using System.Data.SqlClient;
namespace DataLayer
{
    public class SellerDL : DataProvider
    {
        public bool Login(SellerDTO seller)
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM SellersTbl WHERE SellerName = '" + seller.SellerName + "' AND SellerPassword = '" + seller.SellerPassword + "'";
                return (int)MyExecuteScalar(sql, CommandType.Text) > 0;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}