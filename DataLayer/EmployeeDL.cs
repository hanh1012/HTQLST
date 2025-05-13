using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TransferObject;

namespace DataLayer
{
    public class EmployeeDL : DataProvider
    {
        public void Add(EmployeeDTO employee)
        {
            string sql = "INSERT INTO SellersTbl VALUES(" + employee.SellerId + ",'" + employee.SellerName + "'," +
                        employee.SellerAge + ",'" + employee.SellerMobileNo + "','" + employee.SellerPassword + "')";
            ExecuteNonQuery(sql);
        }

        public void Update(EmployeeDTO employee)
        {
            string sql = "UPDATE SellersTbl SET SellerName='" + employee.SellerName + "', SellerAge=" + employee.SellerAge +
                        ", SellerMobileNo='" + employee.SellerMobileNo + "', SellerPassword='" + employee.SellerPassword +
                        "' WHERE SellerId=" + employee.SellerId;
            ExecuteNonQuery(sql);
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM SellersTbl WHERE SellerId=" + id;
            ExecuteNonQuery(sql);
        }

        public DataTable GetAll()
        {
            string sql = "SELECT * FROM SellersTbl";
            return ExecuteQuery(sql);
        }
    }
}