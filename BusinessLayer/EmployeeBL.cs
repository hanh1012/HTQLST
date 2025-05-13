using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TransferObject;
using DataLayer;

namespace BusinessLayer
{
    public class EmployeeBL
    {
        private EmployeeDL employeeDL;

        public EmployeeBL()
        {
            employeeDL = new EmployeeDL();
        }

        public void Add(EmployeeDTO employee)
        {
            if (string.IsNullOrEmpty(employee.SellerName) || employee.SellerAge <= 0 ||
                string.IsNullOrEmpty(employee.SellerMobileNo) || string.IsNullOrEmpty(employee.SellerPassword))
                throw new Exception("Thông tin nhân viên không hợp lệ");
            employeeDL.Add(employee);
        }

        public void Update(EmployeeDTO employee)
        {
            if (string.IsNullOrEmpty(employee.SellerName) || employee.SellerAge <= 0 ||
                string.IsNullOrEmpty(employee.SellerMobileNo) || string.IsNullOrEmpty(employee.SellerPassword))
                throw new Exception("Thông tin nhân viên không hợp lệ");
            employeeDL.Update(employee);
        }

        public void Delete(int id)
        {
            employeeDL.Delete(id);
        }

        public DataTable GetAll()
        {
            return employeeDL.GetAll();
        }
    }
}