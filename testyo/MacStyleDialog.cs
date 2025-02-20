using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSONotify {
	public partial class MacStyleDialog: Form {
		private Size m_OriginalSize = new Size(0, 0);
		private Size m_MotionSize = new Size(0, 0);
		private bool m_IsOpened;
		private bool m_IsAnimating;
		private Form m_ParentForm = null;
		private MessageBoxButtons m_ButtonStyle;

		public delegate void onConfirm();
		public delegate void onCancel();
		public delegate void onDeny();
		public event onConfirm OnConfirm;
		public event onCancel OnCancel;
		public event onDeny OnDeny;

		Timer m_MotionTimer;

		public MacStyleDialog() {
			InitializeComponent();
			this.m_OriginalSize = this.Size;
			this.m_MotionSize.Width = this.Size.Width;
			this.m_MotionSize.Height = this.Size.Height;
			this.m_MotionTimer = new Timer();
			this.m_MotionTimer.Interval = 10;
			this.m_MotionTimer.Tick += this.motionTimer_Tick;
			if(this.m_ParentForm != null) {
				this.StartPosition = FormStartPosition.Manual;
				this.Left = this.m_ParentForm.Location.X;
				this.Top = this.m_ParentForm.Location.Y;
			}
		}
		public new Form Parent {
			get { return this.m_ParentForm; }
			set { 
				this.m_ParentForm = value;
				this.m_ParentForm.Move += m_ParentForm_Move;
			}
		}

		void m_ParentForm_Move(object sender, EventArgs e) {
			if(this.m_ParentForm != null) {
				this.Left = (this.m_ParentForm.Left + (this.m_ParentForm.Size.Width / 2)) - (this.Size.Width / 2);
				this.Top = this.m_ParentForm.Top + 22;
			}
		}
		private void motionTimer_Tick(object sender, EventArgs e) {
			if(this.m_IsAnimating) {
				if(this.m_IsOpened) {
					if(this.m_MotionSize.Height < this.m_OriginalSize.Height) {
						this.m_MotionSize.Height += 22;
						if(this.Visible == false) {
							this.Visible = true;
						}
					} else {
						this.m_MotionSize.Height = this.m_OriginalSize.Height;
						this.m_IsAnimating = false;
						this.m_MotionTimer.Stop();
					}
				} else {
					if(this.m_MotionSize.Height > 0) {
						this.m_MotionSize.Height -= 22;
						
					} else {
						this.m_MotionSize.Height = 0;
						this.m_IsAnimating = false;
						this.m_MotionTimer.Stop();
						this.Visible = false;
					}
					
				}
				this.Size = this.m_MotionSize;
			}
		}
		public void close() {
			this.m_IsOpened = false;
			this.m_IsAnimating = true;
			this.m_MotionTimer.Start();
			this.m_ParentForm.Enabled = true;
			this.m_OriginalSize = this.Size;
			this.m_MotionSize.Width = this.Size.Width;
		}
		public void setStartSize(Size size) {
			this.Size = size;
			this.m_OriginalSize = size;
			this.m_MotionSize.Width = size.Width;
		}
		public void open() {
			this.m_MotionSize.Height = 0;
			this.m_IsOpened = true;
			this.m_IsAnimating = true;
			this.Left = (this.m_ParentForm.Left + (this.m_ParentForm.Size.Width / 2)) - (this.Size.Width / 2);
			this.Top = this.m_ParentForm.Top + 22;
			this.m_MotionSize.Width = this.Width;
			this.m_OriginalSize.Width = this.Width;
			this.m_MotionTimer.Start();
			this.m_ParentForm.Enabled = false;
		}

		private void onResizeEnd(object sender, EventArgs e) {
			syncSize();
		}
		private void syncSize() {
			if(this.m_ParentForm != null) {
				this.Left = (this.m_ParentForm.Left + (this.m_ParentForm.Size.Width / 2)) - (this.Size.Width / 2);
			}
		}

		private void DenyButton_Click(object sender, EventArgs e) {
			this.close();
			switch(this.m_ButtonStyle) {
				case MessageBoxButtons.OKCancel: {
					if(this.OnCancel != null) {
						this.OnCancel();
					}
					break;
				}
				case MessageBoxButtons.RetryCancel: {
					if(this.OnCancel != null) {
						this.OnCancel();
					}
					break;
				}
				case MessageBoxButtons.YesNo: {
					if(this.OnDeny != null) {
						this.OnDeny();
					}
					break;
				}
				case MessageBoxButtons.YesNoCancel: {
					if(this.OnDeny != null) {
						this.OnDeny();
					}
					break;
				}
			}
		}
		private void CancelButton_Click(object sender, EventArgs e) {
			this.close();
			if(this.OnCancel != null) {
				this.OnCancel();
			}
		}

		private void AcceptButton_Click(object sender, EventArgs e) {
			this.close();
			if(this.OnConfirm != null) {
				this.OnConfirm();
			}
		}

		private void onResizeDrag(object sender, EventArgs e) {
			syncSize();
		}

		private void onResizeBegin(object sender, EventArgs e) {
			syncSize();
		}

		public void promptMessage(string message, MessageBoxButtons buttons) {
			this.label.Text = message;
			this.progressbar.Visible = false;
			this.m_ButtonStyle = buttons;
			switch(this.m_ButtonStyle) {
				case MessageBoxButtons.OK: {
					deny.Visible = false;
					accept.Visible = true;
					cancel.Visible = false;
					accept.Text = "Ok";
					break;
				}
				case MessageBoxButtons.OKCancel: {
					deny.Visible = true;
					accept.Visible = true;
					cancel.Visible = false;
					accept.Text = "Ok";
					deny.Text = "Cancel";
					break;
				}
				case MessageBoxButtons.RetryCancel: {
					deny.Visible = true;
					accept.Visible = true;
					cancel.Visible = true;
					accept.Text = "Retry";
					deny.Text = "Cancel";
					break;
				}
				case MessageBoxButtons.YesNo: {
					deny.Visible = true;
					accept.Visible = true;
					cancel.Visible = false;
					accept.Text = "Yes";
					deny.Text = "No";
					cancel.Text = "Cancel";
					break;
				}
				case MessageBoxButtons.YesNoCancel: {
					deny.Visible = true;
					accept.Visible = true;
					cancel.Visible = true;
					accept.Text = "Yes";
					deny.Text = "No";
					cancel.Text = "Cancel";
					break;
				}
			}

			this.open();
		}
		public void promptProgressbar(string label) {
			this.label.Text = label;
			this.progressbar.Visible = true;
			this.open();
		}
	}
}
