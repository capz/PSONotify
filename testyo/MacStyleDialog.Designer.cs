namespace PSONotify {
	partial class MacStyleDialog {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.deny = new System.Windows.Forms.Button();
			this.accept = new System.Windows.Forms.Button();
			this.label = new System.Windows.Forms.Label();
			this.progressbar = new System.Windows.Forms.ProgressBar();
			this.cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// deny
			// 
			this.deny.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.deny.Location = new System.Drawing.Point(295, 159);
			this.deny.Name = "deny";
			this.deny.Size = new System.Drawing.Size(75, 23);
			this.deny.TabIndex = 0;
			this.deny.Text = "Deny";
			this.deny.UseVisualStyleBackColor = true;
			this.deny.Click += new System.EventHandler(this.DenyButton_Click);
			// 
			// accept
			// 
			this.accept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.accept.Location = new System.Drawing.Point(376, 159);
			this.accept.Name = "accept";
			this.accept.Size = new System.Drawing.Size(75, 23);
			this.accept.TabIndex = 1;
			this.accept.Text = "Accept";
			this.accept.UseVisualStyleBackColor = true;
			this.accept.Click += new System.EventHandler(this.AcceptButton_Click);
			// 
			// label
			// 
			this.label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label.Location = new System.Drawing.Point(12, 9);
			this.label.Name = "label";
			this.label.Size = new System.Drawing.Size(439, 173);
			this.label.TabIndex = 2;
			this.label.Text = "label1";
			// 
			// progressbar
			// 
			this.progressbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressbar.Location = new System.Drawing.Point(12, 25);
			this.progressbar.Name = "progressbar";
			this.progressbar.Size = new System.Drawing.Size(439, 23);
			this.progressbar.TabIndex = 3;
			// 
			// cancel
			// 
			this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cancel.Location = new System.Drawing.Point(12, 159);
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size(75, 23);
			this.cancel.TabIndex = 4;
			this.cancel.Text = "Cancel";
			this.cancel.UseVisualStyleBackColor = true;
			this.cancel.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// MacStyleDialog
			// 
			this.AcceptButton = this.deny;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(463, 194);
			this.ControlBox = false;
			this.Controls.Add(this.cancel);
			this.Controls.Add(this.progressbar);
			this.Controls.Add(this.accept);
			this.Controls.Add(this.deny);
			this.Controls.Add(this.label);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "MacStyleDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.TopMost = true;
			this.ResizeBegin += new System.EventHandler(this.onResizeBegin);
			this.ResizeEnd += new System.EventHandler(this.onResizeEnd);
			this.Resize += new System.EventHandler(this.onResizeDrag);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button deny;
		private System.Windows.Forms.Button accept;
		public System.Windows.Forms.ProgressBar progressbar;
		public System.Windows.Forms.Label label;
		private System.Windows.Forms.Button cancel;

	}
}