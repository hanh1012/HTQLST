using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TransferObject;
using BusinessLayer;

namespace PresentationLayer
{
    public partial class CustomerManagement_Form : Form
    {
        private CustomerBL customerBL;

        public CustomerManagement_Form()
        {
            InitializeComponent();
            customerBL = new CustomerBL();
            btnClose.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Icon\button_close.jpg");
            btnClose.BackgroundImageLayout = ImageLayout.Zoom;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
        }

        private void populate()
        {
            CustomersDGV.DataSource = customerBL.GetAll();
            CustomersDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            CustomersDGV.MultiSelect = false;
        }

        private void CustomerManagement_Form_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerDTO customer = new CustomerDTO
                {
                    CustomerId = int.Parse(txtCustomerID.Text),
                    CustomerName = txtCustomerName.Text,
                    CustomerEmail = txtCustomerEmail.Text,
                    CustomerPhone = txtCustomerPhone.Text,
                    CustomerPassword = txtCustomerPassword.Text,
                    LoyaltyPoints = 0
                };
                customerBL.Add(customer);
                MessageBox.Show("Thêm khách hàng thành công");
                populate();
                ClearFields();
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
                if (string.IsNullOrEmpty(txtCustomerID.Text))
                {
                    MessageBox.Show("Chọn khách hàng để sửa");
                    return;
                }
                CustomerDTO customer = new CustomerDTO
                {
                    CustomerId = int.Parse(txtCustomerID.Text),
                    CustomerName = txtCustomerName.Text,
                    CustomerEmail = txtCustomerEmail.Text,
                    CustomerPhone = txtCustomerPhone.Text,
                    CustomerPassword = txtCustomerPassword.Text,
                    LoyaltyPoints = int.Parse(txtLoyaltyPoints.Text)
                };
                customerBL.Update(customer);
                MessageBox.Show("Cập nhật khách hàng thành công");
                populate();
                ClearFields();
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
                if (string.IsNullOrEmpty(txtCustomerID.Text))
                {
                    MessageBox.Show("Chọn khách hàng để xóa");
                    return;
                }
                customerBL.Delete(int.Parse(txtCustomerID.Text));
                MessageBox.Show("Xóa khách hàng thành công");
                populate();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CustomersDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtCustomerID.Text = CustomersDGV.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "";
                txtCustomerName.Text = CustomersDGV.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";
                txtCustomerEmail.Text = CustomersDGV.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "";
                txtCustomerPhone.Text = CustomersDGV.Rows[e.RowIndex].Cells[3].Value?.ToString() ?? "";
                txtCustomerPassword.Text = CustomersDGV.Rows[e.RowIndex].Cells[4].Value?.ToString() ?? "";
                txtLoyaltyPoints.Text = CustomersDGV.Rows[e.RowIndex].Cells[5].Value?.ToString() ?? "";
            }
        }

        private void ClearFields()
        {
            txtCustomerID.Text = "";
            txtCustomerName.Text = "";
            txtCustomerEmail.Text = "";
            txtCustomerPhone.Text = "";
            txtCustomerPassword.Text = "";
            txtLoyaltyPoints.Text = "";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBackToCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                Seller_Form seller_Form = new Seller_Form();
                seller_Form.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi quay lại form khách hàng: " + ex.Message);
            }
        }
    }
}