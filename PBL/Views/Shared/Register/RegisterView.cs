﻿using PBL.Views.Shared.Register;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PBL.Views.Shared
{
    public partial class RegisterView : Form, IRegisterView
    {
        //Fields
        private bool _IsSuccessfully;
        private string _Message;

        public RegisterView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            btnSignUp.Click += delegate
            {
                if(txtPass1.Texts != txtPass2.Texts)
                {
                    MessageBox.Show("Password enter again not same");
                    return;
                }
                SignUpEvent?.Invoke(this, EventArgs.Empty);
                if (IsSuccessful)
                {
                    txtPass2.Texts = "";
                    btnReturn.PerformClick();
                }
                MessageBox.Show(Message);
            };
            btnReturn.Click += delegate
            {
                ReturnEvent?.Invoke(this, EventArgs.Empty);
                txtPass2.Texts = "";
                this.Hide();
            };
        }

        public event EventHandler ReturnEvent;
        public event EventHandler SignUpEvent;


        #region Properties
        public string Account { get => txtAccount.Texts; set => txtAccount.Texts = value; }
        public string UserName { get => txtUserName.Texts; set => txtUserName.Texts = value; }
        public string Password { get => txtPass1.Texts; set => txtPass1.Texts = value; }
        public string Email { get => txtEmail.Texts; set => txtEmail.Texts = value; }
        public bool IsSuccessful { get => _IsSuccessfully; set => _IsSuccessfully = value; }
        public string Message { get => _Message; set => _Message = value; }
        #endregion

        #region Show/Hide Password
        private void btCloseEye_Click1(object sender, EventArgs e)
        {
            if (!txtPass1.PasswordChar)
            {
                btOpenEye1.BringToFront();
                txtPass1.PasswordChar = true;
            }
        }
        private void btOpenEye_Click1(object sender, EventArgs e)
        {
            if (txtPass1.PasswordChar)
            {
                btCloseEye1.BringToFront();
                txtPass1.PasswordChar = false;
            }
        }
        private void btOpenEye_Click2(object sender, EventArgs e)
        {
            if (txtPass2.PasswordChar)
            {
                btCloseEye2.BringToFront();
                txtPass2.PasswordChar = false;
            }
        }

        private void btCloseEye_Click2(object sender, EventArgs e)
        {
            if (!txtPass2.PasswordChar)
            {
                btOpenEye2.BringToFront();
                txtPass2.PasswordChar = true;
            }
        }

        private void txtPass__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPass1.Texts)) btCloseEye1.Visible = btOpenEye1.Visible = false;
            else btCloseEye1.Visible = btOpenEye1.Visible = true;
            if (string.IsNullOrEmpty(txtPass2.Texts)) btCloseEye2.Visible = btOpenEye2.Visible = false;
            else btCloseEye2.Visible = btOpenEye2.Visible = true;
        }
        #endregion

        #region Close Button
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Drag and resize form
        // Import the necessary Windows API functions
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        // Define the constant values for the interop functions
        private const int WM_NCLBUTTONDOWN = 0x00A1;
        private const int HT_CAPTION = 0x0002;
        private const int HT_LEFT = 0x0A;
        private const int HT_RIGHT = 0x0B;
        private const int HT_TOP = 0x0C;
        private const int HT_TOPLEFT = 0x0D;
        private const int HT_TOPRIGHT = 0x0E;
        private const int HT_BOTTOM = 0x0F;
        private const int HT_BOTTOMLEFT = 0x10;
        private const int HT_BOTTOMRIGHT = 0x11;

        // Handle the mouse down event on the form to enable dragging and resizing
        private void resizePanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.X <= 5 && e.Y <= 5) // Top-left corner
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_TOPLEFT, 0);
                }
                else if (e.X >= panelTitle.Width - 5 && e.Y <= 5) // Top-right corner
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_TOPRIGHT, 0);
                }
                else if (e.X <= 5 && e.Y >= panelMain.Height - 5) // Bottom-left corner
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_BOTTOMLEFT, 0);
                }
                else if (e.X >= panelMain.Width - 5 && e.Y >= panelMain.Height - 5) // Bottom-right corner
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_BOTTOMRIGHT, 0);
                }
                else if (e.Y <= 5) // Top edge
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_TOP, 0);
                }
                else if (e.Y >= panelMain.Height - 5) // Bottom edge
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_BOTTOM, 0);
                }
                else if (e.X <= 5) // Left edge
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_LEFT, 0);
                }
                else if (e.X >= this.Width - 5) // Right edge
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_RIGHT, 0);
                }
                else if (sender == panelTitle)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
        }
        // Handle the mouse move event on the resize panel to change the mouse pointer
        private void panelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X <= 5 && e.Y <= 5) Cursor = Cursors.SizeNWSE;
            else if (e.X >= panelTitle.Width - 5 && e.Y <= 5) Cursor = Cursors.SizeNESW;
            else if (e.X <= 5 || e.X >= panelTitle.Width - 5) Cursor = Cursors.SizeWE;
            else if (e.Y <= 5) Cursor = Cursors.SizeNS;
            else Cursor = Cursors.Default;
        }
        private void panelMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X >= panelMain.Width - 5 && e.Y >= panelMain.Height - 5) Cursor = Cursors.SizeNWSE;
            else if (e.X <= 5 && e.Y >= panelMain.Height - 5) Cursor = Cursors.SizeNESW;
            else if (e.Y >= panelMain.Height - 5) Cursor = Cursors.SizeNS;
            else if (e.X <= 5 || e.X >= this.Width - 5) Cursor = Cursors.SizeWE;
            else Cursor = Cursors.Default;
        }
        private void btnRestoreDown_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void btnMinisize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region Singleton
        private static RegisterView instance;
        public static RegisterView GetInstance()
        {
            if(instance == null || instance.IsDisposed) { instance = new RegisterView(); }
            return instance;
        }
        #endregion
    }
}
