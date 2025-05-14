using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferObject;
using BusinessLayer;
using System.Drawing.Printing;
using System.Data.SqlClient;
using QRCoder;
using System.Drawing.Imaging;
namespace PresentationLayer
{
    public partial class Selling_Form : Form
    {
        private ProductBL productBL;
        private int Grdtotal = 0, n = 0;

        public Selling_Form()
        {
            InitializeComponent();
            btnClose.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Icon\button_close.jpg");
            btnClose.BackgroundImageLayout = ImageLayout.Zoom;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            productBL = new ProductBL();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SMMSD.mdf;Integrated Security=True");

        private void populate()
        {
            ProdDGV.DataSource = productBL.GetProducts();
        }

        private void populatebills()
        {
            Con.Open();
            string query = "SELECT * FROM BillsTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BillsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void FillCategory()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("SELECT CatName FROM CategoriesTbl", Con);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(string));
            dt.Load(rdr);
            cbSelectCategory.ValueMember = "CatName";
            cbSelectCategory.DataSource = dt;
            Con.Close();
        }

        private void Selling_Form_Load(object sender, EventArgs e) // Đổi từ Selling_From_Load
        {
            populate();
            populatebills();
            FillCategory();
            if (!string.IsNullOrEmpty(Login.Sellername))
                lblSellerName.Text = Login.Sellername;
            else if (Login.CurrentRole == "Admin")
                lblSellerName.Text = "Admin"; // Hoặc lấy tên Admin từ hệ thống
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductName.Text) || string.IsNullOrEmpty(txtProductQuantity.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            int qty = Convert.ToInt32(txtProductQuantity.Text);
            int prodId = Convert.ToInt32(txtProductID.Text); // Giả sử có txtProductID
            int availableQty = productBL.GetProductQuantity(prodId);
            if (qty > availableQty)
            {
                MessageBox.Show("Số lượng tồn kho không đủ");
                return;
            }

            int total = Convert.ToInt32(txtProductPrice.Text) * qty;
            DataGridViewRow newRow = new DataGridViewRow();
            newRow.CreateCells(OrdersDGV);
            newRow.Cells[0].Value = n + 1;
            newRow.Cells[1].Value = txtProductName.Text;
            newRow.Cells[2].Value = qty;
            newRow.Cells[3].Value = txtProductPrice.Text;
            newRow.Cells[4].Value = total;
            OrdersDGV.Rows.Add(newRow);
            n++;
            Grdtotal += total;
            lblAmount.Text = Grdtotal.ToString();

            // Cập nhật tồn kho
            productBL.UpdateQuantity(prodId, qty);
            populate();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            lblDate.Text = DateTime.Today.ToString("dd/MM/yyyy"); // Giữ hiển thị cho giao diện
            billDate = DateTime.Today; // Thêm biến để lưu datetime chuẩn
        }

        private DateTime billDate; // Khai báo biến toàn cục

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBillID.Text))
            {
                MessageBox.Show("Thiếu bill ID");
                return;
            }

