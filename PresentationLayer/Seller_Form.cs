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
    public partial class Seller_Form : Form
    {
        private EmployeeBL employeeBL;

        public Seller_Form()
        {
            InitializeComponent();
            btnClose.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Icon\button_close.jpg");
            btnClose.BackgroundImageLayout = ImageLayout.Zoom;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            employeeBL = new EmployeeBL();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void populate()
        {
            SellersDGV.DataSource = employeeBL.GetAll();
        }

        private void Seller_From_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeDTO employee = new EmployeeDTO
                {
                    SellerId = int.Parse(txtSellerID.Text),
                    SellerName = txtSellerName.Text,
                    SellerAge = int.Parse(txtSellerAge.Text),
                    SellerMobileNo = txtSellerMobileNo.Text,
                    SellerPassword = txtSellerPassword.Text
                };
                employeeBL.Add(employee);
                MessageBox.Show("Seller Added Successfully");
                populate();
                txtSellerID.Text = "";
                txtSellerName.Text = "";
                txtSellerAge.Text = "";
                txtSellerMobileNo.Text = "";
                txtSellerPassword.Text = "";
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
                if (string.IsNullOrEmpty(txtSellerID.Text) || string.IsNullOrEmpty(txtSellerName.Text) ||
                    string.IsNullOrEmpty(txtSellerAge.Text) || string.IsNullOrEmpty(txtSellerMobileNo.Text) ||
                    string.IsNullOrEmpty(txtSellerPassword.Text))
                {
                    MessageBox.Show("Missing Information");
                    return;
                }
                EmployeeDTO employee = new EmployeeDTO
                {
                    SellerId = int.Parse(txtSellerID.Text),
                    SellerName = txtSellerName.Text,
                    SellerAge = int.Parse(txtSellerAge.Text),
                    SellerMobileNo = txtSellerMobileNo.Text,
                    SellerPassword = txtSellerPassword.Text
                };
                employeeBL.Update(employee);
                MessageBox.Show("Seller Successfully Updated");
                populate();
                txtSellerID.Text = "";
                txtSellerName.Text = "";
                txtSellerAge.Text = "";
                txtSellerMobileNo.Text = "";
                txtSellerPassword.Text = "";
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
                if (string.IsNullOrEmpty(txtSellerID.Text))
                {
                    MessageBox.Show("Select the Seller to Delete");
                    return;
                }
                employeeBL.Delete(int.Parse(txtSellerID.Text));
                MessageBox.Show("Seller Deleted Successfully");
                populate();
                txtSellerID.Text = "";
                txtSellerName.Text = "";
                txtSellerAge.Text = "";
                txtSellerMobileNo.Text = "";
                txtSellerPassword.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SellersDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtSellerID.Text = SellersDGV.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "";
                txtSellerName.Text = SellersDGV.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";
                txtSellerAge.Text = SellersDGV.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "";
                txtSellerMobileNo.Text = SellersDGV.Rows[e.RowIndex].Cells[3].Value?.ToString() ?? "";
                txtSellerPassword.Text = SellersDGV.Rows[e.RowIndex].Cells[4].Value?.ToString() ?? "";
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