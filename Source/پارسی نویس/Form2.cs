using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace پارسی_نویس
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {            
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            STRUP nf = new STRUP();
            nf.Show();
            this.Hide();
        }

        private void PictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Image = پارسی_نویس.Properties.Resources.STbu2;
        }

        private void PictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = پارسی_نویس.Properties.Resources.STbu1;
        }

        private void PictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.Image = پارسی_نویس.Properties.Resources.EXbu21;
        }

        private void PictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = پارسی_نویس.Properties.Resources.EXbu2;
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            STRUP nf = new STRUP();
            nf.Show();
            this.Hide();
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}