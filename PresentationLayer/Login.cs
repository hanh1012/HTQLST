using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using TransferObject;

namespace PresentationLayer
{
    public partial class Login : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

        private const int EM_SETCUEBANNER = 0x1501;
        public static string Sellername = "";
        public static string CustomerEmail = "";
        public static string CurrentRole = "";
        public Login()
        {
            InitializeComponent();
            btnClose.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Icon\button_close.jpg");
            btnClose.BackgroundImageLayout = ImageLayout.Zoom;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\SMMSD.mdf;Integrated Security=True");

        private void Login_Load(object sender, EventArgs e)
        {
            SendMessage(txtUsername.Handle, EM_SETCUEBANNER, 0, "Nhập tên người dùng (hoặc email cho khách hàng)");
            SendMessage(txtPassword.Handle, EM_SETCUEBANNER, 0, "Nhập mật khẩu");
            // Xóa các mục cũ trước khi thêm mới để tránh trùng lặp
            cbSelectRole.Items.Clear();
            cbSelectRole.Items.AddRange(new object[] { "Admin", "Seller", "Customer" });
            cbSelectRole.SelectedIndex = -1; // Đặt mặc định không chọn
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin");
                return;
            }

            if (cbSelectRole.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn chức vụ");
                return;
            }

            string role = cbSelectRole.SelectedItem.ToString();
            CurrentRole = role; 
            if (role == "Admin")
            {
                if (txtUsername.Text == "admin" && txtPassword.Text == "password")
                {
                    Product_Form prod = new Product_Form();
                    prod.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Nếu bạn là admin, hãy đảm bảo nhập đúng tên và mật khẩu");
                }
            }
            else if (role == "Seller")
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter($"SELECT COUNT(*) FROM SellersTbl WHERE SellerName='{txtUsername.Text}' AND SellerPassword='{txtPassword.Text}'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    Sellername = txtUsername.Text;
                    Selling_Form sell = new Selling_Form();
                    sell.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai");
                }
                Con.Close();
            }
            else if (role == "Customer")
            {
                CustomerBL customerBL = new CustomerBL();
                CustomerDTO customer = new CustomerDTO
                {
                    CustomerEmail = txtUsername.Text,
                    CustomerPassword = txtPassword.Text
                };
                if (customerBL.Login(customer))
                {
                    CustomerEmail = txtUsername.Text;
                    Customer_Form customerForm = new Customer_Form();
                    customerForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Email hoặc mật khẩu sai");
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            cbSelectRole.SelectedIndex = -1;
        }


    }
}