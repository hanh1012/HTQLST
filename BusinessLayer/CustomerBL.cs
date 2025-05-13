using System;
using System.Data;
using System.Data.SqlClient;
using TransferObject;
using DataLayer;

namespace BusinessLayer
{
    public class CustomerBL
    {
        private CustomerDL customerDL;
        private DataProvider dataProvider;

        public CustomerBL()
        {
            customerDL = new CustomerDL();
            dataProvider = new DataProvider();
        }

        public bool Login(CustomerDTO customer)
        {
            return customerDL.Login(customer);
        }

        public void Add(CustomerDTO customer)
        {
            if (string.IsNullOrEmpty(customer.CustomerName) || string.IsNullOrEmpty(customer.CustomerEmail) ||
                string.IsNullOrEmpty(customer.CustomerPhone) || string.IsNullOrEmpty(customer.CustomerPassword))
                throw new Exception("Thông tin khách hàng không hợp lệ");
            customerDL.Add(customer);
        }

        public void Update(CustomerDTO customer)
        {
            if (string.IsNullOrEmpty(customer.CustomerName) || string.IsNullOrEmpty(customer.CustomerEmail) ||
                string.IsNullOrEmpty(customer.CustomerPhone) || string.IsNullOrEmpty(customer.CustomerPassword))
                throw new Exception("Thông tin khách hàng không hợp lệ");
            customerDL.Update(customer);
        }

        public void Delete(int id)
        {
            customerDL.Delete(id);
        }

        public DataTable GetAll()
        {
            return customerDL.GetAll();
        }

        public void AddPoints(int customerId, int points)
        {
            string sql = "UPDATE CustomersTbl SET LoyaltyPoints = LoyaltyPoints + @Points WHERE CustomerId = @CustomerId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Points", points),
                new SqlParameter("@CustomerId", customerId)
            };
            dataProvider.ExecuteNonQuery(sql, CommandType.Text, parameters);
        }

        public int GetPoints(int customerId)
        {
            string sql = "SELECT LoyaltyPoints FROM CustomersTbl WHERE CustomerId = @CustomerId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CustomerId", customerId)
            };
            object result = dataProvider.MyExecuteScalar(sql, CommandType.Text, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public void UsePoints(int customerId, int points)
        {
            string sql = "UPDATE CustomersTbl SET LoyaltyPoints = LoyaltyPoints - @Points WHERE CustomerId = @CustomerId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Points", points),
                new SqlParameter("@CustomerId", customerId)
            };
            dataProvider.ExecuteNonQuery(sql, CommandType.Text, parameters);
        }

        public int GetCustomerIdByEmail(string email)
        {
            string sql = "SELECT CustomerId FROM CustomersTbl WHERE CustomerEmail = @Email";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Email", email)
            };
            object result = dataProvider.MyExecuteScalar(sql, CommandType.Text, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }
    }
}