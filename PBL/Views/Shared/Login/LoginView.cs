﻿using PBL.Views.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PBL.Views.Shared
{
    public partial class LoginView : Form, ILoginView
    {
        //Fields
        private bool _IsSuccessful;
        private string _Message;

        //Constructor
        public LoginView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        private void AssociateAndRaiseViewEvents()
        {
            btnLogin.Click += delegate {
                LoginEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccessful)
                {
                    this.Hide();
                }
                MessageBox.Show(Message);
            };
            btnSignUp.Click += delegate
            {
                SignUpEvent?.Invoke(this, EventArgs.Empty);
                this.Hide();
            };
        }

        #region Properties
        public string Account
         { get => txtAccount.Texts; set => txtAccount.Texts = value.Trim(); }
        public string Password
            { get => txtPassword.Texts; set => txtPassword.Texts = value.Trim(); }

        public bool isSuccessful 
            { get => _IsSuccessful; set => _IsSuccessful = value; }
        public string Message { get => _Message; set => _Message = value; }
        #endregion

        //Events
        public event EventHandler LoginEvent;
        public event EventHandler SignUpEvent;
        //Methods
        #region Show/Hide Password
        private void btOpenEye_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar)
            {
                btCloseEye.BringToFront();
                txtPassword.PasswordChar = false;
            }
        }

        private void btCloseEye_Click(object sender, EventArgs e)
        {
            if (!txtPassword.PasswordChar)
            {
                btOpenEye.BringToFront();
                txtPassword.PasswordChar = true;
            }
        }

        private void txtPass__TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Texts)) btCloseEye.Visible = btOpenEye.Visible = false;
            else btCloseEye.Visible = btOpenEye.Visible = true;
        }
        #endregion

        #region Title Button
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
                else if(sender == panelTitle)
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
        #endregion


        #region Singleton
        private static LoginView instance;
        public static LoginView GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new LoginView();
            }
            return instance;
        }
        #endregion
    }
}
