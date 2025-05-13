using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TransferObject;

namespace DataLayer
{
    public class CustomerDL : DataProvider
    {
        public void Add(CustomerDTO customer)
        {
            string sql = $"INSERT INTO CustomersTbl VALUES({customer.CustomerId}, '{customer.CustomerName}', '{customer.CustomerEmail}', '{customer.CustomerPhone}', '{customer.CustomerPassword}', {customer.LoyaltyPoints})";
            ExecuteNonQuery(sql);
        }

        public void Update(CustomerDTO customer)
        {
            string sql = $"UPDATE CustomersTbl SET CustomerName='{customer.CustomerName}', CustomerEmail='{customer.CustomerEmail}', CustomerPhone='{customer.CustomerPhone}', CustomerPassword='{customer.CustomerPassword}', LoyaltyPoints={customer.LoyaltyPoints} WHERE CustomerId={customer.CustomerId}";
            ExecuteNonQuery(sql);
        }

        public void Delete(int id)
        {
            string sql = $"DELETE FROM CustomersTbl WHERE CustomerId={id}";
            ExecuteNonQuery(sql);
        }

        public DataTable GetAll()
        {
            string sql = "SELECT * FROM CustomersTbl";
            return ExecuteQuery(sql);
        }

        public bool Login(CustomerDTO customer)
        {
            string sql = $"SELECT COUNT(*) FROM CustomersTbl WHERE CustomerEmail='{customer.CustomerEmail}' AND CustomerPassword='{customer.CustomerPassword}'";
            return (int)MyExecuteScalar(sql, CommandType.Text) > 0;
        }
    }
}
