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
    public class ProductBL
    {
        private ProductDL productDL;

        public ProductBL()
        {
            productDL = new ProductDL();
        }

        public DataTable GetProducts()
        {
            DataTable dt = new DataTable();
            using (SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SMMSD.mdf;Integrated Security=True"))
            {
                Con.Open();
                string query = "SELECT ProdId, ProdName, ProdQty, ProdPrice FROM ProductsTbl";
                SqlDataAdapter sda = new SqlDataAdapter(query, Con);
                sda.Fill(dt);
                Con.Close();
            }
            return dt;
        }

        public void Add(ProductDTO product)
        {
            if (string.IsNullOrEmpty(product.ProdName) || product.ProdQty <= 0 || string.IsNullOrEmpty(product.ProdPrice) || string.IsNullOrEmpty(product.ProdCat))
                throw new Exception("Thông tin sản phẩm không hợp lệ");
            productDL.Add(product);
        }

        public void Update(ProductDTO product)
        {
            if (string.IsNullOrEmpty(product.ProdName) || product.ProdQty <= 0 || string.IsNullOrEmpty(product.ProdPrice) || string.IsNullOrEmpty(product.ProdCat))
                throw new Exception("Thông tin sản phẩm không hợp lệ");
            productDL.Update(product);
        }
        public int GetStockQuantity(int prodId)
        {
            int stock = 0;
            using (SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SMMSD.mdf;Integrated Security=True"))
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ProdQty FROM ProductsTbl WHERE ProdId = @ProdId", Con);
                cmd.Parameters.AddWithValue("@ProdId", prodId);
                stock = (int)cmd.ExecuteScalar();
                Con.Close();
            }
            return stock;
        }

        public void UpdateStockQuantity(int prodId, int newQuantity)
        {
            using (SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SMMSD.mdf;Integrated Security=True"))
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE ProductsTbl SET ProdQty = @NewQty WHERE ProdId = @ProdId", Con);
                cmd.Parameters.AddWithValue("@NewQty", newQuantity);
                cmd.Parameters.AddWithValue("@ProdId", prodId);
                cmd.ExecuteNonQuery();
                Con.Close();
            }
        }
        public void Delete(int id)
        {
            productDL.Delete(id);
        }

        public DataTable GetAll()
        {
            return productDL.GetAll();
        }

        public DataTable GetByCategory(string category)
        {
            return productDL.GetByCategory(category);
        }

        public DataTable GetCategories()
        {
            return productDL.GetCategories();
        }

        public void UpdateQuantity(int prodId, int qty)
        {
            string sql = $"UPDATE ProductsTbl SET ProdQty = ProdQty - {qty} WHERE ProdId = {prodId}";
            productDL.ExecuteNonQuery(sql);
        }

        public int GetProductQuantity(int prodId)
        {
            string sql = $"SELECT ProdQty FROM ProductsTbl WHERE ProdId = {prodId}";
            return (int)productDL.MyExecuteScalar(sql, CommandType.Text);
        }

        public int GetProductIdByName(string prodName)
        {
            string sql = $"SELECT ProdId FROM ProductsTbl WHERE ProdName = @ProdName";
            return (int)productDL.MyExecuteScalar(sql, CommandType.Text, new SqlParameter("@ProdName", prodName));
        }
    }
}