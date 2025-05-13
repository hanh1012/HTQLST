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
    public partial class TopProducts_Form : Form
    {
        private OrderDetailBL orderDetailBL;

        public TopProducts_Form()
        {
            InitializeComponent();
            orderDetailBL = new OrderDetailBL();
            btnClose.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Icon\button_close.jpg");
            btnClose.BackgroundImageLayout = ImageLayout.Zoom;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
        }

        private void TopProducts_Form_Load(object sender, EventArgs e)
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

            try
            {
                int month = int.Parse(cbMonth.SelectedItem.ToString());
                int year = int.Parse(cbYear.SelectedItem.ToString());
                DataTable dt = orderDetailBL.GetTopProducts(month, year);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu sản phẩm bán chạy cho tháng/năm này.");
                    TopProductsDGV.DataSource = null;
                }
                else
                {
                    TopProductsDGV.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo báo cáo sản phẩm bán chạy: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogout_Click(object sender, EventArgs eEventsArgs)
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