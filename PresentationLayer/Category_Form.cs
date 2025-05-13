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
    public partial class Category_Form : Form
    {
        private CategoryBL categoryBL;

        public Category_Form()
        {
            InitializeComponent();
            btnClose.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Icon\button_close.jpg");
            btnClose.BackgroundImageLayout = ImageLayout.Zoom;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            categoryBL = new CategoryBL();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void populate()
        {
            CategoriesDGV.DataSource = categoryBL.GetAll();
            CategoriesDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            CategoriesDGV.MultiSelect = false;
        }

        private void Category_From_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                CategoryDTO category = new CategoryDTO
                {
                    Id = int.Parse(txtCategoryID.Text),
                    CatName = txtCategoryName.Text,
                    CatDesc = txtCategoryDescription.Text
                };
                categoryBL.Add(category);
                MessageBox.Show("Thêm danh mục thành công");
                populate();
                txtCategoryID.Text = "";
                txtCategoryName.Text = "";
                txtCategoryDescription.Text = "";
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
                if (string.IsNullOrEmpty(txtCategoryID.Text) || string.IsNullOrEmpty(txtCategoryName.Text) || string.IsNullOrEmpty(txtCategoryDescription.Text))
                {
                    MessageBox.Show("Không tồn tại danh mục nào để sửa");
                    return;
                }
                CategoryDTO category = new CategoryDTO
                {
                    Id = int.Parse(txtCategoryID.Text),
                    CatName = txtCategoryName.Text,
                    CatDesc = txtCategoryDescription.Text
                };
                categoryBL.Update(category);
                MessageBox.Show("Cập nhật danh mục thành công");
                populate();
                txtCategoryID.Text = "";
                txtCategoryName.Text = "";
                txtCategoryDescription.Text = "";
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
                if (string.IsNullOrEmpty(txtCategoryID.Text))
                {
                    MessageBox.Show("Hãy chọn danh mục muốn xóa");
                    return;
                }
                categoryBL.Delete(int.Parse(txtCategoryID.Text));
                MessageBox.Show("Xóa danh mục thành công");
                populate();
                txtCategoryID.Text = "";
                txtCategoryName.Text = "";
                txtCategoryDescription.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CategoriesDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtCategoryID.Text = CategoriesDGV.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "";
                txtCategoryName.Text = CategoriesDGV.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";
                txtCategoryDescription.Text = CategoriesDGV.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "";
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            Product_Form prod = new Product_Form();
            prod.Show();
            this.Hide();
        }

        private void btnSellers_Click(object sender, EventArgs e)
        {
            Seller_Form sell = new Seller_Form();
            sell.Show();
            this.Hide();
        }

        private void btnSelling_Click(object sender, EventArgs e)
        {
            Selling_Form sell = new Selling_Form();
            sell.Show();
            this.Hide();
        }

        private void CategoriesDGV_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtCategoryID.Text = CategoriesDGV.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "";
                txtCategoryName.Text = CategoriesDGV.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";
                txtCategoryDescription.Text = CategoriesDGV.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "";
            }
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
