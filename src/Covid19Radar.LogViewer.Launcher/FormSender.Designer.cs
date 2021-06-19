
namespace Covid19Radar.LogViewer.Launcher
{
	partial class FormSender
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
			if (disposing && (components != null)) {
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
			this.labelAddress = new System.Windows.Forms.Label();
			this.tboxAddress = new System.Windows.Forms.TextBox();
			this.labelPort = new System.Windows.Forms.Label();
			this.nudPort = new System.Windows.Forms.NumericUpDown();
			this.labelFile = new System.Windows.Forms.Label();
			this.tboxFile = new System.Windows.Forms.TextBox();
			this.btnOpenFile = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSend = new System.Windows.Forms.Button();
			this.ofd = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
			this.SuspendLayout();
			// 
			// labelAddress
			// 
			this.labelAddress.AutoSize = true;
			this.labelAddress.Location = new System.Drawing.Point(8, 8);
			this.labelAddress.Name = "labelAddress";
			this.labelAddress.Size = new System.Drawing.Size(74, 15);
			this.labelAddress.TabIndex = 0;
			this.labelAddress.Text = "labelAddress";
			// 
			// tboxAddress
			// 
			this.tboxAddress.Location = new System.Drawing.Point(8, 32);
			this.tboxAddress.Name = "tboxAddress";
			this.tboxAddress.Size = new System.Drawing.Size(480, 23);
			this.tboxAddress.TabIndex = 1;
			// 
			// labelPort
			// 
			this.labelPort.AutoSize = true;
			this.labelPort.Location = new System.Drawing.Point(8, 64);
			this.labelPort.Name = "labelPort";
			this.labelPort.Size = new System.Drawing.Size(54, 15);
			this.labelPort.TabIndex = 2;
			this.labelPort.Text = "labelPort";
			// 
			// nudPort
			// 
			this.nudPort.Location = new System.Drawing.Point(8, 88);
			this.nudPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nudPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudPort.Name = "nudPort";
			this.nudPort.Size = new System.Drawing.Size(120, 23);
			this.nudPort.TabIndex = 3;
			this.nudPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nudPort.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// labelFile
			// 
			this.labelFile.AutoSize = true;
			this.labelFile.Location = new System.Drawing.Point(8, 120);
			this.labelFile.Name = "labelFile";
			this.labelFile.Size = new System.Drawing.Size(50, 15);
			this.labelFile.TabIndex = 4;
			this.labelFile.Text = "labelFile";
			// 
			// tboxFile
			// 
			this.tboxFile.Location = new System.Drawing.Point(8, 144);
			this.tboxFile.Name = "tboxFile";
			this.tboxFile.Size = new System.Drawing.Size(456, 23);
			this.tboxFile.TabIndex = 5;
			this.tboxFile.ReadOnly = true;
			// 
			// btnOpenFile
			// 
			this.btnOpenFile.Location = new System.Drawing.Point(464, 144);
			this.btnOpenFile.Name = "btnOpenFile";
			this.btnOpenFile.Size = new System.Drawing.Size(27, 23);
			this.btnOpenFile.TabIndex = 6;
			this.btnOpenFile.Text = "...";
			this.btnOpenFile.UseVisualStyleBackColor = true;
			this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(336, 248);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(416, 248);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(75, 23);
			this.btnSend.TabIndex = 8;
			this.btnSend.Text = "btnSend";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// ofd
			// 
			this.ofd.AddExtension = false;
			this.ofd.ReadOnlyChecked = true;
			this.ofd.RestoreDirectory = true;
			this.ofd.SupportMultiDottedExtensions = true;
			// 
			// FormSender
			// 
			this.AcceptButton = this.btnSend;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(496, 281);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOpenFile);
			this.Controls.Add(this.tboxFile);
			this.Controls.Add(this.labelFile);
			this.Controls.Add(this.nudPort);
			this.Controls.Add(this.labelPort);
			this.Controls.Add(this.tboxAddress);
			this.Controls.Add(this.labelAddress);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSender";
			this.Text = "FormSender";
			this.Load += new System.EventHandler(this.FormSender_Load);
			((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelAddress;
		private System.Windows.Forms.TextBox tboxAddress;
		private System.Windows.Forms.Label labelPort;
		private System.Windows.Forms.NumericUpDown nudPort;
		private System.Windows.Forms.Label labelFile;
		private System.Windows.Forms.TextBox tboxFile;
		private System.Windows.Forms.Button btnOpenFile;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.OpenFileDialog ofd;
	}
}
