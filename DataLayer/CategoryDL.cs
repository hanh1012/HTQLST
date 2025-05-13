using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TransferObject;

namespace DataLayer
{
    public class CategoryDL : DataProvider
    {
        public void Add(CategoryDTO category)
        {
            string sql = "INSERT INTO CategoriesTbl VALUES(" + category.Id + ",'" + category.CatName + "','" + category.CatDesc + "')";
            ExecuteNonQuery(sql);
        }

        public void Update(CategoryDTO category)
        {
            string sql = "UPDATE CategoriesTbl SET CatName='" + category.CatName + "', CatDesc='" + category.CatDesc + "' WHERE Id=" + category.Id;
            ExecuteNonQuery(sql);
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM CategoriesTbl WHERE Id=" + id;
            ExecuteNonQuery(sql);
        }

        public DataTable GetAll()
        {
            string sql = "SELECT * FROM CategoriesTbl";
            return ExecuteQuery(sql);
        }
    }
}