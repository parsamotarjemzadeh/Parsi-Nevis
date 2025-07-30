using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace پارسی_نویس
{
    public partial class STRUP : Form
    {
        public STRUP()
        {
            InitializeComponent();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            bool isopen = true;
            try
            {
                string tmpst = "";
                string[] opj = File.ReadAllLines(listRB.Items[listRF.SelectedIndex].ToString());
                File.WriteAllText(".\\PrNa.parsinevis", opj[0]);
                for (int i = 0; i < (((listRB.Items[listRF.SelectedIndex].ToString()).Length) - (((opj[0] + ".prjparsinevis").ToString()).Length)) ; i++)
                {
                    tmpst = tmpst + (listRB.Items[listRF.SelectedIndex].ToString())[i];
                }
                tmpst = tmpst + "tpfiles\\";
                File.WriteAllText(".\\PrPa.parsinevis", tmpst);
                File.WriteAllText(".\\NewPr.parsinevis", "0");
            }
            catch
            {
                MessageBox.Show("فایل های مربوط به پروژه یافت نشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isopen = false;
                delrec(listRF.SelectedIndex);
            }
            if (isopen == true)
            {
                Form1 frmp = new Form1();
                frmp.Show();
                this.Hide();
            }
        }

        private void STRUP_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            label5.BackColor = System.Drawing.Color.Red;
            label5.ForeColor = System.Drawing.Color.White;
            button1.Enabled = false;
            bool tmp_co_bool = true;
            for (int i = 1; i < textBox1.Text.Length; i++)
            {
                if (textBox1.Text[i] < 48 || textBox1.Text[i] > 122) tmp_co_bool = false;
                if (textBox1.Text[i] > 90 && textBox1.Text[i] < 97) tmp_co_bool = false;
                if (textBox1.Text[i] > 57 && textBox1.Text[i] < 65) tmp_co_bool = false;
            }
            if (textBox1.Text == "") label5.Text = "نام نمی‌تواند خالی باشد.";
            else if (textBox1.Text.Length > 25) label5.Text = "نام حداکثر می‌تواند 25 حرف داشته باشد.";
            else if (textBox1.Text[0] < 65 || textBox1.Text[0] > 122) label5.Text = "نام فقط با حروف لاتین شروع می‌شود.";
            else if (textBox1.Text[0] > 90 && textBox1.Text[0] < 97) label5.Text = "نام فقط با حروف لاتین شروع می‌شود.";
            else if (tmp_co_bool == false) label5.Text = "در نام می‌توان فقط از حروف لاتین و اعداد استفاده کرد.";
            
            else
            {
                label5.BackColor = System.Drawing.Color.LightGreen;
                label5.ForeColor = System.Drawing.Color.Black;
                label5.Text = "نام پروژه قابل قبول است";
                button1.Enabled = true;
            }
        }

        private void STRUP_Load(object sender, EventArgs e)
        {
            TextBox1_TextChanged(null, null);
            string[] g1 = File.ReadAllLines(".\\RCF.parsinevis");
            string[] g2 = File.ReadAllLines(".\\RCB.parsinevis");
            listRB.Items.Clear();
            listRF.Items.Clear();
            for (int i = 0; i < g1.Length; i++) listRF.Items.Add(g1[i]);
            for (int i = 0; i < g2.Length; i++) listRB.Items.Add(g2[i]);
            if (listRF.SelectedIndex != -1)
            {
                button2.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            bool exsw = false;
            if (textBox2.Text != "")
            {
                string pathString = System.IO.Path.Combine(textBox2.Text, textBox1.Text);
                if (System.IO.File.Exists(System.IO.Path.Combine(pathString, textBox1.Text + ".Prjparsinevis")))
                {
                    if(MessageBox.Show("پوشه‌ای با نام پروژه شما در محل مشخص شده وجود دارد و ممکن است به پروژه شما آسیب بزند. آیا مایل به ادامه عملیات هستید؟", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        exsw = false;
                    else
                        exsw = true;
                }
                if (exsw == false)
                {
                    System.IO.Directory.CreateDirectory(pathString);
                    File.WriteAllText(System.IO.Path.Combine(pathString, textBox1.Text + ".Prjparsinevis"), textBox1.Text + "\n" + System.IO.Path.Combine(pathString, "tpfiles")+"\\");
                    System.IO.Path.Combine(pathString, "tpfiles");
                    File.WriteAllText(".\\PrNa.parsinevis", textBox1.Text);
                    File.WriteAllText(".\\PrPa.parsinevis", System.IO.Path.Combine(pathString, "tpfiles") + "\\");
                    File.WriteAllText(".\\NewPr.parsinevis", "1");
                    Form1 frmp = new Form1();
                    frmp.Show();
                    this.Hide();
                    //File.AppendAllText(".\\RCF.parsinevis", textBox1.Text + "\n");
                    //File.AppendAllText(".\\RCB.parsinevis", System.IO.Path.Combine(pathString, textBox1.Text + ".Prjparsinevis")+"\n");
                    File.WriteAllText(".\\RCF.parsinevis", textBox1.Text + "\n" + File.ReadAllText(".\\RCF.parsinevis"));
                    File.WriteAllText(".\\RCB.parsinevis", System.IO.Path.Combine(pathString, textBox1.Text + ".Prjparsinevis") + "\n" + File.ReadAllText(".\\RCB.parsinevis"));
                    STRUP_Load(null, null);
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] opj = File.ReadAllLines(openFileDialog1.FileName);
                for (int i = 0; i < listRF.Items.Count; i++)
                {
                    if (opj[0] == listRF.Items[i].ToString())
                        if (openFileDialog1.FileName == listRB.Items[i].ToString())
                            delrec(i);
                }
                //File.AppendAllText(".\\RCF.parsinevis", opj[0] + "\n");
                //File.AppendAllText(".\\RCB.parsinevis", openFileDialog1.FileName + "\n");
                File.WriteAllText(".\\RCF.parsinevis", opj[0] + "\n" + File.ReadAllText(".\\RCF.parsinevis"));
                File.WriteAllText(".\\RCB.parsinevis", openFileDialog1.FileName + "\n" + File.ReadAllText(".\\RCB.parsinevis"));
                STRUP_Load(null, null);
            }
        }

        void delrec(int ind)
        {
            File.WriteAllText(".\\RCF.parsinevis", "");
            File.WriteAllText(".\\RCB.parsinevis", "");
            for (int i = 0; i < ind; i++)
            {
                File.AppendAllText(".\\RCF.parsinevis", listRF.Items[i].ToString() + "\n");
                File.AppendAllText(".\\RCB.parsinevis", listRB.Items[i].ToString() + "\n");
            }
            for (int i = (ind + 1); i < listRF.Items.Count; i++)
            {
                File.AppendAllText(".\\RCF.parsinevis", listRF.Items[i].ToString() + "\n");
                File.AppendAllText(".\\RCB.parsinevis", listRB.Items[i].ToString() + "\n");
            }
            STRUP_Load(null, null);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            delrec(listRF.SelectedIndex);
        }

        private void ListRF_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listRF.SelectedIndex != -1)
            {
                button2.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Aboutus abfo = new Aboutus();
            abfo.ShowDialog();
        }
    }
}
