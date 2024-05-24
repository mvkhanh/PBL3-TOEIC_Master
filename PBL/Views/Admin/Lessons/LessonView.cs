﻿using PBL.Views.Admin.Lessons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL
{
    public partial class LessonView : Form, ILessonView
    {
        //Fields
        private bool _IsEdit;
        private bool _IsSuccessful;
        private string _Message;

        public LessonView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabPageLessonDetail);
        }

        private void AssociateAndRaiseViewEvents()
        {
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SearchEvent?.Invoke(this, EventArgs.Empty);
                }
            };
            btnAddNew.Click += delegate
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageLessonList);
                tabControl1.TabPages.Add(tabPageLessonDetail);
                tabPageLessonDetail.Text = "Add new Lesson";
            };
            btnEdit.Click += delegate
            {
                try
                {
                    EditEvent?.Invoke(this, EventArgs.Empty);
                    tabControl1.TabPages.Remove(tabPageLessonList);
                    tabControl1.TabPages.Add(tabPageLessonDetail);
                    tabPageLessonDetail.Text = "Edit Lesson";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            btnDelete.Click += delegate
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected Lesson?", "Warning", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
            btnSave.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (IsSuccessful)
                {
                    tabControl1.TabPages.Remove(tabPageLessonDetail);
                    tabControl1.TabPages.Add(tabPageLessonList);
                }
                MessageBox.Show(Message);
            };
            btnCancel.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageLessonDetail);
                tabControl1.TabPages.Add(tabPageLessonList);
            };
        }

        //Properties
        public int LessonId { get => Convert.ToInt32(txtId.Text); set => txtId.Text = value.ToString(); }
        public string LessonName { get => txtName.Text; set => txtName.Text = value; }
        public DateTime LessonPublishDay { get => Convert.ToDateTime(regist.Value); set => regist.Value = value; }
        public byte[] LessonContent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int LessonViews { get => Convert.ToInt32(txtView.Text); set => txtView.Text = value.ToString(); }
        public int LessonId_Teacher { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SearchValue { get => txtSearch.Text; set => txtSearch.Text = value; }
        public bool IsEdit { get => _IsEdit; set => _IsEdit = value; }
        public bool IsSuccessful { get => _IsSuccessful; set => _IsSuccessful = value; }
        public string Message { get => _Message; set => _Message = value; }

        //Events
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler EditEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;


        //Methods
        public void SetLessonListBindingSource(BindingSource lessonList)
        {
            dataGridView1.DataSource = lessonList;
        }

        //Singleton
        private static LessonView instance;
        public static LessonView GetInstance()
        {
            if (instance == null || instance.IsDisposed) instance = new LessonView();
            return instance;
        }
    }
}