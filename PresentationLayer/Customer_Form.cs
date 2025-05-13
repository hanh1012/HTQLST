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
using TransferObject;
using QRCoder;

namespace PresentationLayer
{
    public partial class Customer_Form : Form
    {
        private ProductBL productBL;
        private CustomerBL customerBL;
        private int Grdtotal = 0, n = 0;
        private DataTable cartTable;
        private PictureBox qrPictureBox; // Biến để lưu mã QR
        public Customer_Form()
        {
            InitializeComponent();
            productBL = new ProductBL();
            customerBL = new CustomerBL();
            btnClose.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Icon\button_close.jpg");
            btnClose.BackgroundImageLayout = ImageLayout.Zoom;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
        }

        private void populate()
        {
            try
            {
                ProdDGV.DataSource = productBL.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load sản phẩm: " + ex.Message);
            }
        }

        private void FillCategory()
        {
            try
            {
                DataTable dt = productBL.GetCategories();
                cbSelectCategory.ValueMember = "CatName";
                cbSelectCategory.DisplayMember = "CatName";
                cbSelectCategory.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh mục: " + ex.Message);
            }
        }

        private void Customer_Form_Load(object sender, EventArgs e)
        {
            try
            {
                cartTable = new DataTable();
                cartTable.Columns.Add("STT", typeof(int));
                cartTable.Columns.Add("Tên sản phẩm", typeof(string));
                cartTable.Columns.Add("Số lượng", typeof(int));
                cartTable.Columns.Add("Giá", typeof(int));
                cartTable.Columns.Add("Tổng", typeof(int));
                CartDGV.DataSource = cartTable;

                populate();
                FillCategory();
                lblCustomerEmail.Text = Login.CustomerEmail ?? "Guest";

                // Hiển thị điểm tích lũy
                int customerId = customerBL.GetCustomerIdByEmail(Login.CustomerEmail);
                int currentPoints = customerBL.GetPoints(customerId);
                lblCurrentPoints.Text = $": {currentPoints}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load form: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductName.Text) || string.IsNullOrEmpty(txtProductQuantity.Text))
            {
                MessageBox.Show("Chọn sản phẩm và số lượng!");
                return;
            }

            try
            {
                int quantity = Convert.ToInt32(txtProductQuantity.Text.Trim()); // Loại bỏ khoảng trắng
                int price = Convert.ToInt32(txtProductPrice.Text.Replace(" ", "").Trim()); // Loại bỏ khoảng trắng và dấu cách
                int total = price * quantity;

                DataGridViewRow selectedRow = null;
                foreach (DataGridViewRow row in ProdDGV.Rows)
                {
                    if (row.Cells[1].Value?.ToString() == txtProductName.Text)
                    {
                        selectedRow = row;
                        break;
                    }
                }
                if (selectedRow == null)
                {
                    MessageBox.Show("Sản phẩm không tồn tại!");
                    return;
                }
                int stockQty = Convert.ToInt32(selectedRow.Cells[2].Value);
                if (quantity > stockQty)
                {
                    MessageBox.Show("Số lượng không đủ!");
                    return;
                }

                cartTable.Rows.Add(n + 1, txtProductName.Text, quantity, price, total);
                n++;
                Grdtotal += total;
                lblTotalAmount.Text = "Tổng cộng: "+ Grdtotal.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm giỏ hàng: " + ex.Message + ". Vui lòng kiểm tra số lượng hoặc giá.");
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (cartTable.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng trống");
                return;
            }

            try
            {
                int customerId = customerBL.GetCustomerIdByEmail(Login.CustomerEmail);

                BillBL billBL = new BillBL();
                int billId = new Random().Next(1000, 9999);
                BillDTO bill = new BillDTO
                {
                    BillId = billId,
                    SellerName = "Customer",
                    BillDate = DateTime.Today.ToString("yyyy-MM-dd"),
                    TotalAmount = Grdtotal
                };
                billBL.Add(bill);

                OrderDetailBL orderDetailBL = new OrderDetailBL();
                int orderId = new Random().Next(1000, 9999);
                foreach (DataRow row in cartTable.Rows)
                {
                    int prodId = productBL.GetProductIdByName(row["Tên sản phẩm"].ToString());
                    int quantity = Convert.ToInt32(row["Số lượng"]);
                    OrderDetailDTO orderDetail = new OrderDetailDTO
                    {
                        OrderId = orderId++,
                        BillId = billId,
                        ProdId = prodId,
                        Quantity = quantity
                    };
                    orderDetailBL.Add(orderDetail);

                    // Giảm tồn kho
                    int currentStock = productBL.GetStockQuantity(prodId);
                    if (currentStock >= quantity)
                    {
                        productBL.UpdateStockQuantity(prodId, currentStock - quantity);
                    }
                    else
                    {
                        MessageBox.Show($"Số lượng tồn kho của {row["Tên sản phẩm"]} không đủ!");
                        return;
                    }
                }

                int pointsEarned = Grdtotal / 1000;
                customerBL.AddPoints(customerId, pointsEarned);
                int updatedPoints = customerBL.GetPoints(customerId);

                string message = $"Đặt hàng thành công! Bạn nhận được {pointsEarned} điểm. Tổng điểm: {updatedPoints}.";
                if (updatedPoints >= 50)
                {
                    message += "\nBạn đã đủ điểm để đổi quà! (50 điểm)";
                }
                MessageBox.Show(message);

                cartTable.Clear();
                n = 0;
                Grdtotal = 0;
                lblTotalAmount.Text = "0";
                lblCurrentPoints.Text = $"Điểm: {updatedPoints}";
                populate(); // Cập nhật lại ProdDGV với tồn kho mới
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán: " + ex.Message);
            }
        }

        private void btnGenerateQR_Click(object sender, EventArgs e)
        {
            if (CartDGV.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng trống");
                return;
            }

            try
            {
                // Xóa mã QR cũ (nếu có)
                if (qrPictureBox != null && this.Controls.Contains(qrPictureBox))
                {
                    this.Controls.Remove(qrPictureBox);
                    qrPictureBox.Dispose();
                }

                // Hiển thị QR ngân hàng tĩnh từ file
                qrPictureBox = new PictureBox
                {
                    Size = new Size(200, 200),
                    Location = new Point(300, 100),
                    Image = Image.FromFile(Application.StartupPath + @"\Icon\QR.jpg"),
                    SizeMode = PictureBoxSizeMode.Zoom
                };
                this.Controls.Add(qrPictureBox);
                qrPictureBox.BringToFront();
                MessageBox.Show($"Quét mã QR để thanh toán số tiền: {Grdtotal} VNĐ.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị mã QR: " + ex.Message);
            }
        }

        private void cbSelectCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                ProdDGV.DataSource = productBL.GetByCategory(cbSelectCategory.SelectedValue?.ToString() ?? "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lọc danh mục: " + ex.Message);
            }
        }

        private void ProdDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    txtProductName.Text = ProdDGV.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";
                    txtProductPrice.Text = ProdDGV.Rows[e.RowIndex].Cells[3].Value?.ToString() ?? "";
                    txtProductQuantity.Text = "1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chọn sản phẩm: " + ex.Message);
            }
        }


        private void btnCloseQR_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (qrPictureBox != null && this.Controls.Contains(qrPictureBox))
                {
                    this.Controls.Remove(qrPictureBox);
                    qrPictureBox.Dispose();
                    qrPictureBox = null; // Xóa tham chiếu
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đóng mã QR: " + ex.Message);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                new Login().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng xuất: " + ex.Message);
            }
        }
    }
}