            try
            {
                Con.Open();
                string query = $"INSERT INTO BillsTbl VALUES(@BillId, @SellerName, @BillDate, @TotalAmount)";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@BillId", int.Parse(txtBillID.Text));
                cmd.Parameters.AddWithValue("@SellerName", lblSellerName.Text);
                cmd.Parameters.AddWithValue("@BillDate", billDate); // Sử dụng datetime chuẩn
                cmd.Parameters.AddWithValue("@TotalAmount", Convert.ToInt32(lblAmount.Text));
                cmd.ExecuteNonQuery();

                // Lưu chi tiết đơn hàng
                OrderDetailBL orderDetailBL = new OrderDetailBL();
                int orderId = new Random().Next(1000, 9999);
                foreach (DataGridViewRow row in OrdersDGV.Rows)
                {
                    if (row.IsNewRow) continue;
                    OrderDetailDTO orderDetail = new OrderDetailDTO
                    {
                        OrderId = orderId++,
                        BillId = int.Parse(txtBillID.Text),
                        ProdId = productBL.GetProductIdByName(row.Cells[1].Value.ToString()),
                        Quantity = Convert.ToInt32(row.Cells[2].Value)
                    };
                    orderDetailBL.Add(orderDetail);
                }

                MessageBox.Show("Thêm thành công");
                Con.Close();
                populatebills();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Con.Close();
            }
        }


        private void cbSelectCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Con.Open();
            string query = "SELECT ProdId, ProdName, ProdQty, ProdPrice FROM ProductsTbl WHERE ProdCat = @Category";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            sda.SelectCommand.Parameters.AddWithValue("@Category", cbSelectCategory.SelectedValue.ToString());
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProdDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void ProdDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtProductID.Text = ProdDGV.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? ""; // ProdId
                txtProductName.Text = ProdDGV.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? ""; // ProdName
                txtProductQuantity.Text = "1"; // Mặc định là 1, có thể chỉnh
                txtProductPrice.Text = ProdDGV.Rows[e.RowIndex].Cells[3].Value?.ToString() ?? ""; // ProdPrice
            }
        }


        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

                e.Graphics.DrawString("K.M.S. SuperMarket", new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Red, new Point(230));
                e.Graphics.DrawString($"Bill ID: {BillsDGV.SelectedRows[0].Cells[0].Value}", new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 70));
                e.Graphics.DrawString($"Seller Name: {BillsDGV.SelectedRows[0].Cells[1].Value}", new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 100));
                e.Graphics.DrawString($"Bill Date: {BillsDGV.SelectedRows[0].Cells[2].Value}", new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 130));
                e.Graphics.DrawString($"Total Amount: {BillsDGV.SelectedRows[0].Cells[3].Value}", new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 160));

                // Tạo mã QR
                string qrContent = $"BillID:{BillsDGV.SelectedRows[0].Cells[0].Value},Amount:{BillsDGV.SelectedRows[0].Cells[3].Value}";
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);
                e.Graphics.DrawImage(qrCodeImage, new Point(100, 200));

                e.Graphics.DrawString("K.M.G.S.S.", new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Red, new Point(230, 300));
            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (PrintPreviewDialog.ShowDialog() == DialogResult.OK)
            {
                PrintDocument.Print();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void btnSellers_Click(object sender, EventArgs e)
        {
            Seller_Form sell = new Seller_Form();
            sell.Show();
            this.Hide();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            Category_Form cat = new Category_Form();
            cat.Show();
            this.Hide();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            Product_Form prod = new Product_Form();
            prod.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void btnTopProducts_Click(object sender, EventArgs e)
        {
            try
            {
                TopProducts_Form topProductsForm = new TopProducts_Form();
                topProductsForm.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form sản phẩm bán chạy: " + ex.Message);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                RevenueReport_Form revenueReport_Form = new RevenueReport_Form();
                revenueReport_Form.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form sản phẩm bán chạy: " + ex.Message);
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerManagement_Form customerManagement_Form = new CustomerManagement_Form();
                customerManagement_Form.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi quay lại form khách hàng: " + ex.Message);
            }
        }



        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductName.Text) || string.IsNullOrEmpty(txtProductQuantity.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm và số lượng");
                return;
            }

            int qty = Convert.ToInt32(txtProductQuantity.Text);
            int prodId = Convert.ToInt32(ProdDGV.SelectedRows[0].Cells[0].Value); // Lấy ProdId từ ProdDGV
            int availableQty = productBL.GetProductQuantity(prodId);
            if (qty > availableQty)
            {
                MessageBox.Show("Số lượng tồn kho không đủ");
                return;
            }

            int price = Convert.ToInt32(ProdDGV.SelectedRows[0].Cells[3].Value); // Lấy giá từ ProdDGV
            int total = price * qty;

            DataGridViewRow newRow = new DataGridViewRow();
            newRow.CreateCells(OrdersDGV);
            newRow.Cells[0].Value = n + 1;
            newRow.Cells[1].Value = txtProductName.Text;
            newRow.Cells[2].Value = qty;
            newRow.Cells[3].Value = price; // Dùng giá từ ProdDGV
            newRow.Cells[4].Value = total;
            OrdersDGV.Rows.Add(newRow);
            n++;
            Grdtotal += total;
            lblAmount.Text = Grdtotal.ToString();

            // Cập nhật tồn kho
            productBL.UpdateQuantity(prodId, qty);
            populate();
        }
    }
}
