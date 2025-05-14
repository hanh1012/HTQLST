using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;

namespace PresentationLayer
{
    public partial class RevenueReport_Form : Form
    {
        private BillBL billBL;

        public RevenueReport_Form()
        {
            InitializeComponent();
            billBL = new BillBL();
            btnClose.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Icon\button_close.jpg");
            btnClose.BackgroundImageLayout = ImageLayout.Zoom;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
        }

        private void RevenueReport_Form_Load(object sender, EventArgs e)
        {
            cbMonth.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" });
            cbYear.Items.AddRange(new object[] { "2023", "2024", "2025" });
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (cbMonth.SelectedIndex == -1 || cbYear.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn tháng và năm");
                return;
            }

            int month = int.Parse(cbMonth.SelectedItem.ToString());
            int year = int.Parse(cbYear.SelectedItem.ToString());
            DataTable dt = billBL.GetRevenueByMonth(month, year);
            RevenueDGV.DataSource = dt;

            int totalRevenue = 0;
            foreach (DataRow row in dt.Rows)
            {
                totalRevenue += Convert.ToInt32(row["TotalAmount"]);
            }
            lblTotalRevenue.Text = $"Tổng doanh thu: {totalRevenue}";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
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