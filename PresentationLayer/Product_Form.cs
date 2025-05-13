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

namespace PresentationLayer
{
    public partial class Product_Form : Form
    {
        private ProductBL productBL;

        public Product_Form()
        {
            InitializeComponent();
            btnClose.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Icon\button_close.jpg");
            btnClose.BackgroundImageLayout = ImageLayout.Zoom;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            productBL = new ProductBL();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void populate()
        {
            ProductsDGV.DataSource = productBL.GetAll();
        }

        private void FillCategory()
        {
            DataTable dt = productBL.GetCategories();
            cbSearchCategory.ValueMember = "CatName";
            cbSearchCategory.DataSource = dt;
            cbSelectCategory.ValueMember = "CatName";
            cbSelectCategory.DataSource = dt;
        }

        private void Product_From_Load(object sender, EventArgs e)
        {
            FillCategory();
            populate();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ProductDTO product = new ProductDTO
                {
                    ProdId = int.Parse(txtProductID.Text),
                    ProdName = txtProductName.Text,
                    ProdQty = int.Parse(txtProductQuantity.Text),
                    ProdPrice = txtProductPrice.Text,
                    ProdCat = cbSelectCategory.SelectedValue.ToString()
                };
                productBL.Add(product);
                MessageBox.Show("Thêm sản phẩm thành công!");
                populate();
                txtProductID.Text = "";
                txtProductName.Text = "";
                txtProductQuantity.Text = "";
                txtProductPrice.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProductID.Text) || string.IsNullOrEmpty(txtProductName.Text) || string.IsNullOrEmpty(txtProductQuantity.Text) || string.IsNullOrEmpty(txtProductPrice.Text))
                {
                    MessageBox.Show("Missing Information");
                    return;
                }
                ProductDTO product = new ProductDTO
                {
                    ProdId = int.Parse(txtProductID.Text),
                    ProdName = txtProductName.Text,
                    ProdQty = int.Parse(txtProductQuantity.Text),
                    ProdPrice = txtProductPrice.Text,
                    ProdCat = cbSelectCategory.SelectedValue.ToString()
                };
                productBL.Update(product);
                MessageBox.Show("Cập nhật sản phẩm thành công!");
                populate();
                txtProductID.Text = "";
                txtProductName.Text = "";
                txtProductQuantity.Text = "";
                txtProductPrice.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProductID.Text))
                {
                    MessageBox.Show("Select the Product to Delete");
                    return;
                }
                productBL.Delete(int.Parse(txtProductID.Text));
                MessageBox.Show("Xóa sản phẩm thành công!");
                populate();
                txtProductID.Text = "";
                txtProductName.Text = "";
                txtProductQuantity.Text = "";
                txtProductPrice.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbSearchCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ProductsDGV.DataSource = productBL.GetByCategory(cbSearchCategory.SelectedValue.ToString());
        }

        private void ProductsDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = ProductsDGV.Rows[e.RowIndex];
                txtProductID.Text = row.Cells[0].Value.ToString();
                txtProductName.Text = row.Cells[1].Value.ToString();
                txtProductQuantity.Text = row.Cells[2].Value.ToString();
                txtProductPrice.Text = row.Cells[3].Value.ToString();
                cbSelectCategory.SelectedValue = row.Cells[4].Value.ToString();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void btnSelling_Click(object sender, EventArgs e)
        {
            Selling_Form sell = new Selling_Form();
            sell.Show();
            this.Hide();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            Category_Form cat = new Category_Form();
            cat.Show();
            this.Hide();
        }

        private void btnSellers_Click(object sender, EventArgs e)
        {
            Seller_Form sell = new Seller_Form();
            sell.Show();
            this.Hide();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
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
    }
}
