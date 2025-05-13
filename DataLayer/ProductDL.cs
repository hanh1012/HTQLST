using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TransferObject;

namespace DataLayer
{
    public class ProductDL : DataProvider
    {
        public void Add(ProductDTO product)
        {
            string sql = "INSERT INTO ProductsTbl VALUES(" + product.ProdId + ",'" + product.ProdName + "'," + product.ProdQty + ",'" + product.ProdPrice + "','" + product.ProdCat + "')";
            ExecuteNonQuery(sql);
        }

        public void Update(ProductDTO product)
        {
            string sql = "UPDATE ProductsTbl SET ProdName='" + product.ProdName + "', ProdQty=" + product.ProdQty + ", ProdPrice='" + product.ProdPrice + "', ProdCat='" + product.ProdCat + "' WHERE ProdId=" + product.ProdId;
            ExecuteNonQuery(sql);
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM ProductsTbl WHERE ProdId=" + id;
            ExecuteNonQuery(sql);
        }

        public DataTable GetAll()
        {
            string sql = "SELECT * FROM ProductsTbl";
            return ExecuteQuery(sql);
        }

        public DataTable GetByCategory(string category)
        {
            string sql = "SELECT * FROM ProductsTbl WHERE ProdCat='" + category + "'";
            return ExecuteQuery(sql);
        }

        public DataTable GetCategories()
        {
            string sql = "SELECT CatName FROM CategoriesTbl";
            return ExecuteQuery(sql);
        }

        public DataTable GetProducts()
        {
             string sql = "SELECT ProdName, ProdQty FROM ProductsTbl";
             return ExecuteQuery(sql);
        }
    
}
}