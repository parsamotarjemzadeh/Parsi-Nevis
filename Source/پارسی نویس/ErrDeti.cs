using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace پارسی_نویس
{
    public partial class ErrDeti : Form
    {
        public ErrDeti()
        {
            InitializeComponent();
        }

        private void ErrDeti_Load(object sender, EventArgs e)
        {
            label1.Text = Form1.EBshow;
            int i = 3;
            string tmp = "";
            while (Form1.EBback[i].ToString() != "$")
                i++;
            i++;
            while (Form1.EBback[i].ToString() != "$")
            {
                tmp += Form1.EBback[i].ToString();
                i++;
            }
            label2.Text = "محل خطا:     " + Form1.EBrname + "    خط " + (Convert.ToInt32(tmp) + 1).ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form1.EBshdeti = true;
            this.Close();
        }
    }
}
