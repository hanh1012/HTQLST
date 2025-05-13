namespace PresentationLayer
{
    partial class Customer_Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnClose = new System.Windows.Forms.Button();
            this.ProdDGV = new System.Windows.Forms.DataGridView();
            this.CartDGV = new System.Windows.Forms.DataGridView();
            this.cbSelectCategory = new System.Windows.Forms.ComboBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.txtProductPrice = new System.Windows.Forms.TextBox();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblCustomerEmail = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGenerateQR = new System.Windows.Forms.Button();
            this.txtProductQuantity = new System.Windows.Forms.TextBox();
            this.btnCloseQR = new System.Windows.Forms.Button();
            this.lblCurrentPoints = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ProdDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CartDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1324, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 49);
            this.btnClose.TabIndex = 0;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ProdDGV
            // 
            this.ProdDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProdDGV.Location = new System.Drawing.Point(22, 150);
            this.ProdDGV.Margin = new System.Windows.Forms.Padding(4);
            this.ProdDGV.Name = "ProdDGV";
            this.ProdDGV.RowHeadersWidth = 51;
            this.ProdDGV.Size = new System.Drawing.Size(559, 250);
            this.ProdDGV.TabIndex = 1;
            this.ProdDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProdDGV_CellClick);
            // 
            // CartDGV
            // 
            this.CartDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CartDGV.Location = new System.Drawing.Point(606, 150);
            this.CartDGV.Margin = new System.Windows.Forms.Padding(4);
            this.CartDGV.Name = "CartDGV";
            this.CartDGV.RowHeadersWidth = 51;
            this.CartDGV.Size = new System.Drawing.Size(542, 250);
            this.CartDGV.TabIndex = 2;
            // 
            // cbSelectCategory
            // 
            this.cbSelectCategory.FormattingEnabled = true;
            this.cbSelectCategory.Location = new System.Drawing.Point(22, 100);
            this.cbSelectCategory.Margin = new System.Windows.Forms.Padding(4);
            this.cbSelectCategory.Name = "cbSelectCategory";
            this.cbSelectCategory.Size = new System.Drawing.Size(218, 36);
            this.cbSelectCategory.TabIndex = 3;
            this.cbSelectCategory.SelectionChangeCommitted += new System.EventHandler(this.cbSelectCategory_SelectionChangeCommitted);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(701, 467);
            this.txtProductName.Margin = new System.Windows.Forms.Padding(4);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(163, 34);
            this.txtProductName.TabIndex = 4;
            // 
            // txtProductPrice
            // 
            this.txtProductPrice.Location = new System.Drawing.Point(701, 514);
            this.txtProductPrice.Margin = new System.Windows.Forms.Padding(4);
            this.txtProductPrice.Name = "txtProductPrice";
            this.txtProductPrice.Size = new System.Drawing.Size(163, 34);
            this.txtProductPrice.TabIndex = 5;
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.Location = new System.Drawing.Point(606, 612);
            this.btnAddToCart.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(160, 50);
            this.btnAddToCart.TabIndex = 7;
            this.btnAddToCart.Text = "Thêm vào giỏ";
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            // 
            // btnCheckout
            // 
            this.btnCheckout.Location = new System.Drawing.Point(827, 612);
            this.btnCheckout.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(164, 50);
            this.btnCheckout.TabIndex = 8;
            this.btnCheckout.Text = "Thanh toán";
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(13, 700);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(185, 40);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblCustomerEmail
            // 
            this.lblCustomerEmail.AutoSize = true;
            this.lblCustomerEmail.Location = new System.Drawing.Point(22, 20);
            this.lblCustomerEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCustomerEmail.Name = "lblCustomerEmail";
            this.lblCustomerEmail.Size = new System.Drawing.Size(64, 28);
            this.lblCustomerEmail.TabIndex = 10;
            this.lblCustomerEmail.Text = "Email";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Location = new System.Drawing.Point(601, 700);
            this.lblTotalAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(117, 28);
            this.lblTotalAmount.TabIndex = 11;
            this.lblTotalAmount.Text = "Tổng cộng:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(601, 420);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 28);
            this.label1.TabIndex = 12;
            this.label1.Text = "Tên sản phẩm";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(601, 470);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 28);
            this.label2.TabIndex = 13;
            this.label2.Text = "Giá";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(601, 520);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 28);
            this.label3.TabIndex = 14;
            this.label3.Text = "Số lượng";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 70);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 28);
            this.label4.TabIndex = 15;
            this.label4.Text = "Loại hàng";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(601, 100);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 28);
            this.label5.TabIndex = 16;
            this.label5.Text = "Giỏ hàng";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(881, 420);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 28);
            this.label6.TabIndex = 18;
            this.label6.Text = "Điểm tích lũy";
            // 
            // btnGenerateQR
            // 
            this.btnGenerateQR.Location = new System.Drawing.Point(999, 612);
            this.btnGenerateQR.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerateQR.Name = "btnGenerateQR";
            this.btnGenerateQR.Size = new System.Drawing.Size(164, 50);
            this.btnGenerateQR.TabIndex = 19;
            this.btnGenerateQR.Text = "Mã QR";
            this.btnGenerateQR.Click += new System.EventHandler(this.btnGenerateQR_Click);
            // 
            // txtProductQuantity
            // 
            this.txtProductQuantity.Location = new System.Drawing.Point(702, 549);
            this.txtProductQuantity.Margin = new System.Windows.Forms.Padding(4);
            this.txtProductQuantity.Name = "txtProductQuantity";
            this.txtProductQuantity.Size = new System.Drawing.Size(163, 34);
            this.txtProductQuantity.TabIndex = 6;
            // 
            // btnCloseQR
            // 
            this.btnCloseQR.Location = new System.Drawing.Point(493, 20);
            this.btnCloseQR.Name = "btnCloseQR";
            this.btnCloseQR.Size = new System.Drawing.Size(88, 36);
            this.btnCloseQR.TabIndex = 21;
            this.btnCloseQR.Text = "Tắt QR";
            this.btnCloseQR.UseVisualStyleBackColor = true;
            this.btnCloseQR.Click += new System.EventHandler(this.btnCloseQR_Click_1);
            // 
            // lblCurrentPoints
            // 
            this.lblCurrentPoints.AutoSize = true;
            this.lblCurrentPoints.Location = new System.Drawing.Point(1028, 420);
            this.lblCurrentPoints.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentPoints.Name = "lblCurrentPoints";
            this.lblCurrentPoints.Size = new System.Drawing.Size(24, 28);
            this.lblCurrentPoints.TabIndex = 23;
            this.lblCurrentPoints.Text = "0";
            // 
            // Customer_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1396, 768);
            this.Controls.Add(this.lblCurrentPoints);
            this.Controls.Add(this.btnCloseQR);
            this.Controls.Add(this.btnGenerateQR);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lblCustomerEmail);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnCheckout);
            this.Controls.Add(this.btnAddToCart);
            this.Controls.Add(this.txtProductQuantity);
            this.Controls.Add(this.txtProductPrice);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.cbSelectCategory);
            this.Controls.Add(this.CartDGV);
            this.Controls.Add(this.ProdDGV);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Customer_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Dashboard";
            this.Load += new System.EventHandler(this.Customer_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ProdDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CartDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView ProdDGV;
        private System.Windows.Forms.DataGridView CartDGV;
        private System.Windows.Forms.ComboBox cbSelectCategory;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.TextBox txtProductPrice;
        private System.Windows.Forms.Button btnAddToCart;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblCustomerEmail;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGenerateQR;
        private System.Windows.Forms.TextBox txtProductQuantity;
        private System.Windows.Forms.Button btnCloseQR;
        private System.Windows.Forms.Label lblCurrentPoints;
    }
}