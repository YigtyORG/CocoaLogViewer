/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

namespace Covid19Radar.LogViewer.Launcher
{
	partial class FormReceiver
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
			this.labelDescription = new System.Windows.Forms.Label();
			this.textBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// labelDescription
			// 
			this.labelDescription.AutoSize = true;
			this.labelDescription.Font = new System.Drawing.Font("Yu Gothic UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.labelDescription.Location = new System.Drawing.Point(8, 8);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(255, 45);
			this.labelDescription.TabIndex = 0;
			this.labelDescription.Text = "labelDescription";
			// 
			// textBox
			// 
			this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.textBox.Location = new System.Drawing.Point(8, 64);
			this.textBox.Multiline = true;
			this.textBox.Name = "textBox";
			this.textBox.ReadOnly = true;
			this.textBox.Size = new System.Drawing.Size(480, 208);
			this.textBox.TabIndex = 1;
			this.textBox.Text = "textBox";
			// 
			// FormReceiver
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(496, 281);
			this.Controls.Add(this.textBox);
			this.Controls.Add(this.labelDescription);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(512, 320);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(512, 320);
			this.Name = "FormReceiver";
			this.Text = "FormReceiver";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormReceiver_FormClosing);
			this.Load += new System.EventHandler(this.FormReceiver_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelDescription;
		private System.Windows.Forms.TextBox textBox;
	}
}
