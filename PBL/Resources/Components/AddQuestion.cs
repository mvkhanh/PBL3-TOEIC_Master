﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL.Resources.Components
{
    public partial class AddQuestion : UserControl
    {
        public string AnswerA
        {
            get => txtA.Texts;
            set => txtA.Texts = value;
        }
        public string AnswerB
        {
            get => txtB.Texts;
            set => txtB.Texts = value;
        }
        public string AnswerC
        {
            get => txtC.Texts;
            set => txtC.Texts = value;
        }
        public string AnswerD
        {
            get => txtD.Texts;
            set => txtD.Texts = value;
        }
        public string Content
        {
            get => txtContent.Texts;
            set => txtContent.Texts = value;
        }
        public string ID
        {
            get => txtNumber.Texts;
            set => txtNumber.Texts = value;
        }

        public AddQuestion()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}