namespace SimpleBrowser.UI
{
    partial class SimpleBrowserFrame
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.backBtn = new System.Windows.Forms.Button();
            this.forwardBtn = new System.Windows.Forms.Button();
            this.reloadBtn = new System.Windows.Forms.Button();
            this.webViewContainer = new System.Windows.Forms.Panel();
            this.addressBar = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.backBtn, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.forwardBtn, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.reloadBtn, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.webViewContainer, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.addressBar, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(380, 303);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // backBtn
            // 
            this.backBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backBtn.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.backBtn.Image = global::SimpleBrowser.Properties.Resources.backword;
            this.backBtn.Location = new System.Drawing.Point(1, 1);
            this.backBtn.Margin = new System.Windows.Forms.Padding(1);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(26, 26);
            this.backBtn.TabIndex = 0;
            this.backBtn.UseVisualStyleBackColor = true;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // forwardBtn
            // 
            this.forwardBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.forwardBtn.Image = global::SimpleBrowser.Properties.Resources.forword;
            this.forwardBtn.Location = new System.Drawing.Point(29, 1);
            this.forwardBtn.Margin = new System.Windows.Forms.Padding(1);
            this.forwardBtn.Name = "forwardBtn";
            this.forwardBtn.Size = new System.Drawing.Size(26, 26);
            this.forwardBtn.TabIndex = 1;
            this.forwardBtn.UseVisualStyleBackColor = true;
            this.forwardBtn.Click += new System.EventHandler(this.forwardBtn_Click);
            // 
            // reloadBtn
            // 
            this.reloadBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reloadBtn.Image = global::SimpleBrowser.Properties.Resources.reload;
            this.reloadBtn.Location = new System.Drawing.Point(57, 1);
            this.reloadBtn.Margin = new System.Windows.Forms.Padding(1);
            this.reloadBtn.Name = "reloadBtn";
            this.reloadBtn.Size = new System.Drawing.Size(26, 26);
            this.reloadBtn.TabIndex = 2;
            this.reloadBtn.UseVisualStyleBackColor = true;
            this.reloadBtn.Click += new System.EventHandler(this.reloadBtn_Click);
            // 
            // webViewContainer
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.webViewContainer, 4);
            this.webViewContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webViewContainer.Location = new System.Drawing.Point(0, 28);
            this.webViewContainer.Margin = new System.Windows.Forms.Padding(0);
            this.webViewContainer.Name = "webViewContainer";
            this.webViewContainer.Size = new System.Drawing.Size(380, 275);
            this.webViewContainer.TabIndex = 4;
            // 
            // addressBar
            // 
            this.addressBar.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.addressBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.addressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addressBar.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.addressBar.Location = new System.Drawing.Point(86, 3);
            this.addressBar.Margin = new System.Windows.Forms.Padding(2, 3, 2, 2);
            this.addressBar.Name = "addressBar";
            this.addressBar.Size = new System.Drawing.Size(292, 23);
            this.addressBar.TabIndex = 5;
            this.addressBar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.addressBar_KeyDown);
            // 
            // SimpleBrowserFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 303);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SimpleBrowserFrame";
            this.Text = "SimpleBrowser";
            this.Load += new System.EventHandler(this.SimpleBrowserFrame_Load);
            this.Resize += new System.EventHandler(this.SimpleBrowserFrame_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button backBtn;
        private System.Windows.Forms.Button forwardBtn;
        private System.Windows.Forms.Button reloadBtn;
        private System.Windows.Forms.Panel webViewContainer;
        private System.Windows.Forms.TextBox addressBar;
    }
}

