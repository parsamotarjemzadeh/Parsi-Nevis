using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;

namespace پارسی_نویس
{
    public partial class Form1 : Form
    {
        string project_name = "";
        string project_path = "";
        string selectedEventText = "";
        bool newPr = false;
        bool canRun = true;
        bool runorsave = true;
        //..........متغیرها
        int coMOT = 0;
        int[] mots = new int[999];
        string[] motName = new string[999];
        int edtMOT = 0;
        //..........
        int coFil = 0;
        //..........
        Control[] objLBL = new Control[500];
        Control[] objBTN = new Control[250];
        Control[] objTXT = new Control[200];
        Control[] objIMG = new Control[150];
        Control[] objCHK = new Control[300];
        //..........
        int coLBL = 0;
        int coBTN = 0;
        int coTXT = 0;
        int coIMG = 0;
        int coCHK = 0;
        //..........
        bool edtLBL = false;
        bool edtBTN = false;
        bool edtTXT = false;
        bool edtIMG = false;
        bool edtCHK = false;
        //..........
        int enoLBL = 0;
        int enoBTN = 0;
        int enoTXT = 0;
        int enoIMG = 0;
        int enoCHK = 0;
        //..........
        string[] nameLBL = new string[500];
        string[] nameBTN = new string[250];
        string[] nameTXT = new string[200];
        string[] nameIMG = new string[150];
        string[] nameCHK = new string[300];
        //..........
        int[,] xyLBL = new int[500, 2];
        int[,] xyBTN = new int[250, 2];
        int[,] xyTXT = new int[200, 2];
        int[,] xyIMG = new int[150, 2];
        int[,] xyCHK = new int[300, 2];
        //..........
        bool[] LBLvis = new bool[500];
        bool[] BTNvis = new bool[250];
        bool[] TXTvis = new bool[200];
        bool[] IMGvis = new bool[150];
        bool[] CHKvis = new bool[300];
        //..........
        public static string EBshow, EBback, EBrname;
        public static bool EBshdeti = false;
        public Form1()
        {
            InitializeComponent();
            project_path = File.ReadAllText(".\\PrPa.parsinevis");
            project_name = File.ReadAllText(".\\PrNa.parsinevis");
            string NewPrtmp = File.ReadAllText(".\\NewPr.parsinevis");
            if (NewPrtmp == "0")
                newPr = false;
            else
                newPr = true;
            if (newPr == true)
            {
                Directory.CreateDirectory(project_path);
                Bitmap bitmapimgsave = (Bitmap)Image.FromFile(".\\DF.png");
                MemoryStream memoryStream = new MemoryStream();
                bitmapimgsave.Save(memoryStream, ImageFormat.Png);
                byte[] bitmapBytes = memoryStream.GetBuffer();
                string bitmapString = Convert.ToBase64String(bitmapBytes, Base64FormattingOptions.InsertLineBreaks);
                string enterlessbitmap = "";
                enterlessbitmap = Regex.Replace(bitmapString, "\r\n", "");
                File.WriteAllText(project_path + "imgPIC0.parsinevis", enterlessbitmap);
                File.WriteAllText(project_path + "frm.parsinevis", "600\n600\n" + ColorTranslator.ToHtml(Color.FromArgb(panelProgram.BackColor.ToArgb())));
            }
            else
            {
                openAll();
            }
        }

        void mainProChangeSize(object sender, EventArgs e)
        {
            panelProgram.Size = new Size(Convert.ToInt32(numFormSizeWidth.Value), Convert.ToInt32(numFormSizeHeight.Value));
        }

        bool nameController(string s)
        {
            bool tmp_co_bool = true;
            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] < 48 || s[i] > 122) tmp_co_bool = false;
                if (s[i] > 90 && s[i] < 97) tmp_co_bool = false;
                if (s[i] > 57 && s[i] < 65) tmp_co_bool = false;
            }
            return tmp_co_bool;
        }

        bool checkNewName(string s, string[] a, bool st, int enoobj)
        {
            bool tmpCheckNewName = true;
            for (int i = 0; i < lbllist.Items.Count; i++) if (lbllist.Items[i].ToString() == s) tmpCheckNewName = false;
            for (int i = 0; i < btnlist.Items.Count; i++) if (btnlist.Items[i].ToString() == s) tmpCheckNewName = false;
            for (int i = 0; i < txtlist.Items.Count; i++) if (txtlist.Items[i].ToString() == s) tmpCheckNewName = false;
            for (int i = 0; i < imglist.Items.Count; i++) if (imglist.Items[i].ToString() == s) tmpCheckNewName = false;
            for (int i = 0; i < chklist.Items.Count; i++) if (chklist.Items[i].ToString() == s) tmpCheckNewName = false;
            for (int i = 0; i < listTmpMOT.Items.Count; i++) if (listTmpMOT.Items[i].ToString() == s) tmpCheckNewName = false;
            if (st == true && a[enoobj] == s) tmpCheckNewName = true;
            return tmpCheckNewName;
        }
        void labelErrors()
        {
            lblLabelErrors.BackColor = System.Drawing.Color.Red;
            lblLabelErrors.ForeColor = System.Drawing.Color.White;
            btnNewLabel.Enabled = false;
            if (txtLblName.Text == "") lblLabelErrors.Text = "نام نمی‌تواند خالی باشد.";
            else if (txtLblName.Text.Length > 15) lblLabelErrors.Text = "نام حداکثر می‌تواند 15 حرف داشته باشد.";
            else if (txtLblName.Text[0] < 65 || txtLblName.Text[0] > 122) lblLabelErrors.Text = "نام فقط با حروف لاتین شروع می‌شود.";
            else if (txtLblName.Text[0] > 90 && txtLblName.Text[0] < 97) lblLabelErrors.Text = "نام فقط با حروف لاتین شروع می‌شود.";
            else if (nameController(txtLblName.Text) == false) lblLabelErrors.Text = "در نام می‌توان فقط از حروف لاتین و اعداد استفاده کرد.";
            else if (checkNewName(txtLblName.Text, nameLBL, edtLBL, enoLBL) == false) lblLabelErrors.Text = "این نام قبلا استفاده شده است.";
            else if (txtLblText.Text == "") lblLabelErrors.Text = "متن نمی‌تواند خالی باشد.";
            else if (txtLblText.Text.Length > 25) lblLabelErrors.Text = "متن حد اکثر می‌تواند از 25 حرف تشکیل شود.";

            else
            {
                lblLabelErrors.BackColor = System.Drawing.Color.LightGreen;
                lblLabelErrors.ForeColor = System.Drawing.Color.Black;
                lblLabelErrors.Text = "با کلیک بر روی دکمه زیر، تغییرات اعمال می‌شود.";
                btnNewLabel.Enabled = true;
            }

        }
        void BtnErrors()
        {
            btnlblerrors.BackColor = System.Drawing.Color.Red;
            btnlblerrors.ForeColor = System.Drawing.Color.White;
            btnNewbtn.Enabled = false;
            if (txtbtnName.Text == "") btnlblerrors.Text = "نام نمی‌تواند خالی باشد.";
            else if (txtbtnName.Text.Length > 15) btnlblerrors.Text = "نام حداکثر می‌تواند 15 حرف داشته باشد.";
            else if (txtbtnName.Text[0] < 65 || txtbtnName.Text[0] > 122) btnlblerrors.Text = "نام فقط با حروف لاتین شروع می‌شود.";
            else if (txtbtnName.Text[0] > 90 && txtbtnName.Text[0] < 97) btnlblerrors.Text = "نام فقط با حروف لاتین شروع می‌شود.";
            else if (nameController(txtbtnName.Text) == false) btnlblerrors.Text = "در نام می‌توان فقط از حروف لاتین و اعداد استفاده کرد.";
            else if (checkNewName(txtbtnName.Text, nameBTN, edtBTN, enoBTN) == false) btnlblerrors.Text = "این نام قبلا استفاده شده است.";
            else if (txtbtnText.Text == "") btnlblerrors.Text = "متن نمی‌تواند خالی باشد.";
            else if (txtbtnText.Text.Length > 25) btnlblerrors.Text = "متن حد اکثر می‌تواند از 25 حرف تشکیل شود.";

            else
            {
                btnlblerrors.BackColor = System.Drawing.Color.LightGreen;
                btnlblerrors.ForeColor = System.Drawing.Color.Black;
                btnlblerrors.Text = "با کلیک بر روی دکمه زیر، تغییرات اعمال می‌شود.";
                btnNewbtn.Enabled = true;
            }
        }
        void TXTErrors()
        {
            txtlblerrors.BackColor = System.Drawing.Color.Red;
            txtlblerrors.ForeColor = System.Drawing.Color.White;
            btnNewtxt.Enabled = false;
            if (txttxtname.Text == "") txtlblerrors.Text = "نام نمی‌تواند خالی باشد.";
            else if (txttxtname.Text.Length > 15) txtlblerrors.Text = "نام حداکثر می‌تواند 15 حرف داشته باشد.";
            else if (txttxtname.Text[0] < 65 || txttxtname.Text[0] > 122) txtlblerrors.Text = "نام فقط با حروف لاتین شروع می‌شود.";
            else if (txttxtname.Text[0] > 90 && txttxtname.Text[0] < 97) txtlblerrors.Text = "نام فقط با حروف لاتین شروع می‌شود.";
            else if (nameController(txttxtname.Text) == false) txtlblerrors.Text = "در نام می‌توان فقط از حروف لاتین و اعداد استفاده کرد.";
            else if (checkNewName(txttxtname.Text, nameTXT, edtTXT, enoTXT) == false) txtlblerrors.Text = "این نام قبلا استفاده شده است.";
            else if (txttxttext.Text.Length > 25) txtlblerrors.Text = "متن حد اکثر می‌تواند از 25 حرف تشکیل شود.";

            else
            {
                txtlblerrors.BackColor = System.Drawing.Color.LightGreen;
                txtlblerrors.ForeColor = System.Drawing.Color.Black;
                txtlblerrors.Text = "با کلیک بر روی دکمه زیر، تغییرات اعمال می‌شود.";
                btnNewtxt.Enabled = true;
            }
        }
        void IMGErrors()
        {
            imglblerrors.BackColor = System.Drawing.Color.Red;
            imglblerrors.ForeColor = System.Drawing.Color.White;
            btnNewimg.Enabled = false;
            if (txtimgName.Text == "") imglblerrors.Text = "نام نمی‌تواند خالی باشد.";
            else if (txtimgName.Text.Length > 15) imglblerrors.Text = "نام حداکثر می‌تواند 15 حرف داشته باشد.";
            else if (txtimgName.Text[0] < 65 || txtimgName.Text[0] > 122) imglblerrors.Text = "نام فقط با حروف لاتین شروع می‌شود.";
            else if (txtimgName.Text[0] > 90 && txtimgName.Text[0] < 97) imglblerrors.Text = "نام فقط با حروف لاتین شروع می‌شود.";
            else if (nameController(txtimgName.Text) == false) imglblerrors.Text = "در نام می‌توان فقط از حروف لاتین و اعداد استفاده کرد.";
            else if (checkNewName(txtimgName.Text, nameIMG, edtIMG, enoIMG) == false) imglblerrors.Text = "این نام قبلا استفاده شده است.";

            else
            {
                imglblerrors.BackColor = System.Drawing.Color.LightGreen;
                imglblerrors.ForeColor = System.Drawing.Color.Black;
                imglblerrors.Text = "با کلیک بر روی دکمه زیر، تغییرات اعمال می‌شود.";
                btnNewimg.Enabled = true;
            }
        }
        void CHKErrors()
        {
            chklblerrors.BackColor = System.Drawing.Color.Red;
            chklblerrors.ForeColor = System.Drawing.Color.White;
            btnNewchk.Enabled = false;
            if (txtchkname.Text == "") chklblerrors.Text = "نام نمی‌تواند خالی باشد.";
            else if (txtchkname.Text.Length > 15) chklblerrors.Text = "نام حداکثر می‌تواند 15 حرف داشته باشد.";
            else if (txtchkname.Text[0] < 65 || txtchkname.Text[0] > 122) chklblerrors.Text = "نام فقط با حروف لاتین شروع می‌شود.";
            else if (txtchkname.Text[0] > 90 && txtchkname.Text[0] < 97) chklblerrors.Text = "نام فقط با حروف لاتین شروع می‌شود.";
            else if (nameController(txtchkname.Text) == false) chklblerrors.Text = "در نام می‌توان فقط از حروف لاتین و اعداد استفاده کرد.";
            else if (checkNewName(txtchkname.Text, nameCHK, edtCHK, enoCHK) == false) chklblerrors.Text = "این نام قبلا استفاده شده است.";
            else if (txtchkText.Text.Length > 25) chklblerrors.Text = "متن حد اکثر می‌تواند از 25 حرف تشکیل شود.";

            else
            {
                chklblerrors.BackColor = System.Drawing.Color.LightGreen;
                chklblerrors.ForeColor = System.Drawing.Color.Black;
                chklblerrors.Text = "با کلیک بر روی دکمه زیر، تغییرات اعمال می‌شود.";
                btnNewchk.Enabled = true;
            }
        }
        void callerrors()
        {
            labelErrors();
            BtnErrors();
            TXTErrors();
            IMGErrors();
            CHKErrors();
            MOTerrors();
            ChangeMOTerrors();
            IFstateChangeErrors(null, null);
            WHILEstateChangeErrors(null, null);
            toValueErrors(null, null);
            objShowTabErrors(null, null);
            ListMainProgram_SelectedIndexChanged(null, null);
            UpdateEventsList();
            CheckHolePrErrors();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Motalllist_SelectedIndexChanged(null, null);
            callerrors();
            LoadEventCode();
        }
        int nameTOint(string s)
        {
            if (s.Length == 4) return (Convert.ToInt32((s[3]).ToString()));
            else if (s.Length == 5) return (Convert.ToInt32((s[3]+s[4]).ToString()));
            else return (Convert.ToInt32((s[3]+s[4]+s[5]).ToString()));
        }

        void LBLedit(int no)
        {
            edtLBL = false;
            btnNewLabel.Text = "ذخیره تغییرات";
            btnDeleteLBL.Visible = true;
            label6.Text = "تغییر برچسب ";
            txtLblName.Text = nameLBL[no];
            txtLblText.Text = objLBL[no].Text;
            piclabelBackColor.BackColor = objLBL[no].BackColor;
            piclabelForeColor.BackColor = objLBL[no].ForeColor;
            numLabelwidth.Value = xyLBL[no, 0];
            numLabelHeight.Value = xyLBL[no, 1];
            edtLBL = true;
            labelErrors();
        }

        void BTNedit(int no)
        {
            edtBTN = false;
            btnNewbtn.Text = "ذخیره تغییرات";
            btnDeleteBtn.Visible = true;
            label23.Text = "تغییر دکمه ";
            txtbtnName.Text = nameBTN[no];
            txtbtnText.Text = objBTN[no].Text;
            picbtnBackcolor.BackColor = objBTN[no].BackColor;
            picbtnForecolor.BackColor = objBTN[no].ForeColor;
            numbtnWidth.Value = xyBTN[no, 0];
            numbtnHeight.Value = xyBTN[no, 1];
            numbtnsize0.Value = objBTN[no].Size.Width;
            numbtnsize1.Value = objBTN[no].Size.Height;
            edtBTN = true;
            BtnErrors();
        }

        void TXTedit(int no)
        {
            edtTXT = false;
            btnNewtxt.Text = "ذخیره تغییرات";
            btnDeleteTxt.Visible = true;
            label44.Text = "تغییر جعبه متنی ";
            txttxtname.Text = nameTXT[no];
            txttxttext.Text = objTXT[no].Text;
            pictxtBackColor.BackColor = objTXT[no].BackColor;
            pictxtForeColor.BackColor = objTXT[no].ForeColor;
            numtxtWidth.Value = xyTXT[no, 0];
            numtxtHeight.Value = xyTXT[no, 1];
            numtxtsize.Value = objTXT[no].Size.Width;
            edtTXT = true;
            TXTErrors();
        }

        void IMGedit(int no)
        {
            edtIMG = false;
            btnNewimg.Text = "ذخیره تغییرات";
            btnDeleteImg.Visible = true;
            label57.Text = "تغییر تصویر ";
            txtimgName.Text = nameIMG[no];
            numimgWidth.Value = xyIMG[no, 0];
            numimgHeight.Value = xyIMG[no, 1];
            numimgsize0.Value = objIMG[no].Size.Width;
            numimgsize1.Value = objIMG[no].Size.Height;
            picimg.Image = ((PictureBox)objIMG[no]).Image;
            if (((PictureBox)objIMG[no]).SizeMode == PictureBoxSizeMode.StretchImage) chkimglock.Checked = false;
            else chkimglock.Checked = true;
            edtIMG = true;
            IMGErrors();
        }

        void CHKedit(int no)
        {
            edtCHK = false;
            btnNewchk.Text = "ذخیره تغییرات";
            btnDeleteChk.Visible = true;
            label68.Text = "تغییر جعبه گزینه ";
            txtchkname.Text = nameCHK[no];
            txtchkText.Text = objCHK[no].Text;
            numchkWidth.Value = xyCHK[no, 0];
            numchkHeight.Value = xyCHK[no, 1];
            radchkfalse.Checked = false;
            radchktrue.Checked = false;
            if (((CheckBox)objCHK[no]).Checked == true) radchktrue.Checked = true;
            else radchkfalse.Checked = true;
            edtCHK = true;
            CHKErrors();
        }

        void NewMode()
        {
            edtLBL = false;
            edtBTN = false;
            edtTXT = false;
            edtIMG = false;
            edtCHK = false;

            btnNewLabel.Text = "ایجاد برچسب جدید";
            btnNewbtn.Text = "ایجاد دکمه جدید";
            btnNewtxt.Text = "ایجاد جعبه متنی جدید";
            btnNewimg.Text = "ایجاد تصویر جدید";
            btnNewchk.Text = "ایجاد جعبه گزینه جدید";

            btnDeleteLBL.Visible = false;
            btnDeleteBtn.Visible = false;
            btnDeleteTxt.Visible = false;
            btnDeleteImg.Visible = false;
            btnDeleteChk.Visible = false;

            label6.Text = "برچسب جدید";
            label23.Text = "دکمه جدید";
            label44.Text = "جعبه متنی جدید";
            label57.Text = "تصویر جدید";
            label68.Text = "جعبه گزینه جدید";

            numLabelHeight.Value = 20;
            numLabelwidth.Value = 20;
            numbtnHeight.Value = 20;
            numbtnWidth.Value = 20;
            numbtnsize0.Value = 20;
            numbtnsize1.Value = 20;
            numtxtHeight.Value = 20;
            numtxtWidth.Value = 20;
            numtxtsize.Value = 100;
            numimgHeight.Value = 20;
            numimgWidth.Value = 20;
            numimgsize0.Value = 50;
            numimgsize1.Value = 50;
            numchkHeight.Value = 20;
            numchkWidth.Value = 20;

            txtLblName.Text = "";
            txtLblText.Text = "";
            txtbtnName.Text = "";
            txtbtnText.Text = "";
            txttxtname.Text = "";
            txttxttext.Text = "";
            txtimgName.Text = "";
            txtchkname.Text = "";
            txtchkText.Text = "";

            piclabelBackColor.BackColor = System.Drawing.Color.White;
            piclabelForeColor.BackColor = System.Drawing.Color.Black;
            picbtnBackcolor.BackColor = System.Drawing.Color.LightGray;
            picbtnForecolor.BackColor = System.Drawing.Color.Black;
            pictxtBackColor.BackColor = System.Drawing.Color.White;
            pictxtForeColor.BackColor = System.Drawing.Color.Black;

            chkimglock.Checked = true;
            picimg.Image = پارسی_نویس.Properties.Resources.DF;
            radchkfalse.Checked = false;
            radchktrue.Checked = true;

            callerrors();

            comboTabViOBJmain.Items.Clear();
            for (int i = 0; i < lbllist.Items.Count; i++) comboTabViOBJmain.Items.Add(lbllist.Items[i].ToString());
            for (int i = 0; i < btnlist.Items.Count; i++) comboTabViOBJmain.Items.Add(btnlist.Items[i].ToString());
            for (int i = 0; i < txtlist.Items.Count; i++) comboTabViOBJmain.Items.Add(txtlist.Items[i].ToString());
            for (int i = 0; i < imglist.Items.Count; i++) comboTabViOBJmain.Items.Add(imglist.Items[i].ToString());
            for (int i = 0; i < chklist.Items.Count; i++) comboTabViOBJmain.Items.Add(chklist.Items[i].ToString());

            saveAll();
            ComboSelectEvent_TextChanged(null, null);
            addLISTval();
        }

        void seLBL(object sender, EventArgs e)
        {
            tabToolbox.SelectedTab = tabToolLabel;
            enoLBL = nameTOint(((Label)sender).Name);
            LBLedit(enoLBL);
        }
        void seBTN(object sender, EventArgs e)
        {
            tabToolbox.SelectedTab = tabToolButton;
            enoBTN = nameTOint(((Button)sender).Name);
            BTNedit(enoBTN);
        }
        void seTXT(object sender, EventArgs e)
        {
            tabToolbox.SelectedTab = tabToolTextBox;
            enoTXT = nameTOint(((TextBox)sender).Name);
            TXTedit(enoTXT);
        }
        void seIMG(object sender, EventArgs e)
        {
            tabToolbox.SelectedTab = tabToolImageBox;
            enoIMG = nameTOint(((PictureBox)sender).Name);
            IMGedit(enoIMG);
        }
        void seCHK(object sender, EventArgs e)
        {
            tabToolbox.SelectedTab = tabToolCheckBox;
            enoCHK = nameTOint(((CheckBox)sender).Name);
            CHKedit(enoCHK);
        }

        void saveLBL()
        {
            xyLBL[enoLBL, 0] = Convert.ToInt32(numLabelwidth.Value);
            xyLBL[enoLBL, 1] = Convert.ToInt32(numLabelHeight.Value);
            objLBL[enoLBL].Location = new Point(xyLBL[enoLBL, 0], xyLBL[enoLBL, 1]);
            objLBL[enoLBL].BackColor = piclabelBackColor.BackColor;
            objLBL[enoLBL].ForeColor = piclabelForeColor.BackColor;
        }
        void saveBTN()
        {
            xyBTN[enoBTN, 0] = Convert.ToInt32(numbtnWidth.Value);
            xyBTN[enoBTN, 1] = Convert.ToInt32(numbtnHeight.Value);
            objBTN[enoBTN].Location = new Point(xyBTN[enoBTN, 0], xyBTN[enoBTN, 1]);
            objBTN[enoBTN].BackColor = picbtnBackcolor.BackColor;
            objBTN[enoBTN].ForeColor = picbtnForecolor.BackColor;
            objBTN[enoBTN].Size = new Size(Convert.ToInt32(numbtnsize0.Value), Convert.ToInt32(numbtnsize1.Value));
        }
        void saveTXT()
        {
            xyTXT[enoTXT, 0] = Convert.ToInt32(numtxtWidth.Value);
            xyTXT[enoTXT, 1] = Convert.ToInt32(numtxtHeight.Value);
            objTXT[enoTXT].Location = new Point(xyTXT[enoTXT, 0], xyTXT[enoTXT, 1]);
            objTXT[enoTXT].BackColor = pictxtBackColor.BackColor;
            objTXT[enoTXT].ForeColor = pictxtForeColor.BackColor;
            objTXT[enoTXT].Size = new Size(Convert.ToInt32(numtxtsize.Value),20);
        }
        void saveIMG()
        {
            xyIMG[enoIMG, 0] = Convert.ToInt32(numimgWidth.Value);
            xyIMG[enoIMG, 1] = Convert.ToInt32(numimgHeight.Value);
            objIMG[enoIMG].Location = new Point(xyIMG[enoIMG, 0], xyIMG[enoIMG, 1]);
            ((PictureBox)objIMG[enoIMG]).Image = picimg.Image;
            ((PictureBox)objIMG[enoIMG]).SizeMode = picimg.SizeMode;
            objIMG[enoIMG].Size = new Size(Convert.ToInt32(numimgsize0.Value), Convert.ToInt32(numimgsize1.Value));
        }
        void saveCHK()
        {
            xyCHK[enoCHK, 0] = Convert.ToInt32(numchkWidth.Value);
            xyCHK[enoCHK, 1] = Convert.ToInt32(numchkHeight.Value);
            objCHK[enoCHK].Location = new Point(xyCHK[enoCHK, 0], xyCHK[enoCHK, 1]);
            ((CheckBox)objCHK[enoCHK]).Checked = radchktrue.Checked;
        }

        void saveAll()
        {
            File.WriteAllText(project_path + "frm.parsinevis", numFormSizeWidth.Value.ToString() + "\n" + numFormSizeHeight.Value.ToString() + "\n" + ColorTranslator.ToHtml(Color.FromArgb(panelProgram.BackColor.ToArgb())));
            int i = 0;
            string tmpSTRvis = "";
            for (i = 0; i < coLBL; i++)
            {
                if (LBLvis[i] == true) tmpSTRvis += "1";
                else tmpSTRvis += "0";
            }
            File.WriteAllText(project_path + "LBLvis.parsinevis", tmpSTRvis);

            tmpSTRvis = "";
            for (i = 0; i < coBTN; i++)
            {
                if (BTNvis[i] == true) tmpSTRvis += "1";
                else tmpSTRvis += "0";
            }
            File.WriteAllText(project_path + "BTNvis.parsinevis", tmpSTRvis);

            tmpSTRvis = "";
            for (i = 0; i < coTXT; i++)
            {
                if (TXTvis[i] == true) tmpSTRvis += "1";
                else tmpSTRvis += "0";
            }
            File.WriteAllText(project_path + "TXTvis.parsinevis", tmpSTRvis);

            tmpSTRvis = "";
            for (i = 0; i < coIMG; i++)
            {
                if (IMGvis[i] == true) tmpSTRvis += "1";
                else tmpSTRvis += "0";
            }
            File.WriteAllText(project_path + "IMGvis.parsinevis", tmpSTRvis);

            tmpSTRvis = "";
            for (i = 0; i < coCHK; i++)
            {
                if (CHKvis[i] == true) tmpSTRvis += "1";
                else tmpSTRvis += "0";
            }
            File.WriteAllText(project_path + "CHKvis.parsinevis", tmpSTRvis);

            tmpSTRvis = "";
            for (i = 0; i < coMOT; i++)
            {
                if (mots[i] == 3) tmpSTRvis += "0";
                else tmpSTRvis += "1";
            }
            File.WriteAllText(project_path + "MOTvis.parsinevis", tmpSTRvis);

            string fileNameforSave = "LBL";
            for (i = 0; i < coLBL; i++)
            {
                if (LBLvis[i] == true)
                {
                    File.WriteAllText(project_path + fileNameforSave + i.ToString() + ".parsinevis",
                        nameLBL[i] + "\n" +
                        objLBL[i].Text + "\n" +
                        xyLBL[i, 0].ToString() + "\n" +
                        xyLBL[i, 1].ToString() + "\n" +
                        ColorTranslator.ToHtml(Color.FromArgb(objLBL[i].BackColor.ToArgb())) + "\n" +
                        ColorTranslator.ToHtml(Color.FromArgb(objLBL[i].ForeColor.ToArgb())));
                }
            }

            fileNameforSave = "BTN";
            for (i = 0; i < coBTN; i++)
            {
                if (BTNvis[i] == true)
                {
                    File.WriteAllText(project_path + fileNameforSave + i.ToString() + ".parsinevis",
                        nameBTN[i] + "\n" +
                        objBTN[i].Text + "\n" +
                        xyBTN[i, 0].ToString() + "\n" +
                        xyBTN[i, 1].ToString() + "\n" +
                        objBTN[i].Size.Width.ToString() + "\n" +
                        objBTN[i].Size.Height.ToString() + "\n" +
                        ColorTranslator.ToHtml(Color.FromArgb(objBTN[i].BackColor.ToArgb())) + "\n" +
                        ColorTranslator.ToHtml(Color.FromArgb(objBTN[i].ForeColor.ToArgb())));
                }
            }

            fileNameforSave = "TXT";
            for (i = 0; i < coTXT; i++)
            {
                if (TXTvis[i] == true)
                {
                    File.WriteAllText(project_path + fileNameforSave + i.ToString() + ".parsinevis",
                        nameTXT[i] + "\n" +
                        objTXT[i].Text + "\n" +
                        xyTXT[i, 0].ToString() + "\n" +
                        xyTXT[i, 1].ToString() + "\n" +
                        objTXT[i].Size.Width.ToString() + "\n" +
                        ColorTranslator.ToHtml(Color.FromArgb(objTXT[i].BackColor.ToArgb())) + "\n" +
                        ColorTranslator.ToHtml(Color.FromArgb(objTXT[i].ForeColor.ToArgb())));
                }
            }

            fileNameforSave = "IMG";
            for (i = 0; i < coIMG; i++)
            {
                if (IMGvis[i] == true)
                {
                    int imgsavetmpSizemode = 0;
                    if (((PictureBox)objIMG[i]).SizeMode == PictureBoxSizeMode.StretchImage) imgsavetmpSizemode = 1;

                    File.WriteAllText(project_path + fileNameforSave + i.ToString() + ".parsinevis",
                        nameIMG[i] + "\n" +
                        xyIMG[i, 0].ToString() + "\n" +
                        xyIMG[i, 1].ToString() + "\n" +
                        objIMG[i].Size.Width.ToString() + "\n" +
                        objIMG[i].Size.Height.ToString() + "\n" +
                        imgsavetmpSizemode.ToString());
                }
            }

            fileNameforSave = "CHK";
            for (i = 0; i < coCHK; i++)
            {
                if (CHKvis[i] == true)
                {
                    int chkchecksavetmp = 0;
                    if (((CheckBox)objCHK[i]).Checked == true) chkchecksavetmp = 1;

                    File.WriteAllText(project_path + fileNameforSave + i.ToString() + ".parsinevis",
                        nameCHK[i] + "\n" +
                        objCHK[i].Text + "\n" +
                        xyCHK[i, 0].ToString() + "\n" +
                        xyCHK[i, 1].ToString() + "\n" +
                        chkchecksavetmp.ToString());
                }
            }

            fileNameforSave = "MOT";
            for (i = 0; i < coMOT; i++)
            {
                File.WriteAllText(project_path + fileNameforSave + i.ToString() + ".parsinevis", mots[i].ToString() + "\n" + motName[i]);
            }

            txtSaveAllTmp.Text = "";
            for (i = 0; i < lbllist.Items.Count; i++)
            {
                if (i != 0) txtSaveAllTmp.AppendText("\n");
                txtSaveAllTmp.AppendText(lbllist.Items[i].ToString());
            }
            File.WriteAllText(project_path + "lbllist.parsinevis", txtSaveAllTmp.Text);

            txtSaveAllTmp.Text = "";
            for (i = 0; i < btnlist.Items.Count; i++)
            {
                if (i != 0) txtSaveAllTmp.AppendText("\n");
                txtSaveAllTmp.AppendText(btnlist.Items[i].ToString());
            }
            File.WriteAllText(project_path + "btnlist.parsinevis", txtSaveAllTmp.Text);

            txtSaveAllTmp.Text = "";
            for (i = 0; i < txtlist.Items.Count; i++)
            {
                if (i != 0) txtSaveAllTmp.AppendText("\n");
                txtSaveAllTmp.AppendText(txtlist.Items[i].ToString());
            }
            File.WriteAllText(project_path + "txtlist.parsinevis", txtSaveAllTmp.Text);

            txtSaveAllTmp.Text = "";
            for (i = 0; i < imglist.Items.Count; i++)
            {
                if (i != 0) txtSaveAllTmp.AppendText("\n");
                txtSaveAllTmp.AppendText(imglist.Items[i].ToString());
            }
            File.WriteAllText(project_path + "imglist.parsinevis", txtSaveAllTmp.Text);

            txtSaveAllTmp.Text = "";
            for (i = 0; i < chklist.Items.Count; i++)
            {
                if (i != 0) txtSaveAllTmp.AppendText("\n");
                txtSaveAllTmp.AppendText(chklist.Items[i].ToString());
            }
            File.WriteAllText(project_path + "chklist.parsinevis", txtSaveAllTmp.Text);

            txtSaveAllTmp.Text = "";
            for (i = 0; i < listTmpMOT.Items.Count; i++)
            {
                if (i != 0) txtSaveAllTmp.AppendText("\n");
                txtSaveAllTmp.AppendText(listTmpMOT.Items[i].ToString());
            }
            File.WriteAllText(project_path + "motlist.parsinevis", txtSaveAllTmp.Text);

            File.WriteAllText(project_path + "co.parsinevis", coLBL.ToString() + "\n" + coBTN.ToString() + "\n" + coTXT.ToString() + "\n" + coIMG.ToString() + "\n" + coCHK.ToString() + "\n" + coMOT.ToString());

            txtSaveAllTmp.Text = "";
            for(i = 0; i < coLBL; i++)
            {
                if (LBLvis[i] == true) txtSaveAllTmp.AppendText("1");
                else txtSaveAllTmp.AppendText("0");
            }
            File.WriteAllText(project_path + "LBLvis.parsinevis", txtSaveAllTmp.Text);

            txtSaveAllTmp.Text = "";
            for (i = 0; i < coBTN; i++)
            {
                if (BTNvis[i] == true) txtSaveAllTmp.AppendText("1");
                else txtSaveAllTmp.AppendText("0");
            }
            File.WriteAllText(project_path + "BTNvis.parsinevis", txtSaveAllTmp.Text);

            txtSaveAllTmp.Text = "";
            for (i = 0; i < coTXT; i++)
            {
                if (TXTvis[i] == true) txtSaveAllTmp.AppendText("1");
                else txtSaveAllTmp.AppendText("0");
            }
            File.WriteAllText(project_path + "TXTvis.parsinevis", txtSaveAllTmp.Text);

            txtSaveAllTmp.Text = "";
            for (i = 0; i < coIMG; i++)
            {
                if (IMGvis[i] == true) txtSaveAllTmp.AppendText("1");
                else txtSaveAllTmp.AppendText("0");
            }
            File.WriteAllText(project_path + "IMGvis.parsinevis", txtSaveAllTmp.Text);

            txtSaveAllTmp.Text = "";
            for (i = 0; i < coCHK; i++)
            {
                if (CHKvis[i] == true) txtSaveAllTmp.AppendText("1");
                else txtSaveAllTmp.AppendText("0");
            }
            File.WriteAllText(project_path + "CHKvis.parsinevis", txtSaveAllTmp.Text);
        }

        void openAll()
        {
            int i = 0;
            string[] gt = new string[6];
            gt = File.ReadAllLines(project_path + "co.parsinevis");
            coLBL = Convert.ToInt32(gt[0]);
            coBTN = Convert.ToInt32(gt[1]);
            coTXT = Convert.ToInt32(gt[2]);
            coIMG = Convert.ToInt32(gt[3]);
            coCHK = Convert.ToInt32(gt[4]);
            coMOT = Convert.ToInt32(gt[5]);

            string opTMP = "";
            txtOpenAllTmp.Text = "";
            string[] sst = new string[12];

            sst = File.ReadAllLines(project_path + "frm.parsinevis");
            numFormSizeWidth.Value = Convert.ToInt32(sst[0]);
            numFormSizeHeight.Value = Convert.ToInt32(sst[1]);
            picFormBackColor.BackColor = ColorTranslator.FromHtml(sst[2]);
            panelProgram.BackColor = ColorTranslator.FromHtml(sst[2]);

            opTMP = File.ReadAllText(project_path + "LBLvis.parsinevis");
            for (i = 0; i < coLBL; i++)
            {
                if (opTMP[i].ToString() == "1")
                {
                    sst = File.ReadAllLines(project_path + "LBL" + i.ToString() + ".parsinevis");
                    LBLvis[i] = true;
                    nameLBL[i] = sst[0];
                    objLBL[i] = new Label();
                    objLBL[i].Click += seLBL;
                    ((Label)objLBL[i]).AutoSize = true;
                    treeView1.Nodes[0].Nodes.Add(nameLBL[i], nameLBL[i]);
                    objLBL[i].Name = "LBL" + i.ToString();
                    objLBL[i].Text = sst[1];
                    xyLBL[i, 0] = Convert.ToInt32(sst[2]);
                    xyLBL[i, 1] = Convert.ToInt32(sst[3]);
                    objLBL[i].Location = new Point(xyLBL[i, 0], xyLBL[i, 1]);
                    objLBL[i].BackColor = ColorTranslator.FromHtml(sst[4]);
                    objLBL[i].ForeColor = ColorTranslator.FromHtml(sst[5]);
                    panelProgram.Controls.Add(objLBL[i]);
                }
                else
                {
                    nameLBL[i] = "-1";
                    LBLvis[i] = false;
                }
            }

            opTMP = File.ReadAllText(project_path + "BTNvis.parsinevis");
            for (i = 0; i < coBTN; i++)
            {
                if (opTMP[i].ToString() == "1")
                {
                    sst = File.ReadAllLines(project_path + "BTN" + i.ToString() + ".parsinevis");
                    BTNvis[i] = true;
                    nameBTN[i] = sst[0];
                    objBTN[i] = new Button();
                    objBTN[i].Click += seBTN;
                    treeView1.Nodes[1].Nodes.Add(nameBTN[i], nameBTN[i]);
                    objBTN[i].Name = "BTN" + i.ToString();
                    objBTN[i].Text = sst[1];
                    xyBTN[i, 0] = Convert.ToInt32(sst[2]);
                    xyBTN[i, 1] = Convert.ToInt32(sst[3]);
                    objBTN[i].Location = new Point(xyBTN[i, 0], xyBTN[i, 1]);
                    objBTN[i].Size = new Size(Convert.ToInt32(sst[4]), Convert.ToInt32(sst[5]));
                    objBTN[i].BackColor = ColorTranslator.FromHtml(sst[6]);
                    objBTN[i].ForeColor = ColorTranslator.FromHtml(sst[7]);
                    panelProgram.Controls.Add(objBTN[i]);
                }
                else
                {
                    nameBTN[i] = "-1";
                    BTNvis[i] = false;
                }
            }

            opTMP = File.ReadAllText(project_path + "TXTvis.parsinevis");
            for (i = 0; i < coTXT; i++)
            {
                if (opTMP[i].ToString() == "1")
                {
                    sst = File.ReadAllLines(project_path + "TXT" + i.ToString() + ".parsinevis");
                    TXTvis[i] = true;
                    nameTXT[i] = sst[0];
                    objTXT[i] = new TextBox();
                    objTXT[i].Click += seTXT;
                    treeView1.Nodes[2].Nodes.Add(nameTXT[i], nameTXT[i]);
                    objTXT[i].Name = "TXT" + i.ToString();
                    objTXT[i].Text = sst[1];
                    xyTXT[i, 0] = Convert.ToInt32(sst[2]);
                    xyTXT[i, 1] = Convert.ToInt32(sst[3]);
                    objTXT[i].Location = new Point(xyTXT[i, 0], xyTXT[i, 1]);
                    objTXT[i].Size = new Size(Convert.ToInt32(sst[4]), 20);
                    objTXT[i].BackColor = ColorTranslator.FromHtml(sst[5]);
                    objTXT[i].ForeColor = ColorTranslator.FromHtml(sst[6]);
                    ((TextBox)objTXT[i]).ReadOnly = true;
                    panelProgram.Controls.Add(objTXT[i]);
                }
                else
                {
                    nameTXT[i] = "-1";
                    TXTvis[i] = false;
                }
            }

            opTMP = File.ReadAllText(project_path + "IMGvis.parsinevis");
            for (i = 0; i < coIMG; i++)
            {
                if (opTMP[i].ToString() == "1")
                {
                    sst = File.ReadAllLines(project_path + "IMG" + i.ToString() + ".parsinevis");
                    IMGvis[i] = true;
                    nameIMG[i] = sst[0];
                    objIMG[i] = new PictureBox();
                    objIMG[i].Click += seIMG;
                    treeView1.Nodes[3].Nodes.Add(nameIMG[i], nameIMG[i]);
                    objIMG[i].Name = "IMG" + i.ToString();
                    xyIMG[i, 0] = Convert.ToInt32(sst[1]);
                    xyIMG[i, 1] = Convert.ToInt32(sst[2]);
                    objIMG[i].Location = new Point(xyIMG[i, 0], xyIMG[i, 1]);
                    objIMG[i].Size = new Size(Convert.ToInt32(sst[3]), Convert.ToInt32(sst[4]));
                    if (Convert.ToInt32(sst[5]) == 1)
                        ((PictureBox)objIMG[i]).SizeMode = PictureBoxSizeMode.StretchImage;
                    else
                        ((PictureBox)objIMG[i]).SizeMode = PictureBoxSizeMode.Zoom;
                    byte[] bitmapBytes111 = Convert.FromBase64String(File.ReadAllText(project_path + "imgPIC" + i + ".parsinevis"));
                    MemoryStream memoryStream111 = new MemoryStream(bitmapBytes111);
                    Image image111 = Image.FromStream(memoryStream111);
                    ((PictureBox)objIMG[i]).Image = image111;
                    panelProgram.Controls.Add(objIMG[i]);
                }
                else
                {
                    nameIMG[i] = "-1";
                    IMGvis[i] = false;
                }
            }

            opTMP = File.ReadAllText(project_path + "CHKvis.parsinevis");
            for (i = 0; i < coCHK; i++)
            {
                if (opTMP[i].ToString() == "1")
                {
                    sst = File.ReadAllLines(project_path + "CHK" + i.ToString() + ".parsinevis");
                    CHKvis[i] = true;
                    nameCHK[i] = sst[0];
                    objCHK[i] = new CheckBox();
                    objCHK[i].Click += seCHK;
                    treeView1.Nodes[4].Nodes.Add(nameCHK[i], nameCHK[i]);
                    objCHK[i].Name = "CHK" + i.ToString();
                    objCHK[i].Text = sst[1];
                    xyCHK[i, 0] = Convert.ToInt32(sst[2]);
                    xyCHK[i, 1] = Convert.ToInt32(sst[3]);
                    objCHK[i].Location = new Point(xyCHK[i, 0], xyCHK[i, 1]);
                    ((CheckBox)objCHK[i]).AutoCheck = false;
                    ((CheckBox)objCHK[i]).AutoSize = true;
                    if (Convert.ToUInt32(sst[4]) == 1)
                        ((CheckBox)objCHK[i]).Checked = true;
                    else
                        ((CheckBox)objCHK[i]).Checked = false;
                    panelProgram.Controls.Add(objCHK[i]);
                }
                else
                {
                    nameCHK[i] = "-1";
                    CHKvis[i] = false;
                }
            }

            for (i = 0; i < coMOT; i++)
            {
                sst = File.ReadAllLines(project_path + "MOT" + i.ToString() + ".parsinevis");
                if (sst[0].ToString() == "0")
                {
                    mots[i] = 0;
                    motName[i] = sst[1];
                }
                else if (sst[0].ToString() == "1")
                {
                    mots[i] = 1;
                    motName[i] = sst[1];
                }
                else if (sst[0].ToString() == "2")
                {
                    mots[i] = 2;
                    motName[i] = sst[1];
                }
                else
                {
                    mots[i] = 3;
                    motName[i] = "-2";
                }
            }
            radMOTchange(null, null);

            string[] lbllistRead = File.ReadAllLines(project_path + "lbllist.parsinevis");
            for (int yy = 0; yy < lbllistRead.Length; yy++) lbllist.Items.Add(lbllistRead[yy]);

            string[] btnlistRead = File.ReadAllLines(project_path + "btnlist.parsinevis");
            for (int yy = 0; yy < btnlistRead.Length; yy++) btnlist.Items.Add(btnlistRead[yy]);

            string[] txtlistRead = File.ReadAllLines(project_path + "txtlist.parsinevis");
            for (int yy = 0; yy < txtlistRead.Length; yy++) txtlist.Items.Add(txtlistRead[yy]);

            string[] imglistRead = File.ReadAllLines(project_path + "imglist.parsinevis");
            for (int yy = 0; yy < imglistRead.Length; yy++) imglist.Items.Add(imglistRead[yy]);

            string[] chklistRead = File.ReadAllLines(project_path + "chklist.parsinevis");
            for (int yy = 0; yy < chklistRead.Length; yy++) chklist.Items.Add(chklistRead[yy]);

            string[] motlistRead = File.ReadAllLines(project_path + "motlist.parsinevis");
            for (int yy = 0; yy < motlistRead.Length; yy++) listTmpMOT.Items.Add(motlistRead[yy]);

            NewMode();
        }
        private void BtnLabelBackColor_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                piclabelBackColor.BackColor = colorDialog1.Color;
                if (edtLBL == true)
                {
                    saveLBL();
                }
            }
        }

        private void BtnFormBackColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                picFormBackColor.BackColor = colorDialog1.Color;
                panelProgram.BackColor = colorDialog1.Color;
            }
            saveAll();
        }

        private void TxtLblName_TextChanged(object sender, EventArgs e)
        {
            labelErrors();
        }

        private void TxtLblText_TextChanged(object sender, EventArgs e)
        {
            labelErrors();
        }

        private void BtnLabelForeColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                piclabelForeColor.BackColor = colorDialog1.Color;
                if (edtLBL == true)
                {
                    saveLBL();
                }
            }
        }

        private void BtnNewLabel_Click(object sender, EventArgs e)
        {
            if (edtLBL == false)
            {
                nameLBL[coLBL] = txtLblName.Text;
                xyLBL[coLBL, 0] = Convert.ToInt32(numLabelwidth.Value);
                xyLBL[coLBL, 1] = Convert.ToInt32(numLabelHeight.Value);
                string tmpnamelbl = "LBL" + coLBL.ToString();
                objLBL[coLBL] = new Label();
                objLBL[coLBL].Name = tmpnamelbl;
                objLBL[coLBL].Text = txtLblText.Text;
                objLBL[coLBL].Location = new Point(xyLBL[coLBL, 0], xyLBL[coLBL, 1]);
                objLBL[coLBL].BackColor = piclabelBackColor.BackColor;
                objLBL[coLBL].ForeColor = piclabelForeColor.BackColor;
                objLBL[coLBL].Click += seLBL;
                ((Label)objLBL[coLBL]).AutoSize = true;
                lbllist.Items.Add(nameLBL[coLBL]);
                //LBLedit(coLBL);
                panelProgram.Controls.Add(objLBL[coLBL]);
                treeView1.Nodes[0].Nodes.Add(nameLBL[coLBL], nameLBL[coLBL]);

                File.WriteAllText(project_path + tmpnamelbl + ".parsinevis",
                    nameLBL[coLBL] + "\n" +
                    txtLblText.Text + "\n" +
                    xyLBL[coLBL, 0].ToString() + "\n" +
                    xyLBL[coLBL, 1].ToString() + "\n" +
                    ColorTranslator.ToHtml(Color.FromArgb(piclabelBackColor.BackColor.ToArgb())) + "\n" +
                    ColorTranslator.ToHtml(Color.FromArgb(piclabelForeColor.BackColor.ToArgb())));
                File.WriteAllText(project_path + tmpnamelbl + "e.parsinevis", "");

                LBLvis[coLBL] = true;
                objLBL[coLBL].BringToFront();
                coLBL++;
                enoLBL = coLBL;
                NewMode();
            }
            else
            {
                for (int i = 0; i < lbllist.Items.Count; i++)
                {
                    if (lbllist.Items[i].ToString() == nameLBL[enoLBL])
                    {
                        treeView1.Nodes[0].Nodes[i].Text = txtLblName.Text;
                        lbllist.Items[i] = txtLblName.Text;
                    }
                }
                nameLBL[enoLBL] = txtLblName.Text;
                objLBL[enoLBL].Text = txtLblText.Text;
                saveLBL();
                NewMode();
            }
            callerrors();
            addLISTval();
        }

        void LBLobjed(object sender, EventArgs e)
        {
            if (edtLBL == true)
            {
                saveLBL();
            }
        }
        void BTNobjed(object sender, EventArgs e)
        {
            if (edtBTN == true)
            {
                saveBTN();
            }
        }
        void TXTobjed(object sender, EventArgs e)
        {
            if (edtTXT == true)
            {
                saveTXT();
            }
        }
        void IMGobjed(object sender, EventArgs e)
        {
            if (edtIMG == true)
            {
                saveIMG();
            }
        }
        void CHKobjed(object sender, EventArgs e)
        {
            if (edtCHK == true)
            {
                saveCHK();
            }
        }

        private void PanelProgram_Paint(object sender, PaintEventArgs e)
        {
        }

        private void PanelProgram_Click(object sender, EventArgs e)
        {
            NewMode();
        }

        private void BtnNewbtn_Click(object sender, EventArgs e)
        {
            if (edtBTN == false)
            {
                nameBTN[coBTN] = txtbtnName.Text;
                xyBTN[coBTN, 0] = Convert.ToInt32(numbtnWidth.Value);
                xyBTN[coBTN, 1] = Convert.ToInt32(numbtnHeight.Value);
                string tmpnamebtn = "BTN" + coBTN.ToString();
                objBTN[coBTN] = new Button();
                objBTN[coBTN].Name = tmpnamebtn;
                objBTN[coBTN].Text = txtbtnText.Text;
                objBTN[coBTN].Location = new Point(xyBTN[coBTN, 0], xyBTN[coBTN, 1]);
                objBTN[coBTN].BackColor = picbtnBackcolor.BackColor;
                objBTN[coBTN].ForeColor = picbtnForecolor.BackColor;
                objBTN[coBTN].Click += seBTN;
                objBTN[coBTN].Size = new Size(Convert.ToInt32(numbtnsize0.Value), Convert.ToInt32(numbtnsize1.Value));
                btnlist.Items.Add(nameBTN[coBTN]);
                //BTNedit(coBTN);
                panelProgram.Controls.Add(objBTN[coBTN]);
                treeView1.Nodes[1].Nodes.Add(nameBTN[coBTN] , nameBTN[coBTN]);

                File.WriteAllText(project_path + tmpnamebtn + ".parsinevis",
                    nameBTN[coBTN] + "\n" +
                    txtbtnText.Text + "\n" +
                    xyBTN[coBTN, 0].ToString() + "\n" +
                    xyBTN[coBTN, 1].ToString() + "\n" +
                    numbtnsize0.Value.ToString() + "\n" +
                    numbtnsize1.Value.ToString() + "\n" +
                    ColorTranslator.ToHtml(Color.FromArgb(picbtnBackcolor.BackColor.ToArgb())) + "\n" +
                    ColorTranslator.ToHtml(Color.FromArgb(picbtnForecolor.BackColor.ToArgb())));
                File.WriteAllText(project_path + tmpnamebtn + "e.parsinevis", "");

                BTNvis[coBTN] = true;
                objBTN[coBTN].BringToFront();
                coBTN++;
                enoBTN = coBTN;
                NewMode();
            }
            else
            {
                for (int i = 0; i < btnlist.Items.Count; i++)
                {
                    if (btnlist.Items[i].ToString() == nameBTN[enoBTN])
                    {
                        treeView1.Nodes[1].Nodes[i].Text = txtbtnName.Text;
                        btnlist.Items[i] = txtbtnName.Text;
                    }
                }
                nameBTN[enoBTN] = txtbtnName.Text;
                objBTN[enoBTN].Text = txtbtnText.Text;
                saveBTN();
                NewMode();
            }
            callerrors();
            addLISTval();
        }

        private void BtnbtnBackcolor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                picbtnBackcolor.BackColor = colorDialog1.Color;
                if (edtBTN == true)
                {
                    saveBTN();
                }
            }
        }

        private void BtnbtnForecolor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                picbtnForecolor.BackColor = colorDialog1.Color;
                if (edtBTN == true)
                {
                    saveBTN();
                }
            }           
        }

        private void TxtbtnName_TextChanged(object sender, EventArgs e)
        {
            BtnErrors();
        }

        private void TxtbtnText_TextChanged(object sender, EventArgs e)
        {
            BtnErrors();
        }

        private void BtntxtBackColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictxtBackColor.BackColor = colorDialog1.Color;
                if (edtTXT == true)
                {
                    saveTXT();
                }
            }
        }

        private void BtntxtForeColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictxtForeColor.BackColor = colorDialog1.Color;
                if (edtTXT == true)
                {
                    saveTXT();
                }
            }
        }

        private void Txttxtname_TextChanged(object sender, EventArgs e)
        {
            TXTErrors();
        }

        private void Txttxttext_TextChanged(object sender, EventArgs e)
        {
            TXTErrors();
        }

        private void BtnNewtxt_Click(object sender, EventArgs e)
        {
            if (edtTXT == false)
            {
                nameTXT[coTXT] = txttxtname.Text;
                xyTXT[coTXT, 0] = Convert.ToInt32(numtxtWidth.Value);
                xyTXT[coTXT, 1] = Convert.ToInt32(numtxtHeight.Value);
                string tmpnametxt = "TXT" + coTXT.ToString();
                objTXT[coTXT] = new TextBox();
                ((TextBox)objTXT[coTXT]).ReadOnly = true;
                objTXT[coTXT].Name = tmpnametxt;
                objTXT[coTXT].Text = txttxttext.Text;
                objTXT[coTXT].Location = new Point(xyTXT[coTXT, 0], xyTXT[coTXT, 1]);
                objTXT[coTXT].BackColor = pictxtBackColor.BackColor;
                objTXT[coTXT].ForeColor = pictxtForeColor.BackColor;
                objTXT[coTXT].Click += seTXT;
                objTXT[coTXT].Size = new Size(Convert.ToInt32(numtxtsize.Value),20);
                txtlist.Items.Add(nameTXT[coTXT]);
                //TXTedit(coTXT);
                panelProgram.Controls.Add(objTXT[coTXT]);
                treeView1.Nodes[2].Nodes.Add(nameTXT[coTXT], nameTXT[coTXT]);

                File.WriteAllText(project_path + tmpnametxt + ".parsinevis",
                    nameTXT[coTXT] + "\n" +
                    txttxttext.Text + "\n" +
                    xyTXT[coTXT, 0].ToString() + "\n" +
                    xyTXT[coTXT, 1].ToString() + "\n" +
                    numtxtsize.Value.ToString() + "\n" +
                    ColorTranslator.ToHtml(Color.FromArgb(pictxtBackColor.BackColor.ToArgb())) + "\n" +
                    ColorTranslator.ToHtml(Color.FromArgb(pictxtForeColor.BackColor.ToArgb())));
                File.WriteAllText(project_path + tmpnametxt + "e.parsinevis", "");

                TXTvis[coTXT] = true;
                objTXT[coTXT].BringToFront();
                coTXT++;
                enoTXT = coTXT;
                NewMode();
            }
            else
            {
                for (int i = 0; i < txtlist.Items.Count; i++)
                {
                    if (txtlist.Items[i].ToString() == nameTXT[enoTXT])
                    {
                        treeView1.Nodes[2].Nodes[i].Text = txttxtname.Text;
                        txtlist.Items[i] = txttxtname.Text;
                    }
                }
                nameTXT[enoTXT] = txttxtname.Text;
                objTXT[enoTXT].Text = txttxttext.Text;
                saveTXT();
                NewMode();
            }
            callerrors();
            addLISTval();
        }

        private void BtnChooseimg_Click(object sender, EventArgs e)
        {
            if(openimgdlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string imgFileNamePath = openimgdlg.FileName;
                    picimg.Image = Image.FromFile(imgFileNamePath);
                    long picpathsize = new System.IO.FileInfo(imgFileNamePath).Length;
                    if (picpathsize > 701000)
                    {
                        picimg.Image = پارسی_نویس.Properties.Resources.DF;
                        MessageBox.Show("حجم تصویر انتخاب شده باید کمتر از 700 کیلوبایت باشد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        Bitmap bitmapimgsave = (Bitmap)Image.FromFile(imgFileNamePath);
                        MemoryStream memoryStream = new MemoryStream();
                        int tmpnewimg = imgFileNamePath.Length;
                        if ((imgFileNamePath[tmpnewimg - 3].ToString() + imgFileNamePath[tmpnewimg - 2].ToString() + imgFileNamePath[tmpnewimg - 1].ToString()).ToLower() == "png")
                            bitmapimgsave.Save(memoryStream, ImageFormat.Png);
                        else
                            bitmapimgsave.Save(memoryStream, ImageFormat.Jpeg);
                        byte[] bitmapBytes = memoryStream.GetBuffer();
                        string bitmapString = Convert.ToBase64String(bitmapBytes, Base64FormattingOptions.InsertLineBreaks);
                        string enterlessbitmap = "";
                        enterlessbitmap = Regex.Replace(bitmapString, "\r\n", "");
                        if (edtIMG == true)
                            File.WriteAllText(project_path + "imgPIC" + enoIMG.ToString() + ".parsinevis", enterlessbitmap);
                        else
                            File.WriteAllText(project_path + "imgPIC" + coIMG.ToString() + ".parsinevis", enterlessbitmap);
                    }
                }
                catch
                {
                    MessageBox.Show("باز کردن فایل با خطلا مواجه شد!", "خطا", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void Chkimglock_CheckedChanged(object sender, EventArgs e)
        {
            if (chkimglock.Checked == true) picimg.SizeMode = PictureBoxSizeMode.Zoom;
            else picimg.SizeMode = PictureBoxSizeMode.StretchImage;
            if (edtIMG == true) saveIMG();
        }

        private void TxtimgName_TextChanged(object sender, EventArgs e)
        {
            IMGErrors();
        }

        private void BtnNewimg_Click(object sender, EventArgs e)
        {
            if (edtIMG == false)
            {
                nameIMG[coIMG] = txtimgName.Text;
                xyIMG[coIMG, 0] = Convert.ToInt32(numimgWidth.Value);
                xyIMG[coIMG, 1] = Convert.ToInt32(numimgHeight.Value);
                string tmpnametxt = "IMG" + coIMG.ToString();
                objIMG[coIMG] = new PictureBox();
                ((PictureBox)objIMG[coIMG]).Image = picimg.Image;
                ((PictureBox)objIMG[coIMG]).SizeMode = picimg.SizeMode;
                objIMG[coIMG].Name = tmpnametxt;
                objIMG[coIMG].Location = new Point(xyIMG[coIMG, 0], xyIMG[coIMG, 1]);
                objIMG[coIMG].Click += seIMG;
                objIMG[coIMG].Size = new Size(Convert.ToInt32(numimgsize0.Value), Convert.ToInt32(numimgsize1.Value));
                imglist.Items.Add(nameIMG[coIMG]);
                //IMGedit(coIMG);
                panelProgram.Controls.Add(objIMG[coIMG]);
                treeView1.Nodes[3].Nodes.Add(nameIMG[coIMG], nameIMG[coIMG]);

                int imgsavetmpSizemode = 0;
                if (picimg.SizeMode == PictureBoxSizeMode.StretchImage) imgsavetmpSizemode = 1;

                File.WriteAllText(project_path + tmpnametxt + ".parsinevis",
                    nameIMG[coIMG] + "\n" +
                    xyIMG[coIMG, 0].ToString() + "\n" +
                    xyIMG[coIMG, 1].ToString() + "\n" +
                    numimgsize0.Value.ToString() + "\n" +
                    numimgsize1.Value.ToString() + "\n" +
                    imgsavetmpSizemode.ToString());

                File.WriteAllText(project_path + tmpnametxt + "e.parsinevis", "");

                IMGvis[coIMG] = true;
                objIMG[coIMG].BringToFront();

                coIMG++;
                enoIMG = coIMG;

                Bitmap bitmapimgsave = (Bitmap)Image.FromFile(".\\DF.png");
                MemoryStream memoryStream = new MemoryStream();
                bitmapimgsave.Save(memoryStream, ImageFormat.Png);
                byte[] bitmapBytes = memoryStream.GetBuffer();
                string bitmapString = Convert.ToBase64String(bitmapBytes, Base64FormattingOptions.InsertLineBreaks);
                string enterlessbitmap = "";
                enterlessbitmap = Regex.Replace(bitmapString, "\r\n", "");
                File.WriteAllText(project_path + "imgPIC" + coIMG.ToString() + ".parsinevis", enterlessbitmap);
                File.WriteAllText(project_path + "imgTYP" + coIMG.ToString() + ".parsinevis", "2");
               
                NewMode();
            }
            else
            {
                for (int i = 0; i < imglist.Items.Count; i++)
                {
                    if (imglist.Items[i].ToString() == nameIMG[enoIMG])
                    {
                        treeView1.Nodes[3].Nodes[i].Text = txtimgName.Text;
                        imglist.Items[i] = txtimgName.Text;
                    }
                }
                nameIMG[enoIMG] = txtimgName.Text;
                objIMG[enoIMG].Text = txtimgName.Text;
                saveIMG();
                NewMode();
            }
            callerrors();
        }

        private void Txtchkname_TextChanged(object sender, EventArgs e)
        {
            CHKErrors();
        }

        private void TxtchkText_TextChanged(object sender, EventArgs e)
        {
            CHKErrors();
        }

        private void BtnNewchk_Click(object sender, EventArgs e)
        {
            if (edtCHK == false)
            {
                nameCHK[coCHK] = txtchkname.Text;
                xyCHK[coCHK, 0] = Convert.ToInt32(numchkWidth.Value);
                xyCHK[coCHK, 1] = Convert.ToInt32(numchkHeight.Value);
                string tmpnametxt = "CHK" + coCHK.ToString();
                objCHK[coCHK] = new CheckBox();
                ((CheckBox)objCHK[coCHK]).AutoCheck = false;
                ((CheckBox)objCHK[coCHK]).Checked = radchktrue.Checked;
                objCHK[coCHK].Name = tmpnametxt;
                objCHK[coCHK].Text = txtchkText.Text;
                objCHK[coCHK].Location = new Point(xyCHK[coCHK, 0], xyCHK[coCHK, 1]);
                ((CheckBox)objCHK[coCHK]).AutoSize = true;
                objCHK[coCHK].Click += seCHK;
                chklist.Items.Add(nameCHK[coCHK]);
                //CHKedit(coCHK);
                panelProgram.Controls.Add(objCHK[coCHK]);
                treeView1.Nodes[4].Nodes.Add(nameCHK[coCHK], nameCHK[coCHK]);

                int chkchecksavetmp = 0;
                if (radchktrue.Checked == true) chkchecksavetmp = 1;

                File.WriteAllText(project_path + tmpnametxt + ".parsinevis",
                    nameCHK[coCHK] + "\n" +
                    txtchkText.Text + "\n" +
                    xyCHK[coCHK, 0].ToString() + "\n" +
                    xyCHK[coCHK, 1].ToString() + "\n" +
                    chkchecksavetmp.ToString());
                File.WriteAllText(project_path + tmpnametxt + "e.parsinevis", "");

                CHKvis[coCHK] = true;
                objCHK[coCHK].BringToFront();
                coCHK++;
                enoCHK = coCHK;
                NewMode();
            }
            else
            {
                for (int i = 0; i < chklist.Items.Count; i++)
                {
                    if (chklist.Items[i].ToString() == nameCHK[enoCHK])
                    {
                        treeView1.Nodes[4].Nodes[i].Text = txtchkname.Text;
                        chklist.Items[i] = txtchkname.Text;
                    }
                }
                nameCHK[enoCHK] = txtchkname.Text;
                objCHK[enoCHK].Text = txtchkText.Text;
                saveCHK();
                NewMode();
            }
            callerrors();
        }

        private void BtnDeleteLBL_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا واقعا می‌خواهید این برچسب را حذف کنید؟\nبا حذف این برچسب تمام کدها و رویدادهای مربوط به آن حذف می‌شوند.", "هشدار!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ScanAndDeleteValue(JustNameToBName(nameLBL[enoLBL]));
                objLBL[enoLBL].Visible = false;
                lbllist.Items.Remove(nameLBL[enoLBL]);
                treeView1.Nodes.Remove(treeView1.Nodes.Find(nameLBL[enoLBL], true)[0]);
                nameLBL[enoLBL] = "-1";
                LBLvis[enoLBL] = false;
                NewMode();
            }
            addLISTval();
        }

        private void BtnDeleteBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا واقعا می‌خواهید این دکمه را حذف کنید؟\nبا حذف این دکمه تمام کدها و رویدادهای مربوط به آن حذف می‌شوند.", "هشدار!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ScanAndDeleteValue(JustNameToBName(nameBTN[enoBTN]));
                objBTN[enoBTN].Visible = false;
                btnlist.Items.Remove(nameBTN[enoBTN]);
                treeView1.Nodes.Remove(treeView1.Nodes.Find(nameBTN[enoBTN], true)[0]);
                nameBTN[enoBTN] = "-1";
                BTNvis[enoBTN] = false;
                NewMode();
            }
            addLISTval();
        }

        private void BtnDeleteTxt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا واقعا می‌خواهید این جعبه متنی را حذف کنید؟\nبا حذف این جعبه متنی تمام کدها و رویدادهای مربوط به آن حذف می‌شوند.", "هشدار!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ScanAndDeleteValue(JustNameToBName(nameTXT[enoTXT]));
                objTXT[enoTXT].Visible = false;
                txtlist.Items.Remove(nameTXT[enoTXT]);
                treeView1.Nodes.Remove(treeView1.Nodes.Find(nameTXT[enoTXT], true)[0]);
                nameTXT[enoTXT] = "-1";
                TXTvis[enoTXT] = false;
                NewMode();
            }
            addLISTval();
        }

        private void BtnDeleteImg_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا واقعا می‌خواهید این تصویر را حذف کنید؟\nبا حذف این تصویر تمام کدها و رویدادهای مربوط به آن حذف می‌شوند.", "هشدار!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ScanAndDeleteValue(JustNameToBName(nameIMG[enoIMG]));
                objIMG[enoIMG].Visible = false;
                imglist.Items.Remove(nameIMG[enoIMG]);
                treeView1.Nodes.Remove(treeView1.Nodes.Find(nameIMG[enoIMG], true)[0]);
                nameIMG[enoIMG] = "-1";
                IMGvis[enoIMG] = false;
                NewMode();
            }
        }

        private void BtnDeleteChk_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا واقعا می‌خواهید این جعبه گزینه را حذف کنید؟\nبا حذف این جعبه گزینه تمام کدها و رویدادهای مربوط به آن حذف می‌شوند.", "هشدار!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ScanAndDeleteValue(JustNameToBName(nameCHK[enoCHK]));
                objCHK[enoCHK].Visible = false;
                chklist.Items.Remove(nameCHK[enoCHK]);
                treeView1.Nodes.Remove(treeView1.Nodes.Find(nameCHK[enoCHK], true)[0]);
                nameCHK[enoCHK] = "-1";
                CHKvis[enoCHK] = false;
                NewMode();
            }
        }

        void radMOTchange(object sender, EventArgs e)
        {
            motalllist.Items.Clear();
            Motalllist_SelectedIndexChanged(null, null);
            int radMOTchangetmpint = 0;
            if (radmodint.Checked == true) radMOTchangetmpint = 0;
            else if (radmodfloat.Checked == true) radMOTchangetmpint = 1;
            else if (radmodestr.Checked == true) radMOTchangetmpint = 2;
            for (int i = 0; i < coMOT; i++)
            {
                if (mots[i] == radMOTchangetmpint) motalllist.Items.Add(motName[i]);
            }
        }

        void MOTerrors()
        {
            lblNewMOTerrors.BackColor = System.Drawing.Color.Red;
            lblNewMOTerrors.ForeColor = System.Drawing.Color.White;
            btnNewMOT.Enabled = false;
            if (txtNewMOT.Text == "") lblNewMOTerrors.Text = "نام نمی‌تواند خالی باشد.";
            else if (txtNewMOT.Text.Length > 15) lblNewMOTerrors.Text = "نام حداکثر می‌تواند 15 حرف داشته باشد.";
            else if (txtNewMOT.Text[0] < 65 || txtNewMOT.Text[0] > 122) lblNewMOTerrors.Text = "نام فقط با حروف لاتین شروع می‌شود.";
            else if (txtNewMOT.Text[0] > 90 && txtNewMOT.Text[0] < 97) lblNewMOTerrors.Text = "نام فقط با حروف لاتین شروع می‌شود.";
            else if (nameController(txtNewMOT.Text) == false) lblNewMOTerrors.Text = "در نام می‌توان فقط از حروف لاتین و اعداد استفاده کرد.";
            else if (checkNewName(txtNewMOT.Text, motName, false, coMOT) == false) lblNewMOTerrors.Text = "این نام قبلا استفاده شده است.";

            else
            {
                lblNewMOTerrors.BackColor = System.Drawing.Color.LightGreen;
                lblNewMOTerrors.ForeColor = System.Drawing.Color.Black;
                lblNewMOTerrors.Text = "با کلیک بر روی دکمه زیر، متفیر ساخته می‌شود.";
                btnNewMOT.Enabled = true;
            }
        }

        void ChangeMOTerrors()
        {
            lblchangeMOTerrors.BackColor = System.Drawing.Color.Red;
            lblchangeMOTerrors.ForeColor = System.Drawing.Color.White;
            btnChangeMOT.Enabled = false;
            if (txtMOTchangeNewName.Text == "") lblchangeMOTerrors.Text = "نام نمی‌تواند خالی باشد.";
            else if (txtMOTchangeNewName.Text.Length > 15) lblchangeMOTerrors.Text = "نام حداکثر می‌تواند 15 حرف داشته باشد.";
            else if (txtMOTchangeNewName.Text[0] < 65 || txtMOTchangeNewName.Text[0] > 122) lblchangeMOTerrors.Text = "نام فقط با حروف لاتین شروع می‌شود.";
            else if (txtMOTchangeNewName.Text[0] > 90 && txtMOTchangeNewName.Text[0] < 97) lblchangeMOTerrors.Text = "نام فقط با حروف لاتین شروع می‌شود.";
            else if (nameController(txtMOTchangeNewName.Text) == false) lblchangeMOTerrors.Text = "در نام می‌توان فقط از حروف لاتین و اعداد استفاده کرد.";
            else if (checkNewName(txtMOTchangeNewName.Text, motName, true, edtMOT) == false) lblchangeMOTerrors.Text = "این نام قبلا استفاده شده است.";

            else
            {
                lblchangeMOTerrors.BackColor = System.Drawing.Color.LightGreen;
                lblchangeMOTerrors.ForeColor = System.Drawing.Color.Black;
                lblchangeMOTerrors.Text = "با کلیک بر روی دکمه زیر، متفیر ساخته می‌شود.";
                btnChangeMOT.Enabled = true;
            }
        }

        private void TxtNewMOT_TextChanged(object sender, EventArgs e)
        {
            MOTerrors();
        }

        void motNewMode()
        {
            txtNewMOT.Text = "";
            radnewMOTint.Checked = true;
            radnewMOTfloat.Checked = false;
            radnewMOTstring.Checked = false;
            txtMOTchangeNewName.Text = "";
            txtMOTchangeNOWname.Text = "";
            txtMOTchangeNOWstate.Text = "";
            radCHmotInt.Checked = true;
            txtMOTchangeNewName.Enabled = false;
            radCHmotInt.Enabled = false;
            radCHmotFloat.Enabled = false;
            radCHmotString.Enabled = false;
            btnDeleteMOT.Enabled = false;
            callerrors();
            saveAll();
            addLISTval();
        }

        private void BtnNewMOT_Click(object sender, EventArgs e)
        {
            if (radnewMOTint.Checked == true) mots[coMOT] = 0;
            else if (radnewMOTfloat.Checked == true) mots[coMOT] = 1;
            else if (radnewMOTstring.Checked == true) mots[coMOT] = 2;
            motName[coMOT] = txtNewMOT.Text;
            listTmpMOT.Items.Add(txtNewMOT.Text);
            edtMOT = coMOT;
            coMOT++;
            motNewMode();
            radMOTchange(null,null);
            addLISTval();
            UpdateListTmpMot();
        }

        private void Motalllist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (motalllist.SelectedIndex != -1)
            {
                txtMOTchangeNewName.Enabled = true;
                radCHmotInt.Enabled = true;
                radCHmotFloat.Enabled = true;
                radCHmotString.Enabled = true;
                btnDeleteMOT.Enabled = true;
                for (int i = 0; i < coMOT; i++)
                {
                    if (motalllist.Items[motalllist.SelectedIndex].ToString() == motName[i])
                    {
                        if (mots[i] == 0 || mots[i] == 1 || mots[i] == 2) edtMOT = i;
                    }                
                }
                txtMOTchangeNewName.Text = motName[edtMOT];
                txtMOTchangeNOWname.Text = motName[edtMOT];
                if (mots[edtMOT] == 0)      { txtMOTchangeNOWstate.Text = "عدد صحیح"; radCHmotInt.Checked = true; }
                else if (mots[edtMOT] == 1) { txtMOTchangeNOWstate.Text = "عدد اعشاری"; radCHmotFloat.Checked = true; }
                else if (mots[edtMOT] == 2) { txtMOTchangeNOWstate.Text = "رشته"; radCHmotString.Checked = true; }
            }
            else
            {
                txtMOTchangeNewName.Text = "";
                txtMOTchangeNOWname.Text = "";
                txtMOTchangeNOWstate.Text = "";
                radCHmotInt.Checked = true;
                txtMOTchangeNewName.Enabled = false;
                radCHmotInt.Enabled = false;
                radCHmotFloat.Enabled = false;
                radCHmotString.Enabled = false;
                btnDeleteMOT.Enabled = false;
            }
        }

        private void TxtMOTchangeNewName_TextChanged(object sender, EventArgs e)
        {
            ChangeMOTerrors();
        }

        void addLISTval()
        {
            listValues.Items.Clear();
            for (int i = 0; i < listTmpMOT.Items.Count; i++) listValues.Items.Add(listTmpMOT.Items[i]);
            for (int i = 0; i < lbllist.Items.Count; i++) listValues.Items.Add("متن برچسب " + lbllist.Items[i].ToString());
            for (int i = 0; i < btnlist.Items.Count; i++) listValues.Items.Add("متن دکمه " + btnlist.Items[i].ToString());
            for (int i = 0; i < txtlist.Items.Count; i++) listValues.Items.Add("متن جعبه متنی " + txtlist.Items[i].ToString());
            comboIFo1.Items.Clear();
            comboIFo2.Items.Clear();
            comboWHILEo1.Items.Clear();
            comboWHILEo2.Items.Clear();
            combotabMValueFirst.Items.Clear();
            combotabMValueMain.Items.Clear();
            combotabMValueSeccond.Items.Clear();
            for (int i = 0; i < listValues.Items.Count; i++)
            {
                comboIFo1.Items.Add(listValues.Items[i]);
                comboIFo2.Items.Add(listValues.Items[i]);
                comboWHILEo1.Items.Add(listValues.Items[i]);
                comboWHILEo2.Items.Add(listValues.Items[i]);
                combotabMValueFirst.Items.Add(listValues.Items[i]);
                combotabMValueMain.Items.Add(listValues.Items[i]); 
                combotabMValueSeccond.Items.Add(listValues.Items[i]);
            }
            callerrors();
            saveAll();
        }

        void translateToMainList()
        {
            string LTPtxt = "";
            string LTPt1 = "";
            string LTPt2 = "";
            string LTPt3 = "";
            string LTPt4 = "";
            int LTPmnum = 0;
            listBackProgram.Items.Clear();
            listMainProgram.Items.Clear();
            for (int i = 0; i < listPrTMP.Items.Count; i++)
            {
                LTPtxt = listPrTMP.Items[i].ToString();
                LTPmnum = Convert.ToInt32((LTPtxt[0].ToString() + LTPtxt[1].ToString()).ToString());
                listBackProgram.Items.Add(LTPtxt);
                switch(LTPmnum)
                {
                    case 10:
                        int i2 = 2;
                        LTPt1 = "";
                        LTPt2 = "";
                        LTPt3 = "";
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt1 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt2 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        switch (LTPtxt[i2])
                        {
                            case '1':
                                LTPt3 = "برابر";
                                break;

                            case '2':
                                LTPt3 = "نابرابر";
                                break;

                            case '3':
                                LTPt3 = "بزرگتر";
                                break;

                            case '4':
                                LTPt3 = "کوچکتر";
                                break;
                        }
                        listMainProgram.Items.Add("اگر " + BackTXTtoPersian(LTPt1) + "، " + LTPt3 + " بود با " + BackTXTtoPersian(LTPt2) + " دستورات زیر را انجام بده");
                        break;

                    case 11:
                        listMainProgram.Items.Add("پایان شرط");
                        break;

                    case 12:
                        int i3 = 2;
                        LTPt1 = "";
                        LTPt2 = "";
                        LTPt3 = "";
                        while (LTPtxt[i3] != '$')
                        {
                            LTPt1 += LTPtxt[i3].ToString();
                            i3++;
                        }
                        i3++;
                        while (LTPtxt[i3] != '$')
                        {
                            LTPt2 += LTPtxt[i3].ToString();
                            i3++;
                        }
                        i3++;
                        switch (LTPtxt[i3])
                        {
                            case '1':
                                LTPt3 = "برابر";
                                break;

                            case '2':
                                LTPt3 = "نابرابر";
                                break;

                            case '3':
                                LTPt3 = "بزرگتر";
                                break;

                            case '4':
                                LTPt3 = "کوچکتر";
                                break;
                        }
                        listMainProgram.Items.Add("تا وقتی که " + BackTXTtoPersian(LTPt1) + "، " + LTPt3 + " بود با " + BackTXTtoPersian(LTPt2) + " دستورات زیر را تکرار کن");
                        break;

                    case 13:
                        listMainProgram.Items.Add("پایان حلقه تکرار شونده");
                        break;

                    case 14: // مقدار دهی مستقیم
                        int i4 = 2;
                        LTPt1 = "";
                        LTPt2 = "";
                        LTPt3 = "";
                        while (LTPtxt[i4] != '$')
                        {
                            LTPt1 += LTPtxt[i4].ToString();
                            i4++;
                        }
                        i4++;
                        while (LTPtxt[i4] != '$')
                        {
                            LTPt2 += LTPtxt[i4].ToString();
                            i4++;
                        }
                        LTPt3 = File.ReadAllText(project_path + "fil" + LTPt2 + ".parsinevis");
                        listMainProgram.Items.Add("مقدار " + BackTXTtoPersian(LTPt1) + " برابر شود با: " + LTPt3);
                        break;

                    case 15: // مقدار دهی با یک متغیر
                        int i5 = 2;
                        LTPt1 = "";
                        LTPt2 = "";
                        LTPt3 = "";
                        while (LTPtxt[i5] != '$')
                        {
                            LTPt1 += LTPtxt[i5].ToString();
                            i5++;
                        }
                        i5++;
                        while (LTPtxt[i5] != '$')
                        {
                            LTPt2 += LTPtxt[i5].ToString();
                            i5++;
                        }
                        listMainProgram.Items.Add("مقدار " + BackTXTtoPersian(LTPt1) + " برابر شود با: " + BackTXTtoPersian(LTPt2));
                        break;

                    case 16: // مقدار دهی با دو متغیر
                        int i6 = 2;
                        LTPt1 = "";
                        LTPt2 = "";
                        LTPt3 = "";
                        LTPt4 = "";
                        while (LTPtxt[i6] != '$')
                        {
                            LTPt1 += LTPtxt[i6].ToString();
                            i6++;
                        }
                        i6++;
                        while (LTPtxt[i6] != '$')
                        {
                            LTPt2 += LTPtxt[i6].ToString();
                            i6++;
                        }
                        i6++;
                        while (LTPtxt[i6] != '$')
                        {
                            LTPt3 += LTPtxt[i6].ToString();
                            i6++;
                        }
                        i6++;
                        switch(LTPtxt[i6])
                        {
                            case '1':
                                LTPt4 = "به علاوه";
                                break;

                            case '2':
                                LTPt4 = "منهای";
                                break;

                            case '3':
                                LTPt4 = "ضربدر";
                                break;

                            case '4':
                                LTPt4 = "تقسیم بر";
                                break;
                        }
                        listMainProgram.Items.Add("مقدار " + BackTXTtoPersian(LTPt1) + " برابر شود با: " + BackTXTtoPersian(LTPt2) + " " + LTPt4 + " " + BackTXTtoPersian(LTPt3));
                        break;

                    case 17:
                        int i7 = 2;
                        LTPt1 = "";
                        LTPt2 = "";
                        while (LTPtxt[i7] != '$')
                        {
                            LTPt1 += LTPtxt[i7].ToString();
                            i7++;
                        }
                        i7++;
                        if (LTPtxt[i7] == '1') LTPt2 = "قابل مشاهده";
                        else LTPt2 = "پنهان";
                        listMainProgram.Items.Add("وضعیت نمایش شیء " + NameUserOBJ(LTPt1) + " به حالت " + LTPt2 + " تغییر داده شود");
                        break;
                }
            }
            CheckHolePrErrors();
        }

        string findInLists(string sLname, int iState)
        {
            string sLout = "-1";
            switch(iState)
            {
                case 1: //Label
                    for (int i = 0; i < coLBL; i++)
                    {
                        if (nameLBL[i] == sLname && ObjExists("LBL", i) == true) sLout = "LBL" + i.ToString();
                    }
                    break;

                case 2: //Button
                    for (int i = 0; i < coBTN; i++)
                    {
                        if (nameBTN[i] == sLname && ObjExists("BTN", i) == true) sLout = "BTN" + i.ToString();
                    }
                    break;

                case 3: //TextBox
                    for (int i = 0; i < coTXT; i++)
                    {
                        if (nameTXT[i] == sLname && ObjExists("TXT", i) == true) sLout = "TXT" + i.ToString();
                    }
                    break;

                case 4: //Value
                    for (int i = 0; i < coMOT; i++)
                    {
                        if (motName[i] == sLname && ObjExists("MOT", i) == true) sLout = "MOT" + i.ToString();
                    }
                    break;
            }
            return sLout;
        }

        string findNameOFobj(string stmp)
        {
            string sOUTfName = "-1";
            string sfNameTMP = "";
            if (stmp.StartsWith("متن دکمه "))
            {
                for (int i = 9; i < stmp.Length; i++) { sfNameTMP += stmp[i].ToString(); }
                sOUTfName = findInLists(sfNameTMP, 2);
            }
            else if (stmp.StartsWith("متن برچسب "))
            {
                for (int i = 10; i < stmp.Length; i++) sfNameTMP += stmp[i].ToString();
                sOUTfName = findInLists(sfNameTMP, 1);
            }
            else if (stmp.StartsWith("متن جعبه متنی "))
            {
                for (int i = 14; i < stmp.Length; i++) sfNameTMP += stmp[i].ToString();
                sOUTfName = findInLists(sfNameTMP, 3);
            }
            else
            {
                for (int i = 0; i < stmp.Length; i++) sfNameTMP += stmp[i].ToString();
                sOUTfName = findInLists(sfNameTMP, 4);
            }
            return sOUTfName;
        }

        private void BtnIFadd_Click(object sender, EventArgs e)
        {
            int i2 = listMainProgram.SelectedIndex;
            if (i2 == -1) i2 = listMainProgram.Items.Count;
            else i2++;
            int i = 0;
            switch (comboIFstate.Text)
            {
                case "برابر":
                    i = 1;
                    break;

                case "نابرابر":
                    i = 2;
                    break;

                case "بزرگتر":
                    i = 3;
                    break;

                case "کوچکتر":
                    i = 4;
                    break;
            }
            addBLCode("10" + findNameOFobj(comboIFo1.Text) + "$" + findNameOFobj(comboIFo2.Text) + "$" + i.ToString(), listMainProgram.SelectedIndex);
            addBLCode("11", i2);
            CheckHolePrErrors();
        }

        void IFstateChangeErrors(object sender, EventArgs e)
        {
            bool IfsChErBoolTMP = false;
            if (comboIFstate.Text == "برابر" || comboIFstate.Text == "نابرابر" || comboIFstate.Text == "کوچکتر" || comboIFstate.Text == "بزرگتر") IfsChErBoolTMP = true;
            if (IfsChErBoolTMP == true && findNameOFobj(comboIFo1.Text) != "-1" && findNameOFobj(comboIFo2.Text) != "-1")
            {
                lbltabIFerrors.BackColor = System.Drawing.Color.LightGreen;
                lbltabIFerrors.ForeColor = System.Drawing.Color.Black;
                lbltabIFerrors.Text = "با کلیک بر روی دکمه روبه رو، این قطعه کد به برنامه اضافه می‌شود.";
                btnIFadd.Enabled = true;
            }
            else
            {
                lbltabIFerrors.BackColor = System.Drawing.Color.Red;
                lbltabIFerrors.ForeColor = System.Drawing.Color.White;
                lbltabIFerrors.Text = "اطلاعات وارد شده صحیح نیست";
                btnIFadd.Enabled = false;
            }
        }

        void WHILEstateChangeErrors(object sender, EventArgs e)
        {
            bool WhilesChErBoolTMP = false;
            if (comboWHILEstate.Text == "برابر" || comboWHILEstate.Text == "نابرابر" || comboWHILEstate.Text == "کوچکتر" || comboWHILEstate.Text == "بزرگتر") WhilesChErBoolTMP = true;
            if (WhilesChErBoolTMP == true && findNameOFobj(comboWHILEo1.Text) != "-1" && findNameOFobj(comboWHILEo2.Text) != "-1")
            {
                lbltabWHILEerrors.BackColor = System.Drawing.Color.LightGreen;
                lbltabWHILEerrors.ForeColor = System.Drawing.Color.Black;
                lbltabWHILEerrors.Text = "با کلیک بر روی دکمه روبه رو، این قطعه کد به برنامه اضافه می‌شود.";
                btnWHILEadd.Enabled = true;
            }
            else
            {
                lbltabWHILEerrors.BackColor = System.Drawing.Color.Red;
                lbltabWHILEerrors.ForeColor = System.Drawing.Color.White;
                lbltabWHILEerrors.Text = "اطلاعات وارد شده صحیح نیست";
                btnWHILEadd.Enabled = false;
            }
        }

        private void BtnPrDelete_Click(object sender, EventArgs e)
        {
            DeleteCOdeWithIndex(listMainProgram.SelectedIndex);
            btnPrDelete.Enabled = false;
            btnPrUP.Enabled = false;
            btnPrDOWN.Enabled = false;
            SaveEventCode();
            LoadEventCode();
            CheckHolePrErrors();
        }

        bool existINlistBox(ListBox vLlist, string vLstr)
        {
            bool vLbool = false;
            for (int i = 0; i < vLlist.Items.Count; i++)
            {
                if (vLlist.Items[i].ToString() == vLstr) vLbool = true;
            }
            return vLbool;
        }

        void addBLCode(string addSTRcode, int addINDEXcode)
        {
            if (addINDEXcode < 0) addINDEXcode = listBackProgram.Items.Count - 1;
            listPrTMP.Items.Clear();
            if (listBackProgram.Items.Count > 0)
            {
                for (int i = 0; i < listBackProgram.Items.Count; i++)
                {
                    listPrTMP.Items.Add(listBackProgram.Items[i].ToString());
                    if (addINDEXcode == i) listPrTMP.Items.Add(addSTRcode);
                }
            }
            else
            {
                listPrTMP.Items.Add(addSTRcode);
            }
            listBackProgram.Items.Clear();
            translateToMainList();
            SaveEventCode();
        }

        string BackTXTtoPersian(string BTTPstr)
        {
            string BTToutSTR = BTTPstr;
            BTToutSTR = BTTPstr[0].ToString() + BTTPstr[1].ToString() + BTTPstr[2].ToString();
            string BTTTMP = "";
            for (int i = 3; i < BTTPstr.Length; i++) BTTTMP = BTTTMP + BTTPstr[i].ToString();
            int BTTPindex = Convert.ToInt32(BTTTMP);
            switch(BTToutSTR)
            {
                case "MOT":
                    BTToutSTR = motName[BTTPindex];
                    break;

                case "LBL":
                    BTToutSTR = "متن برچسب " + nameLBL[BTTPindex];
                    break;

                case "BTN":
                    BTToutSTR = "متن دکمه " + nameBTN[BTTPindex];
                    break;

                case "TXT":
                    BTToutSTR = "متن جعبه متنی " + nameTXT[BTTPindex];
                    break;
            }
            return BTToutSTR;
        }

        private void BtnWHILEadd_Click(object sender, EventArgs e)
        {
            int i2 = listMainProgram.SelectedIndex;
            if (i2 == -1) i2 = listMainProgram.Items.Count;
            else i2++;
            int i = 0;
            switch (comboIFstate.Text)
            {
                case "برابر":
                    i = 1;
                    break;

                case "نابرابر":
                    i = 2;
                    break;

                case "بزرگتر":
                    i = 3;
                    break;

                case "کوچکتر":
                    i = 4;
                    break;
            }
            addBLCode("12" + findNameOFobj(comboWHILEo1.Text) + "$" + findNameOFobj(comboWHILEo2.Text) + "$" + i.ToString(), listMainProgram.SelectedIndex);
            addBLCode("13", i2);
            CheckHolePrErrors();
        }

        void tabToValueRadState(object sender, EventArgs e)
        {
            bool tmptabVL1 = false;
            bool tmptabVL2 = true;
            if (tabMValueradDI.Checked == true)
            {
                tmptabVL1 = true;
                tmptabVL2 = false;
            }
            else
            {
                tmptabVL1 = false;
                tmptabVL2 = true;
            }
            txtTabValueDir.Visible = tmptabVL1;
            combotabMValueFirst.Visible = tmptabVL2;
            combotabMValueSeccond.Visible = tmptabVL2;
            combotabMValueType.Visible = tmptabVL2;
            chktabMValueS2.Visible = tmptabVL2;
            toValueErrors(null, null);
        }

        void toValueErrors(object sender, EventArgs e)
        {
            bool ToValuesChErBoolTMP = false;
            bool tmpTabToValueErrorsLast = false;
            if (tabMValueradDI.Checked == true)
            {
                if (findNameOFobj(combotabMValueMain.Text) != "-1") tmpTabToValueErrorsLast = true;
                else tmpTabToValueErrorsLast = false;
            }
            else
            {
                if (chktabMValueS2.Checked == false)
                {
                    if (findNameOFobj(combotabMValueFirst.Text) != "-1" && findNameOFobj(combotabMValueMain.Text) != "-1") tmpTabToValueErrorsLast = true;
                    else tmpTabToValueErrorsLast = false;
                }
                else
                {
                    if (combotabMValueType.Text == "به علاوه" || combotabMValueType.Text == "منهای" || combotabMValueType.Text == "ضربدر" || combotabMValueType.Text == "تقسیم بر") ToValuesChErBoolTMP = true;
                    if (ToValuesChErBoolTMP == true && findNameOFobj(combotabMValueFirst.Text) != "-1" && findNameOFobj(combotabMValueMain.Text) != "-1" && findNameOFobj(combotabMValueSeccond.Text) != "-1") tmpTabToValueErrorsLast = true;
                    else tmpTabToValueErrorsLast = false;
                }
            }
            if (tmpTabToValueErrorsLast == true)
            {
                lbltabToValueerrors.BackColor = System.Drawing.Color.LightGreen;
                lbltabToValueerrors.ForeColor = System.Drawing.Color.Black;
                lbltabToValueerrors.Text = "با کلیک بر روی دکمه روبه رو، این قطعه کد به برنامه اضافه می‌شود.";
                btnToValueadd.Enabled = true;
            }
            else
            {
                lbltabToValueerrors.BackColor = System.Drawing.Color.Red;
                lbltabToValueerrors.ForeColor = System.Drawing.Color.White;
                lbltabToValueerrors.Text = "اطلاعات وارد شده صحیح نیست";
                btnToValueadd.Enabled = false;
            }
        }

        private void ChktabMValueS2_CheckedChanged(object sender, EventArgs e)
        {
            if (chktabMValueS2.Checked == true)
            {
                combotabMValueSeccond.Enabled = true;
                combotabMValueType.Enabled = true;
            }
            else
            {
                combotabMValueSeccond.Enabled = false;
                combotabMValueType.Enabled = false;
            }
            toValueErrors(null, null);
        }

        private void BtnToValueadd_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (tabMValueradDI.Checked == true)
            {
                File.WriteAllText(project_path + "fil" + coFil.ToString() + ".parsinevis", txtTabValueDir.Text);
                addBLCode("14" + findNameOFobj(combotabMValueMain.Text) + "$" + coFil.ToString() + "$", listMainProgram.SelectedIndex);
                coFil++;
            }
            else
            {
                if (chktabMValueS2.Checked == true)
                {
                    switch (combotabMValueType.Text)
                    {
                        case "به علاوه":
                            i = 1;
                            break;

                        case "منهای":
                            i = 2;
                            break;

                        case "ضربدر":
                            i = 3;
                            break;

                        case "تقسیم بر":
                            i = 4;
                            break;
                    }
                    addBLCode("16" + findNameOFobj(combotabMValueMain.Text) + "$" + findNameOFobj(combotabMValueFirst.Text) + "$" + findNameOFobj(combotabMValueSeccond.Text) + "$" + i.ToString(), listMainProgram.SelectedIndex);
                }
                else
                {
                    addBLCode("15" + findNameOFobj(combotabMValueMain.Text) + "$" + findNameOFobj(combotabMValueFirst.Text) + "$", listMainProgram.SelectedIndex);
                }
            }
            CheckHolePrErrors();
        }

        string JustNameToBName(string JTTBNin)
        {
            string JTTBNout = "-1";
            for (int i = 0; i < coLBL; i++) if (nameLBL[i] == JTTBNin && ObjExists("LBL", i) == true) JTTBNout = "LBL" + i.ToString();
            for (int i = 0; i < coBTN; i++) if (nameBTN[i] == JTTBNin && ObjExists("BTN", i) == true) JTTBNout = "BTN" + i.ToString();
            for (int i = 0; i < coTXT; i++) if (nameTXT[i] == JTTBNin && ObjExists("TXT", i) == true) JTTBNout = "TXT" + i.ToString();
            for (int i = 0; i < coIMG; i++) if (nameIMG[i] == JTTBNin && ObjExists("IMG", i) == true) JTTBNout = "IMG" + i.ToString();
            for (int i = 0; i < coCHK; i++) if (nameCHK[i] == JTTBNin && ObjExists("CHK", i) == true) JTTBNout = "CHK" + i.ToString();
            for (int i = 0; i < coMOT; i++) if (motName[i] == JTTBNin && ObjExists("MOT", i) == true) JTTBNout = "MOT" + i.ToString();
            return JTTBNout;
        }

        string NameUserOBJ(string NUOBJin)
        {
            string NUOBJout = "";
            string NUOBJtmp = "";
            int NUOBJnum = 0;
            for (int i = 3; i < NUOBJin.Length; i++) NUOBJtmp += NUOBJin[i].ToString();
            NUOBJnum = Convert.ToInt32(NUOBJtmp);
            NUOBJtmp = NUOBJin[0].ToString() + NUOBJin[1].ToString() + NUOBJin[2].ToString();
            switch (NUOBJtmp)
            {
                case "LBL":
                    NUOBJout = nameLBL[NUOBJnum];
                    break;

                case "BTN":
                    NUOBJout = nameBTN[NUOBJnum];
                    break;

                case "TXT":
                    NUOBJout = nameTXT[NUOBJnum];
                    break;

                case "IMG":
                    NUOBJout = nameIMG[NUOBJnum];
                    break;

                case "CHK":
                    NUOBJout = nameCHK[NUOBJnum];
                    break;
            }
            return NUOBJout;
        }

        void objShowTabErrors(object sender, EventArgs e)
        {
            bool ObjMansChErBoolTMP = false;
            if (comboTabViOBJtype.Text == "قابل مشاهده" || comboTabViOBJtype.Text == "پنهان") ObjMansChErBoolTMP = true;
            if (ObjMansChErBoolTMP == true && JustNameToBName(comboTabViOBJmain.Text) != "-1")
            {
                lbltabOBJerrors.BackColor = System.Drawing.Color.LightGreen;
                lbltabOBJerrors.ForeColor = System.Drawing.Color.Black;
                lbltabOBJerrors.Text = "با کلیک بر روی دکمه روبه رو، این قطعه کد به برنامه اضافه می‌شود.";
                btnOBJadd.Enabled = true;
            }
            else
            {
                lbltabOBJerrors.BackColor = System.Drawing.Color.Red;
                lbltabOBJerrors.ForeColor = System.Drawing.Color.White;
                lbltabOBJerrors.Text = "اطلاعات وارد شده صحیح نیست";
                btnOBJadd.Enabled = false;
            }
        }

        private void BtnOBJadd_Click(object sender, EventArgs e)
        {
            string BOBJ_Ctmp = "1";
            if (comboTabViOBJtype.Text == "پنهان") BOBJ_Ctmp = "2";
            addBLCode("17" + JustNameToBName(comboTabViOBJmain.Text) + "$" + BOBJ_Ctmp, listMainProgram.SelectedIndex);
            CheckHolePrErrors();
        }

        private void ListMainProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listMainProgram.SelectedIndex >= 0)
            {
                string LMP_SICtmp = listBackProgram.Items[listMainProgram.SelectedIndex].ToString();
                int LMP_SICnum = Convert.ToInt32(LMP_SICtmp[0].ToString() + LMP_SICtmp[1].ToString());
                if (LMP_SICnum != 11 && LMP_SICnum != 13) btnPrDelete.Enabled = true;
                else btnPrDelete.Enabled = false;
                if (listMainProgram.SelectedIndex > 0)
                {
                    LMP_SICtmp = listBackProgram.Items[(listMainProgram.SelectedIndex - 1)].ToString();
                    LMP_SICnum = Convert.ToInt32(LMP_SICtmp[0].ToString() + LMP_SICtmp[1].ToString());
                    if (LMP_SICnum != 10 && LMP_SICnum != 11 && LMP_SICnum != 12 && LMP_SICnum != 13) btnPrUP.Enabled = true;
                    else btnPrUP.Enabled = false;
                    LMP_SICtmp = listBackProgram.Items[listMainProgram.SelectedIndex].ToString();
                    LMP_SICnum = Convert.ToInt32(LMP_SICtmp[0].ToString() + LMP_SICtmp[1].ToString());
                    if (LMP_SICnum != 10 && LMP_SICnum != 11 && LMP_SICnum != 12 && LMP_SICnum != 13) btnPrUP.Enabled = true;
                }
                else btnPrUP.Enabled = false;
                if (listMainProgram.SelectedIndex < (listMainProgram.Items.Count - 1))
                {
                    LMP_SICtmp = listBackProgram.Items[(listMainProgram.SelectedIndex + 1)].ToString();
                    LMP_SICnum = Convert.ToInt32(LMP_SICtmp[0].ToString() + LMP_SICtmp[1].ToString());
                    if (LMP_SICnum != 10 && LMP_SICnum != 11 && LMP_SICnum != 12 && LMP_SICnum != 13) btnPrDOWN.Enabled = true;
                    else btnPrDOWN.Enabled = false;
                    LMP_SICtmp = listBackProgram.Items[listMainProgram.SelectedIndex].ToString();
                    LMP_SICnum = Convert.ToInt32(LMP_SICtmp[0].ToString() + LMP_SICtmp[1].ToString());
                    if (LMP_SICnum != 10 && LMP_SICnum != 11 && LMP_SICnum != 12 && LMP_SICnum != 13) btnPrDOWN.Enabled = true;
                }
                else btnPrDOWN.Enabled = false;
            }
            else
            {
                btnPrDelete.Enabled = false;
                btnPrUP.Enabled = false;
                btnPrDOWN.Enabled = false;
            }
            CheckHolePrErrors();
        }

        void ChLocationCode(int CLCmode)
        {
            listPrTMP.Items.Clear();
            string CLCfirstCode = listBackProgram.Items[listMainProgram.SelectedIndex].ToString();
            int CLCselectedIndex = listMainProgram.SelectedIndex;
            string CLCseccondCode = "";
            int CLCtmp = 0;
            int CLClaSe = 0;
            if (CLCmode == 1)
            {
                CLCseccondCode = listBackProgram.Items[(CLCselectedIndex - 1)].ToString();
                CLCtmp = (CLCselectedIndex - 1);
                CLClaSe = CLCtmp;
            }
            else
            {
                CLCseccondCode = CLCfirstCode;
                CLCfirstCode = listBackProgram.Items[(CLCselectedIndex + 1)].ToString();
                CLCtmp = CLCselectedIndex;
                CLClaSe = (CLCtmp + 1);
            }
            for (int i = 0; i < CLCtmp; i++) listPrTMP.Items.Add(listBackProgram.Items[i].ToString());
            listPrTMP.Items.Add(CLCfirstCode);
            listPrTMP.Items.Add(CLCseccondCode);
            for (int i = (CLCtmp + 2); i < listMainProgram.Items.Count; i++) listPrTMP.Items.Add(listBackProgram.Items[i].ToString());
            translateToMainList();
            listMainProgram.SelectedIndex = CLClaSe;
        }

        private void BtnPrUP_Click(object sender, EventArgs e)
        {
            ChLocationCode(1);
        }

        private void BtnPrDOWN_Click(object sender, EventArgs e)
        {
            ChLocationCode(2);
        }

        void DeleteCOdeWithIndex(int DCWIindex)
        {
            int BPDd1 = DCWIindex;
            int BPDd2 = 0;
            string BPDseCodeStr = listBackProgram.Items[DCWIindex].ToString();
            int BPDseCodeNum = Convert.ToInt32(BPDseCodeStr[0].ToString() + BPDseCodeStr[1].ToString());
            if (BPDseCodeNum == 10 || BPDseCodeNum == 12)
            {
                int i = 1;
                int i2 = (DCWIindex + 1);
                while (i != 0)
                {
                    BPDseCodeStr = listBackProgram.Items[i2].ToString();
                    BPDseCodeNum = Convert.ToInt32(BPDseCodeStr[0].ToString() + BPDseCodeStr[1].ToString());
                    if (BPDseCodeNum == 10 || BPDseCodeNum == 12) i++;
                    if (BPDseCodeNum == 11 || BPDseCodeNum == 13) i--;
                    BPDd2 = i2;
                    i2++;
                }
            }
            else BPDd2 = BPDd1;
            listPrTMP.Items.Clear();
            for (int i = 0; i < listMainProgram.Items.Count; i++)
            {
                if (i != BPDd1 && i != BPDd2) listPrTMP.Items.Add(listBackProgram.Items[i].ToString());
            }
            translateToMainList();
        }

        void UpdateEventsList()
        {
            listPrEvents.Items.Clear();
            for (int i = 0; i < coBTN; i++)
                if (ObjExists("BTN", i))
                    listPrEvents.Items.Add("رویداد کلیک دکمه " + NameUserOBJ("BTN" + (i.ToString()))); 
            for (int i = 0; i < coTXT; i++)
                if (ObjExists("TXT", i))
                    listPrEvents.Items.Add("رویداد تغییر متن جعبه متنی " + NameUserOBJ("TXT" + (i.ToString())));
            for (int i = 0; i < coCHK; i++)
                if (ObjExists("CHK", i))
                    listPrEvents.Items.Add("رویداد تغییر گزینه جعبه گزینه " + NameUserOBJ("CHK" + (i.ToString())));
            comboSelectEvent.Items.Clear();
            for (int i = 0; i < listPrEvents.Items.Count; i++) comboSelectEvent.Items.Add(listPrEvents.Items[i]);
            
        }

        string EventTextToValues(string ETTVin)
        {
            string ETTout = "-1";
            if (existINlistBox(listPrEvents, ETTVin) == true)
            {
                string ETTVtmpStr = "";
                if (ETTVin.StartsWith("رویداد کلیک دکمه "))
                    for (int i = "رویداد کلیک دکمه ".Length; i < ETTVin.Length; i++) ETTVtmpStr += ETTVin[i].ToString();
                else if (ETTVin.StartsWith("رویداد تغییر متن جعبه متنی "))
                    for (int i = "رویداد تغییر متن جعبه متنی ".Length; i < ETTVin.Length; i++) ETTVtmpStr += ETTVin[i].ToString();
                else if (ETTVin.StartsWith("رویداد تغییر گزینه جعبه گزینه "))
                    for (int i = "رویداد تغییر گزینه جعبه گزینه ".Length; i < ETTVin.Length; i++) ETTVtmpStr += ETTVin[i].ToString();
                ETTout = JustNameToBName(ETTVtmpStr);
            }
            return ETTout;
        }

        void LoadEventCode()
        {
            selectedEventText = comboSelectEvent.Text;
            string LEC_Name = EventTextToValues(comboSelectEvent.Text);
            listMainProgram.Items.Clear();
            listBackProgram.Items.Clear();
            listPrTMP.Items.Clear();
            if (LEC_Name == "-1")
            {
                listMainProgram.Enabled = false;
                tabControl1.Enabled = false;
            }
            else
            {
                listMainProgram.Enabled = true;
                tabControl1.Enabled = true;
                string[] g = File.ReadAllLines(project_path + LEC_Name + "e.parsinevis");
                for (int i = 0; i < g.Length; i++) listPrTMP.Items.Add(g[i]);
                translateToMainList();
            }
        }

        void SaveEventCode()
        {
            saveAll();
            string[] SaECbp = new string[listBackProgram.Items.Count];
            for (int i = 0; i < listBackProgram.Items.Count; i++) SaECbp[i] = listBackProgram.Items[i].ToString();
            if (EventTextToValues(selectedEventText) != "-1")
                File.WriteAllLines(project_path + EventTextToValues(selectedEventText) + "e.parsinevis", SaECbp);
        }

        private void ComboSelectEvent_TextChanged(object sender, EventArgs e)
        {
            SaveEventCode();
            LoadEventCode();
        }

        private void ComboSelectEvent_SelectedValueChanged(object sender, EventArgs e)
        {
            SaveEventCode();
            LoadEventCode();
        }

        private void ComboSelectEvent_TextUpdate(object sender, EventArgs e)
        {
            SaveEventCode();
            LoadEventCode();
        }

        void ScanAndDeleteValue(string SandDin)
        {
            SaveEventCode();
            for (int i = 0; i < btnlist.Items.Count; i++) ScanAndDeleteAtFile(SandDin, JustNameToBName(btnlist.Items[i].ToString()));
            for (int i = 0; i < txtlist.Items.Count; i++) ScanAndDeleteAtFile(SandDin, JustNameToBName(txtlist.Items[i].ToString()));
            for (int i = 0; i < chklist.Items.Count; i++) ScanAndDeleteAtFile(SandDin, JustNameToBName(chklist.Items[i].ToString()));
            LoadEventCode();
        }

        void ScanAndDeleteAtFile(string SandDAFin, string SandDAFfile)
        {
            string LTPt1 = "";
            string LTPt2 = "";
            string LTPt3 = "";
            string LTPtxt = "";
            int i2 = 0;
            listScanMain.Items.Clear();
            listScanTemp.Items.Clear();
            int BPDseCodeNum = 0;
            string[] g = File.ReadAllLines(project_path + SandDAFfile + "e.parsinevis");
            for (int i = 0; i < g.Length; i++) listScanMain.Items.Add(g[i]);
            int i3 = listScanMain.Items.Count;
            for (int i = 0; i < i3; i++)
            {
                LTPtxt = listScanMain.Items[i].ToString();
                BPDseCodeNum = Convert.ToInt32(LTPtxt[0].ToString() + LTPtxt[1].ToString());
                switch(BPDseCodeNum)
                {
                    case 10:
                        i2 = 2;
                        LTPt1 = "";
                        LTPt2 = "";
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt1 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt2 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        if (LTPt1 == SandDAFin || LTPt2 == SandDAFin) {DeleteCodeInScan(i); i--; i3 = listScanMain.Items.Count;}                     
                        break;

                    case 12:
                        i2 = 2;
                        LTPt1 = "";
                        LTPt2 = "";
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt1 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt2 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        if (LTPt1 == SandDAFin || LTPt2 == SandDAFin) { DeleteCodeInScan(i); i--; i3 = listScanMain.Items.Count; }
                        break;

                    case 14: // مقدار دهی مستقیم
                        i2 = 2;
                        LTPt1 = "";
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt1 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        if (LTPt1 == SandDAFin) { DeleteCodeInScan(i); i--; i3 = listScanMain.Items.Count; }
                        break;

                    case 15: // مقدار دهی با یک متغیر
                        i2 = 2;
                        LTPt1 = "";
                        LTPt2 = "";
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt1 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt2 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        if (LTPt1 == SandDAFin || LTPt2 == SandDAFin) { DeleteCodeInScan(i); i--; i3 = listScanMain.Items.Count; }
                        break;

                    case 16: // مقدار دهی با دو متغیر
                        i2 = 2;
                        LTPt1 = "";
                        LTPt2 = "";
                        LTPt3 = "";
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt1 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt2 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt3 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        if (LTPt1 == SandDAFin || LTPt2 == SandDAFin || LTPt3 == SandDAFin) { DeleteCodeInScan(i); i--; i3 = listScanMain.Items.Count; }
                        break;

                    case 17:
                        i2 = 2;
                        LTPt1 = "";
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt1 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        if (LTPt1 == SandDAFin) { DeleteCodeInScan(i); i--; i3 = listScanMain.Items.Count; }
                        break;
                }
            }
            string[] SaECbp = new string[listScanMain.Items.Count];
            for (int i = 0; i < listScanMain.Items.Count; i++) SaECbp[i] = listScanMain.Items[i].ToString();
            File.WriteAllLines(project_path + SandDAFfile + "e.parsinevis", SaECbp);
        }

        void DeleteCodeInScan(int DCISindex)
        {
            int BPDd1 = DCISindex;
            int BPDd2 = 0;
            string BPDseCodeStr = listScanMain.Items[DCISindex].ToString();
            int BPDseCodeNum = Convert.ToInt32(BPDseCodeStr[0].ToString() + BPDseCodeStr[1].ToString());
            if (BPDseCodeNum == 10 || BPDseCodeNum == 12)
            {
                int i = 1;
                int i2 = (DCISindex + 1);
                while (i != 0)
                {
                    BPDseCodeStr = listScanMain.Items[i2].ToString();
                    BPDseCodeNum = Convert.ToInt32(BPDseCodeStr[0].ToString() + BPDseCodeStr[1].ToString());
                    if (BPDseCodeNum == 10 || BPDseCodeNum == 12) i++;
                    if (BPDseCodeNum == 11 || BPDseCodeNum == 13) i--;
                    BPDd2 = i2;
                    i2++;
                }
            }
            else BPDd2 = BPDd1;
            listScanTemp.Items.Clear();
            for (int i = 0; i < listScanMain.Items.Count; i++)
            {
                if (i != BPDd1 && i != BPDd2) listScanTemp.Items.Add(listScanMain.Items[i].ToString());
            }
            listScanMain.Items.Clear();
            for (int i = 0; i < listScanTemp.Items.Count; i++) listScanMain.Items.Add(listScanTemp.Items[i].ToString());
        }

        private void TabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            addLISTval();
            SaveEventCode();
            LoadEventCode();
        }

        private void BtnDeleteMOT_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("آیا واقعا می‌خواهید متغیر را حذف کنید؟\nبا حذف این برچسب تمام کدها و رویدادهای مربوط به آن حذف می‌شوند.", "هشدار!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ScanAndDeleteValue(JustNameToBName(motName[edtMOT]));
                mots[edtMOT] = 3;
                listTmpMOT.Items.Remove(motName[edtMOT]);
                NewMode();
                motNewMode();
                radMOTchange(null, null);
            }
            addLISTval();
            UpdateListTmpMot();
            saveAll();
        }

        private void BtnChangeMOT_Click(object sender, EventArgs e)
        {
            if (radCHmotInt.Checked == true) mots[edtMOT] = 0;
            else if (radCHmotFloat.Checked == true) mots[edtMOT] = 1;
            else if (radCHmotString.Checked == true) mots[edtMOT] = 2;
            motName[edtMOT] = txtMOTchangeNewName.Text;
            UpdateListTmpMot();
            radMOTchange(null, null);
            ComboSelectEvent_TextChanged(null, null);
            addLISTval();
        }

        private void ComboSelectEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveEventCode();
            LoadEventCode();
        }

        void UpdateListTmpMot()
        {
            listTmpMOT.Items.Clear();
            for (int i = 0; i < coMOT; i++)
            {
                if (mots[i] == 0 || mots[i] == 1 || mots[i] == 2) listTmpMOT.Items.Add(motName[i]);
            }
            saveAll();
        }

        string ConvertPrToCsharp(int num, string name)
        {
            string CPTCout = "";
            listCompilePr.Items.Clear();
            txtCompilePr.Text = "";
            string[] g = File.ReadAllLines(project_path + name + num.ToString() + "e.parsinevis");
            for (int i = 0; i < g.Length; i++) listCompilePr.Items.Add(g[i]);

            int i2 = 0;
            string LTPtxt = "";
            string LTPt1 = "";
            string LTPt2 = "";
            string LTPt3 = "";
            string LTPt4 = "";
            string LTPt5 = "";
            string LTPt6 = "";
            string LTPt7 = "";
            string LTPt8 = "";
            string bmotl1 = "";
            string bmotl2 = "";
            int LTPmnum = 0;
            for (int i = 0; i < listCompilePr.Items.Count; i++)
            {
                LTPtxt = listCompilePr.Items[i].ToString();
                LTPmnum = Convert.ToInt32((LTPtxt[0].ToString() + LTPtxt[1].ToString()).ToString());
                switch (LTPmnum)
                {
                    case 10:
                        i2 = 2;
                        LTPt1 = "";
                        LTPt2 = "";
                        LTPt3 = "";
                        LTPt4 = "";
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt1 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt2 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        switch (LTPtxt[i2])
                        {
                            case '1':
                                LTPt3 = "==";
                                break;

                            case '2':
                                LTPt3 = "!=";
                                break;

                            case '3':
                                LTPt3 = ">";
                                break;

                            case '4':
                                LTPt3 = "<";
                                break;
                        }
                        LTPt4 = (LTPt1[0].ToString() + LTPt1[1].ToString() + LTPt1[2].ToString()).ToString();
                        if (LTPt4 != "MOT")
                            LTPt1 += ".Text";
                        LTPt4 = (LTPt2[0].ToString() + LTPt2[1].ToString() + LTPt2[2].ToString()).ToString();
                        if (LTPt4 != "MOT")
                            LTPt2 += ".Text";
                        txtCompilePr.AppendText("\nif(" + LTPt1 + " " + LTPt3 + " " + LTPt2 + ") {");
                        break;

                    case 11:
                        txtCompilePr.AppendText("\n}");
                        break;

                    case 12:
                        i2 = 2;
                        LTPt1 = "";
                        LTPt2 = "";
                        LTPt3 = "";
                        LTPt4 = "";
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt1 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt2 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        switch (LTPtxt[i2])
                        {
                            case '1':
                                LTPt3 = "==";
                                break;

                            case '2':
                                LTPt3 = "!=";
                                break;

                            case '3':
                                LTPt3 = ">";
                                break;

                            case '4':
                                LTPt3 = "<";
                                break;
                        }
                        LTPt4 = (LTPt1[0].ToString() + LTPt1[1].ToString() + LTPt1[2].ToString()).ToString();
                        if (LTPt4 != "MOT")
                            LTPt1 += ".Text";
                        LTPt4 = (LTPt2[0].ToString() + LTPt2[1].ToString() + LTPt2[2].ToString()).ToString();
                        if (LTPt4 != "MOT")
                            LTPt2 += ".Text";
                        txtCompilePr.AppendText("\nwhile(" + LTPt1 + " " + LTPt3 + " " + LTPt2 + ") {");
                        break;

                    case 13:
                        txtCompilePr.AppendText("\n}");
                        break;

                    case 14: // مقدار دهی مستقیم
                        i2 = 2;
                        LTPt1 = "";
                        LTPt2 = "";
                        LTPt3 = "";
                        LTPt4 = "";
                        LTPt5 = "";
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt1 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt2 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        LTPt3 = File.ReadAllText(project_path + "fil" + LTPt2 + ".parsinevis");
                        LTPt4 = (LTPt1[0].ToString() + LTPt1[1].ToString() + LTPt1[2].ToString()).ToString();
                        for (i2 = 3; i2 < LTPt1.Length; i2++)
                            LTPt5 += LTPt1[i2].ToString();
                        if (LTPt4 == "MOT")
                        {
                            if (mots[(int.Parse(LTPt5))] == 2)
                                txtCompilePr.AppendText("\n" + LTPt1 + " = \"" + LTPt3 + "\";");
                            else
                                txtCompilePr.AppendText("\n" + LTPt1 + " = " + LTPt3 + ";");
                        }
                        else
                            txtCompilePr.AppendText("\n" + LTPt1 + ".Text = \"" + LTPt3 + "\";");
                        break;

                    case 15: // مقدار دهی با یک متغیر
                        i2 = 2;
                        LTPt1 = "";
                        LTPt2 = "";
                        LTPt3 = "";
                        LTPt4 = "";
                        LTPt5 = "";
                        bmotl1 = "";
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt1 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt2 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        for (i2 = 3; i2 < LTPt1.Length; i2++)
                            LTPt3 += LTPt1[i2].ToString();
                        for (i2 = 3; i2 < LTPt2.Length; i2++)
                            LTPt4 += LTPt2[i2].ToString();
                        LTPt5 = (LTPt2[0].ToString() + LTPt2[1].ToString() + LTPt2[2].ToString()).ToString();
                        bmotl1 = LTPt2;
                        if (LTPt5 != "MOT")
                            LTPt2 += ".Text";
                        LTPt5 = (LTPt1[0].ToString() + LTPt1[1].ToString() + LTPt1[2].ToString()).ToString();
                        if (LTPt5 == "MOT")
                        {
                            switch (mots[(int.Parse(LTPt3))])
                            {
                                case 0:
                                    txtCompilePr.AppendText("\ntry\n{\n" + LTPt1 + " = Convert.ToInt32(" + LTPt2 + ");\n}" +
                                        "\ncatch\n{\nMessageBox.Show(\"مقدار \\n" + NameUserOBJ(bmotl1) + "\\nاز نوع عددی نیست و نمیتوان آن را در متغیر\\n" + motName[(int.Parse(LTPt3))] + "\\nذخیره کرد.\", \"خطا\", MessageBoxButtons.OK, MessageBoxIcon.Error);\n}");
                                    break;

                                case 1:
                                    txtCompilePr.AppendText("\ntry\n{\n" + LTPt1 + " = Convert.ToDouble(" + LTPt2 + ");\n}" +
                                        "\ncatch\n{\nMessageBox.Show(\"مقدار \\n" + NameUserOBJ(bmotl1) + "\\nاز نوع عددی نیست و نمیتوان آن را در متغیر\\n" + motName[(int.Parse(LTPt3))] + "\\nذخیره کرد.\", \"خطا\", MessageBoxButtons.OK, MessageBoxIcon.Error);\n}");
                                    break;

                                case 2:
                                    txtCompilePr.AppendText("\n" + LTPt1 + " = " + LTPt2 + ".ToString();");
                                    break;
                            }
                        }
                        else
                        txtCompilePr.AppendText("\n" + LTPt1 + ".Text = " + LTPt2 + ".ToString();");
                        break;

                    case 16: // مقدار دهی با دو متغیر
                        i2 = 2;
                        LTPt1 = "";
                        LTPt2 = "";
                        LTPt3 = "";
                        LTPt4 = "";
                        LTPt5 = "";
                        LTPt6 = "";
                        LTPt7 = "";
                        LTPt8 = "";
                        bmotl1 = "";
                        bmotl2 = "";
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt1 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt2 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt3 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        switch (LTPtxt[i2])
                        {
                            case '1':
                                LTPt4 = "+";
                                LTPt6 = "به علاوه";
                                break;

                            case '2':
                                LTPt4 = "-";
                                LTPt6 = "منهای";
                                break;

                            case '3':
                                LTPt4 = "*";
                                LTPt6 = "ضربدر";
                                break;

                            case '4':
                                LTPt4 = "/";
                                LTPt6 = "تقسیم بر";
                                break;
                        }
                        LTPt7 = LTPt2;
                        bmotl1 = LTPt7;
                        LTPt8 = LTPt3;
                        bmotl2 = LTPt8;
                        LTPt5 = (LTPt2[0].ToString() + LTPt2[1].ToString() + LTPt2[2].ToString()).ToString();
                        if (LTPt5 != "MOT")
                            LTPt2 += ".Text";
                        LTPt5 = (LTPt3[0].ToString() + LTPt3[1].ToString() + LTPt3[2].ToString()).ToString();
                        if (LTPt5 != "MOT")
                            LTPt3 += ".Text";
                        LTPt5 = (LTPt1[0].ToString() + LTPt1[1].ToString() + LTPt1[2].ToString()).ToString();
                        if (LTPt5 == "MOT")
                        {
                            LTPt5 = "";
                            for (i2 = 3; i2 < LTPt1.Length; i2++)
                                LTPt5 += LTPt1[i2].ToString();
                            switch (mots[(int.Parse(LTPt5))])
                            {
                                case 0:
                                    txtCompilePr.AppendText("\ntry\n{\n" + LTPt1 + " = Convert.ToInt32(" + LTPt2 + " " + LTPt4 + " " + LTPt3 + ");\n}" +
                                        "\ncatch\n{\nMessageBox.Show(\"مقدار \\n" + NameUserOBJ(bmotl1) + "\\n" + LTPt6 + "\\n" + NameUserOBJ(bmotl2) + "\\nاز نوع عددی نیست و نمیتوان آن را در متغیر\\n" + motName[(int.Parse(LTPt5))] + "\\nذخیره کرد.\", \"خطا\", MessageBoxButtons.OK, MessageBoxIcon.Error);\n}");
                                    break;

                                case 1:
                                    txtCompilePr.AppendText("\ntry\n{\n" + LTPt1 + " = Convert.ToDouble(" + LTPt2 + " " + LTPt4 + " " + LTPt3 + ");\n}" +
                                        "\ncatch\n{\nMessageBox.Show(\"مقدار \\n" + NameUserOBJ(bmotl1) + "\\n" + LTPt6 + "\\n" + NameUserOBJ(bmotl2) + "\\nاز نوع عددی نیست و نمیتوان آن را در متغیر\\n" + motName[(int.Parse(LTPt5))] + "\\nذخیره کرد.\", \"خطا\", MessageBoxButtons.OK, MessageBoxIcon.Error);\n}");
                                    break;

                                case 2:
                                    txtCompilePr.AppendText("\n" + LTPt1 + " = (" + LTPt2 + " " + LTPt4 + " " + LTPt3 + ").ToString();");
                                    break;
                            }
                        }
                        else
                            txtCompilePr.AppendText("\n" + LTPt1 + ".Text = (" + LTPt2 + " " + LTPt4 + " " + LTPt3 + ").ToString();");
                        break;

                    case 17:
                        i2 = 2;
                        LTPt1 = "";
                        LTPt2 = "";
                        while (LTPtxt[i2] != '$')
                        {
                            LTPt1 += LTPtxt[i2].ToString();
                            i2++;
                        }
                        i2++;
                        if (LTPtxt[i2] == '1') LTPt2 = "true";
                        else LTPt2 = "false";
                        txtCompilePr.AppendText("\n" + LTPt1 + ".Visible = " + LTPt2 + ";");
                        break;
                }
            }

            CPTCout = txtCompilePr.Text;
            return CPTCout;
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            if (canRun == true)
            {
                saveAll();
                txtlastPr.Text = "";
                txtTP1Pr.Text = "";
                txtTP2Pr.Text = "";
                txtTP3Pr.Text = "";
                txtCPPr.Text = "";
                txtlastPr.AppendText(File.ReadAllText(".//C1.parsinevis"));
                File.WriteAllText(project_path + "tpIm.parsinevis", "");
                txtlastPr.AppendText("\nthis.Size = new Size(" + numFormSizeWidth.Value.ToString() + "," + (numFormSizeHeight.Value + 29).ToString() + ");");
                txtlastPr.AppendText("\nthis.BackColor =  ColorTranslator.FromHtml(\"" + ColorTranslator.ToHtml(Color.FromArgb(panelProgram.BackColor.ToArgb())) + "\");");
                int i = 0;
                for (i = 0; i < coLBL; i++)
                {
                    if (existINlistBox(lbllist, nameLBL[i]) == true)
                    {
                        txtlastPr.AppendText("\nthis.LBL" + i.ToString() + " = new System.Windows.Forms.Label();");
                        txtlastPr.AppendText("\nLBL" + i.ToString() + ".Location = new Point(" + objLBL[i].Location.X.ToString() + "," + objLBL[i].Location.Y.ToString() + ");");
                        txtlastPr.AppendText("\nLBL" + i.ToString() + ".Visible = true;");
                        txtlastPr.AppendText("\nLBL" + i.ToString() + ".Text = \"" + objLBL[i].Text + "\";");
                        txtlastPr.AppendText("\nLBL" + i.ToString() + ".BackColor = ColorTranslator.FromHtml(\"" + ColorTranslator.ToHtml(Color.FromArgb(objLBL[i].BackColor.ToArgb())) + "\");");
                        txtlastPr.AppendText("\nLBL" + i.ToString() + ".ForeColor = ColorTranslator.FromHtml(\"" + ColorTranslator.ToHtml(Color.FromArgb(objLBL[i].ForeColor.ToArgb())) + "\");");
                        txtlastPr.AppendText("\nLBL" + i.ToString() + ".AutoSize = true;");
                        txtlastPr.AppendText("\nthis.Controls.Add(LBL" + i.ToString() + ");");
                        txtTP1Pr.AppendText("\nprivate System.Windows.Forms.Label LBL" + i.ToString() + ";");
                    }
                }
                for (i = 0; i < coBTN; i++)
                {
                    if (existINlistBox(btnlist, nameBTN[i]) == true)
                    {
                        txtlastPr.AppendText("\nthis.BTN" + i.ToString() + " = new System.Windows.Forms.Button();");
                        txtlastPr.AppendText("\nBTN" + i.ToString() + ".Location = new Point(" + objBTN[i].Location.X.ToString() + "," + objBTN[i].Location.Y.ToString() + ");");
                        txtlastPr.AppendText("\nBTN" + i.ToString() + ".Size = new Size(" + objBTN[i].Size.Width.ToString() + "," + objBTN[i].Size.Height.ToString() + ");");
                        txtlastPr.AppendText("\nBTN" + i.ToString() + ".Visible = true;");
                        txtlastPr.AppendText("\nBTN" + i.ToString() + ".Text = \"" + objBTN[i].Text + "\";");
                        txtlastPr.AppendText("\nBTN" + i.ToString() + ".BackColor = ColorTranslator.FromHtml(\"" + ColorTranslator.ToHtml(Color.FromArgb(objBTN[i].BackColor.ToArgb())) + "\");");
                        txtlastPr.AppendText("\nBTN" + i.ToString() + ".ForeColor = ColorTranslator.FromHtml(\"" + ColorTranslator.ToHtml(Color.FromArgb(objBTN[i].ForeColor.ToArgb())) + "\");");
                        txtlastPr.AppendText("\nBTN" + i.ToString() + ".Click += BTN" + i.ToString() + "e;");
                        txtlastPr.AppendText("\nthis.Controls.Add(BTN" + i.ToString() + ");");
                        txtTP1Pr.AppendText("\nprivate System.Windows.Forms.Button BTN" + i.ToString() + ";");
                        txtCPPr.AppendText("\nvoid BTN" + i.ToString() + "e(object sender, EventArgs e)\n{\n" + ConvertPrToCsharp(i, "BTN") + "\n}");
                    }
                }
                for (i = 0; i < coTXT; i++)
                {
                    if (existINlistBox(txtlist, nameTXT[i]) == true)
                    {
                        txtlastPr.AppendText("\nthis.TXT" + i.ToString() + " = new System.Windows.Forms.TextBox();");
                        txtlastPr.AppendText("\nTXT" + i.ToString() + ".Location = new Point(" + objTXT[i].Location.X.ToString() + "," + objTXT[i].Location.Y.ToString() + ");");
                        txtlastPr.AppendText("\nTXT" + i.ToString() + ".Size = new Size(" + objTXT[i].Size.Width.ToString() + "," + objTXT[i].Size.Height.ToString() + ");");
                        txtlastPr.AppendText("\nTXT" + i.ToString() + ".Visible = true;");
                        txtlastPr.AppendText("\nTXT" + i.ToString() + ".Text = \"" + objTXT[i].Text + "\";");
                        txtlastPr.AppendText("\nTXT" + i.ToString() + ".BackColor = ColorTranslator.FromHtml(\"" + ColorTranslator.ToHtml(Color.FromArgb(objTXT[i].BackColor.ToArgb())) + "\");");
                        txtlastPr.AppendText("\nTXT" + i.ToString() + ".ForeColor = ColorTranslator.FromHtml(\"" + ColorTranslator.ToHtml(Color.FromArgb(objTXT[i].ForeColor.ToArgb())) + "\");");
                        txtlastPr.AppendText("\nTXT" + i.ToString() + ".TextChanged += TXT" + i.ToString() + "e;");
                        txtlastPr.AppendText("\nthis.Controls.Add(TXT" + i.ToString() + ");");
                        txtTP1Pr.AppendText("\nprivate System.Windows.Forms.TextBox TXT" + i.ToString() + ";");
                        txtCPPr.AppendText("\nvoid TXT" + i.ToString() + "e(object sender, EventArgs e)\n{\n" + ConvertPrToCsharp(i, "TXT") + "\n}");
                    }
                }
                for (i = 0; i < coIMG; i++)
                {
                    if (existINlistBox(imglist, nameIMG[i]) == true)
                    {
                        string tmpBtnRunStr1 = "StretchImage";
                        if (((PictureBox)objIMG[i]).SizeMode == PictureBoxSizeMode.Zoom) tmpBtnRunStr1 = "Zoom";
                        txtlastPr.AppendText("\nthis.IMG" + i.ToString() + " = new System.Windows.Forms.PictureBox();");
                        txtlastPr.AppendText("\nIMG" + i.ToString() + ".Location = new Point(" + objIMG[i].Location.X.ToString() + "," + objIMG[i].Location.Y.ToString() + ");");
                        txtlastPr.AppendText("\nIMG" + i.ToString() + ".Size = new Size(" + objIMG[i].Size.Width.ToString() + "," + objIMG[i].Size.Height.ToString() + ");");
                        txtlastPr.AppendText("\nIMG" + i.ToString() + ".SizeMode = System.Windows.Forms.PictureBoxSizeMode." + tmpBtnRunStr1 + ";");
                        txtlastPr.AppendText("\nIMG" + i.ToString() + ".Visible = true;");
                        txtlastPr.AppendText("\nthis.Controls.Add(IMG" + i.ToString() + ");");
                        txtTP1Pr.AppendText("\nprivate System.Windows.Forms.PictureBox IMG" + i.ToString() + ";");
                        File.AppendAllText(project_path + "tpIm.parsinevis", "\nstring pic" + i.ToString() + " = \"" + File.ReadAllText(project_path + "imgPIC" + i.ToString() + ".parsinevis") + "\";");
                        txtTP3Pr.AppendText("\nbyte[] bitmapBytes = Convert.FromBase64String(pic" + i.ToString() + ");");
                        txtTP3Pr.AppendText("\n" + File.ReadAllText(".//C4.parsinevis"));
                        txtTP3Pr.AppendText("\nIMG" + i.ToString() + ".Image = image;");
                    }
                }
                for (i = 0; i < coCHK; i++)
                {
                    if (existINlistBox(chklist, nameCHK[i]) == true)
                    {
                        string tmpBtnRunStr1 = "false";
                        if (((CheckBox)objCHK[i]).Checked == true) tmpBtnRunStr1 = "true";
                        txtlastPr.AppendText("\nthis.CHK" + i.ToString() + " = new System.Windows.Forms.CheckBox();");
                        txtlastPr.AppendText("\nCHK" + i.ToString() + ".Location = new Point(" + objCHK[i].Location.X.ToString() + "," + objCHK[i].Location.Y.ToString() + ");");
                        txtlastPr.AppendText("\nCHK" + i.ToString() + ".Visible = true;");
                        txtlastPr.AppendText("\nCHK" + i.ToString() + ".Text = \"" + objCHK[i].Text + "\";");
                        txtlastPr.AppendText("\nCHK" + i.ToString() + ".Checked = " + tmpBtnRunStr1 + ";");
                        txtlastPr.AppendText("\nCHK" + i.ToString() + ".CheckedChanged += CHK" + i.ToString() + "e;");
                        txtlastPr.AppendText("\nthis.Controls.Add(CHK" + i.ToString() + ");");
                        txtTP1Pr.AppendText("\nprivate System.Windows.Forms.CheckBox CHK" + i.ToString() + ";");
                        txtCPPr.AppendText("\nvoid CHK" + i.ToString() + "e(object sender, EventArgs e)\n{\n" + ConvertPrToCsharp(i, "CHK") + "\n}");
                    }
                }
                txtlastPr.AppendText("\n" + File.ReadAllText(".//C2.parsinevis"));
                txtlastPr.AppendText("\n" + txtTP1Pr.Text + "\n}\npublic partial class Form1 : Form\n{\n");
                txtlastPr.AppendText("\n" + txtTP2Pr.Text);
                File.WriteAllText(project_path + project_name + ".cs", txtlastPr.Text);
                File.AppendAllText(project_path + project_name + ".cs", File.ReadAllText(project_path + "tpIm.parsinevis"));
                txtlastPr.Text = "\n";
                for (i = 0; i < coMOT; i++)
                {
                    switch (mots[i])
                    {
                        case 0:
                            txtlastPr.AppendText("int MOT" + i.ToString() + " = 0;\n");
                            break;

                        case 1:
                            txtlastPr.AppendText("double MOT" + i.ToString() + " = 0.00000000000;\n");
                            break;

                        case 2:
                            txtlastPr.AppendText("string MOT" + i.ToString() + " = \"\";\n");
                            break;
                    }
                }
                txtlastPr.AppendText(File.ReadAllText(".//C3.parsinevis"));
                txtlastPr.AppendText("\n" + txtTP3Pr.Text + "\n}\n" + txtCPPr.Text + "\n");
                txtlastPr.AppendText(File.ReadAllText(".//C5.parsinevis"));

                File.AppendAllText(project_path + project_name + ".cs", txtlastPr.Text);
                File.WriteAllText(".\\TMP\\" + project_name + ".cs", File.ReadAllText(project_path + project_name + ".cs"));
                //File.WriteAllText(".\\CSCC\\ruco.bat", ".\\CSCC\\csc.exe -out:" + project_path + project_name + ".exe " + project_path + project_name + ".cs\n" + project_path + project_name + ".exe");
                if (runorsave == true)
                {
                    File.WriteAllText(".\\ruco.bat",
                        ".\\CSCC\\csc.exe -out:.\\TMP\\" + project_name + ".exe .\\TMP\\" + project_name + ".cs" +
                        "\ncopy .\\TMP\\" + project_name + ".exe\"\" \"" + project_path + project_name + ".exe\"" +
                        "\ndel /f .\\TMP\\" + project_name + ".cs" +
                        "\ndel /f .\\TMP\\" + project_name + ".exe" +
                        "\nstart \"\" \"" + project_path + project_name + ".exe\"");
                    System.Diagnostics.Process.Start(".\\rh.vbs");
                }
            }
            else
                MessageBox.Show("خطا در اجرای برنامه. قبل از اجرای برنامه باید خطاهای برنامه بر طرف شوند.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void CheckHolePrErrors()
        {
            listShowErrors.Items.Clear();
            listBackErrors.Items.Clear();
            ListShowErrors_SelectedIndexChanged(null, null);
            int i = 0;
            for (i = 0; i < coBTN; i++)
                if (ObjExists("BTN", i) == true)
                    AddFileErrors("BTN", i);
            for (i = 0; i < coTXT; i++)
                if (ObjExists("TXT", i) == true)
                    AddFileErrors("TXT", i);
            for (i = 0; i < coCHK; i++)
                if (ObjExists("CHK", i) == true)
                    AddFileErrors("CHK", i);
            if (listBackErrors.Items.Count != 0)
                canRun = false;
            else
                canRun = true;
        }

        void AddFileErrors(string AFEt, int num)
        {
            listCheckErrors.Items.Clear();
            string[] g = File.ReadAllLines(project_path + AFEt + num.ToString() + "e.parsinevis");
            string LTPt1 = "";
            string LTPt2 = "";
            string LTPt3 = "";
            string LTPt4 = "";
            string LTPt5 = "";
            int AFiTmp = 0;
            int ITPt1 = 0;
            int ITPt2 = 0;
            for (int i = 0; i < g.Length; i++) listCheckErrors.Items.Add(g[i]);
            for (int i = 0; i < listCheckErrors.Items.Count; i++)
            {
                LTPt1 = "";
                LTPt2 = "";
                LTPt3 = "";
                LTPt4 = "";
                LTPt5 = "";
                ITPt1 = 0;
                ITPt2 = 0;
                string AFline = listCheckErrors.Items[i].ToString();
                int AFnum = Convert.ToInt32(AFline[0].ToString() + AFline[1].ToString());
                switch (AFnum)
                {
                    case 10:
                        AFiTmp = 2;
                        while (AFline[AFiTmp] != '$')
                        {
                            LTPt1 += AFline[AFiTmp].ToString();
                            AFiTmp++;
                        }
                        AFiTmp++;
                        while (AFline[AFiTmp] != '$')
                        {
                            LTPt2 += AFline[AFiTmp].ToString();
                            AFiTmp++;
                        }
                        LTPt3 = LTPt1[0].ToString() + LTPt1[1].ToString() + LTPt1[2].ToString();
                        LTPt4 = LTPt2[0].ToString() + LTPt2[1].ToString() + LTPt2[2].ToString();
                        if (LTPt3 == "MOT" || LTPt4 == "MOT")
                        {
                            if (LTPt3 == "MOT" && LTPt4 == "MOT")
                            {
                                LTPt3 = "";
                                LTPt4 = "";
                                for (int ttt = 3; ttt < LTPt1.Length; ttt++) LTPt3 += LTPt1[ttt].ToString();
                                for (int ttt = 3; ttt < LTPt2.Length; ttt++) LTPt4 += LTPt2[ttt].ToString();
                                ITPt1 = Convert.ToInt32(LTPt3);
                                ITPt2 = Convert.ToInt32(LTPt4);
                                if(mots[ITPt1] != mots[ITPt2])
                                    PostNewError(AFEt + num.ToString() + "$" + i.ToString() + "$10", "در شرط هر دو متغیر باید از یک نوع باشند.");
                            }
                            else
                            {
                                if (LTPt3 == "MOT")
                                {
                                    LTPt3 = "";
                                    for (int ttt = 3; ttt < LTPt1.Length; ttt++) LTPt3 += LTPt1[ttt].ToString();
                                    ITPt1 = Convert.ToInt32(LTPt3);
                                    if (mots[ITPt1] != 2)
                                        PostNewError(AFEt + num.ToString() + "$" + i.ToString() + "$10", "در شرط هر دو متغیر باید از یک نوع باشند.");
                                }
                                else
                                {
                                    LTPt4 = "";
                                    for (int ttt = 3; ttt < LTPt2.Length; ttt++) LTPt4 += LTPt2[ttt].ToString();
                                    ITPt2 = Convert.ToInt32(LTPt4);
                                    if (mots[ITPt2] != 2)
                                        PostNewError(AFEt + num.ToString() + "$" + i.ToString() + "$10", "در شرط هر دو متغیر باید از یک نوع باشند.");
                                }
                            }
                        }
                        break;

                    case 12:
                        AFiTmp = 2;
                        while (AFline[AFiTmp] != '$')
                        {
                            LTPt1 += AFline[AFiTmp].ToString();
                            AFiTmp++;
                        }
                        AFiTmp++;
                        while (AFline[AFiTmp] != '$')
                        {
                            LTPt2 += AFline[AFiTmp].ToString();
                            AFiTmp++;
                        }
                        LTPt3 = LTPt1[0].ToString() + LTPt1[1].ToString() + LTPt1[2].ToString();
                        LTPt4 = LTPt2[0].ToString() + LTPt2[1].ToString() + LTPt2[2].ToString();
                        if (LTPt3 == "MOT" || LTPt4 == "MOT")
                        {
                            if (LTPt3 == "MOT" && LTPt4 == "MOT")
                            {
                                LTPt3 = "";
                                LTPt4 = "";
                                for (int ttt = 3; ttt < LTPt1.Length; ttt++) LTPt3 += LTPt1[ttt].ToString();
                                for (int ttt = 3; ttt < LTPt2.Length; ttt++) LTPt4 += LTPt2[ttt].ToString();
                                ITPt1 = Convert.ToInt32(LTPt3);
                                ITPt2 = Convert.ToInt32(LTPt4);
                                if (mots[ITPt1] != mots[ITPt2])
                                    PostNewError(AFEt + num.ToString() + "$" + i.ToString() + "$12", "در حلقه تکرار شونده هر دو متغیر باید از یک نوع باشند.");
                            }
                            else
                            {
                                if (LTPt3 == "MOT")
                                {
                                    LTPt3 = "";
                                    for (int ttt = 3; ttt < LTPt1.Length; ttt++) LTPt3 += LTPt1[ttt].ToString();
                                    ITPt1 = Convert.ToInt32(LTPt3);
                                    if (mots[ITPt1] != 2)
                                        PostNewError(AFEt + num.ToString() + "$" + i.ToString() + "$12", "در حلقه تکرار شونده هر دو متغیر باید از یک نوع باشند.");
                                }
                                else
                                {
                                    LTPt4 = "";
                                    for (int ttt = 3; ttt < LTPt2.Length; ttt++) LTPt4 += LTPt2[ttt].ToString();
                                    ITPt2 = Convert.ToInt32(LTPt4);
                                    if (mots[ITPt2] != 2)
                                        PostNewError(AFEt + num.ToString() + "$" + i.ToString() + "$12", "در حلقه تکرار شونده هر دو متغیر باید از یک نوع باشند.");
                                }
                            }
                        }
                        break;

                    case 14:
                        AFiTmp = 2;
                        while (AFline[AFiTmp] != '$')
                        {
                            LTPt1 += AFline[AFiTmp].ToString();
                            AFiTmp++;
                        }
                        AFiTmp++;
                        while (AFline[AFiTmp] != '$')
                        {
                            LTPt2 += AFline[AFiTmp].ToString();
                            AFiTmp++;
                        }
                        LTPt3 = File.ReadAllText(project_path + "fil" + LTPt2 + ".parsinevis");                       
                        LTPt4 = LTPt1[0].ToString() + LTPt1[1].ToString() + LTPt1[2].ToString();
                        if (LTPt4 == "MOT")
                        {
                            LTPt2 = "";
                            for (int ttt = 3; ttt < LTPt1.Length; ttt++) LTPt2 += LTPt1[ttt].ToString();
                            if (mots[Convert.ToInt32(LTPt2)] == 0 || mots[Convert.ToInt32(LTPt2)] == 1)
                            {
                                double AFFloatTMP = 0;
                                try
                                {
                                    AFFloatTMP = Convert.ToDouble(LTPt3);
                                }
                                catch
                                {
                                    PostNewError(AFEt + num.ToString() + "$" + i.ToString() + "$14", "در مقداردهی مستقیم متغیرهای عددی و اعشاری نمی‌توان از متن استفاده کرد.");
                                }
                            }
                        }                        
                        break;

                    case 16:
                        AFiTmp = 2;
                        while (AFline[AFiTmp] != '$')
                        {
                            LTPt1 += AFline[AFiTmp].ToString();
                            AFiTmp++;
                        }
                        AFiTmp++;
                        while (AFline[AFiTmp] != '$')
                        {
                            LTPt2 += AFline[AFiTmp].ToString();
                            AFiTmp++;
                        }
                        AFiTmp++;
                        while (AFline[AFiTmp] != '$')
                        {
                            LTPt3 += AFline[AFiTmp].ToString();
                            AFiTmp++;
                        }
                        AFiTmp++;
                        LTPt4 = LTPt2[0].ToString() + LTPt2[1].ToString() + LTPt2[2].ToString();
                        LTPt5 = LTPt3[0].ToString() + LTPt3[1].ToString() + LTPt3[2].ToString();
                        if (LTPt4 == "MOT" || LTPt5 == "MOT")
                        {
                            if (LTPt4 == "MOT" && LTPt5 == "MOT")
                            {
                                LTPt4 = "";
                                LTPt5 = "";
                                for (int ttt = 3; ttt < LTPt2.Length; ttt++) LTPt4 += LTPt2[ttt].ToString();
                                for (int ttt = 3; ttt < LTPt3.Length; ttt++) LTPt5 += LTPt3[ttt].ToString();
                                if (mots[Convert.ToInt32(LTPt4)] != mots[Convert.ToInt32(LTPt5)])
                                    PostNewError(AFEt + num.ToString() + "$" + i.ToString() + "$16", "در مقداردهی متغیرها با دو متغیر، هر دو متغیر باید از یک نوع باشند.");
                                else if (AFline[AFiTmp] != '1' && mots[Convert.ToInt32(LTPt4)] == 2)
                                    PostNewError(AFEt + num.ToString() + "$" + i.ToString() + "$16", "در مقدار دهی متغیرها با دو متغیر، بین دو رشته فقط می‌توان از عملگر به علاوه استفاده کرد.");
                            }
                            else
                            {
                                LTPt4 = "";
                                LTPt5 = "";
                                if (LTPt4 == "MOT")
                                {
                                    for (int ttt = 3; ttt < LTPt2.Length; ttt++) LTPt4 += LTPt2[ttt].ToString();
                                    if (mots[Convert.ToInt32(LTPt4)] != 2)
                                        PostNewError(AFEt + num.ToString() + "$" + i.ToString() + "$16", "در مقداردهی متغیرها با دو متغیر، هر دو متغیر باید از یک نوع باشند.");
                                    else if (AFline[AFiTmp] != '1')
                                        PostNewError(AFEt + num.ToString() + "$" + i.ToString() + "$16", "در مقدار دهی متغیرها با دو متغیر، بین دو رشته فقط می‌توان از عملگر به علاوه استفاده کرد.");
                                }
                                else
                                {
                                    for (int ttt = 3; ttt < LTPt3.Length; ttt++) LTPt5 += LTPt3[ttt].ToString();
                                    if (mots[Convert.ToInt32(LTPt5)] != 2)
                                        PostNewError(AFEt + num.ToString() + "$" + i.ToString() + "$16", "در مقداردهی متغیرها با دو متغیر، هر دو متغیر باید از یک نوع باشند.");
                                    else if (AFline[AFiTmp] != '1')
                                        PostNewError(AFEt + num.ToString() + "$" + i.ToString() + "$16", "در مقدار دهی متغیرها با دو متغیر، بین دو رشته فقط می‌توان از عملگر به علاوه استفاده کرد.");
                                }
                            }
                        }
                        else
                        {
                            if (AFline[AFiTmp] != '1')
                                PostNewError(AFEt + num.ToString() + "$" + i.ToString() + "$16", "در مقدار دهی متغیرها با دو متغیر، بین دو رشته فقط می‌توان از عملگر به علاوه استفاده کرد.");
                        }
                        break;
                }
            }
        }

        bool ObjExists(string OEt, int num)
        {
            bool OEout = false;
            switch (OEt)
            {
                case "LBL":
                    if (LBLvis[num] == true) OEout = true;
                    break;

                case "BTN":
                    if (BTNvis[num] == true) OEout = true;
                    break;

                case "TXT":
                    if (TXTvis[num] == true) OEout = true;
                    break;

                case "IMG":
                    if (IMGvis[num] == true) OEout = true;
                    break;

                case "CHK":
                    if (CHKvis[num] == true) OEout = true;
                    break;

                case "MOT":
                    if (mots[num] == 0 || mots[num] == 1 || mots[num] == 2) OEout = true;
                    break;
            }
            return OEout;
        }

        void PostNewError(string BERR, string FERR)
        {
            listBackErrors.Items.Add(BERR);
            listShowErrors.Items.Add(FERR);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            callerrors();
            saveAll();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            STRUP fstup = new STRUP();
            fstup.Show();
        }

        private void OvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callerrors();
            saveAll();
            Application.Exit();
        }

        private void مدیریتپروژههاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            callerrors();
            saveAll();
            this.Close();
        }

        private void ذخیرهفایلاجراییexeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (canRun == true)
            {
                saveFileDialog1.FileName = "";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    runorsave = false;
                    BtnRun_Click(null, null);
                    File.WriteAllText(".\\ruco.bat",
                        ".\\CSCC\\csc.exe -out:.\\TMP\\" + project_name + ".exe .\\TMP\\" + project_name + ".cs" +
                        "\ncopy .\\TMP\\" + project_name + ".exe\"\" \"" + saveFileDialog1.FileName + "\"" +
                        "\ndel /f .\\TMP\\" + project_name + ".cs" +
                        "\ndel /f .\\TMP\\" + project_name + ".exe");
                    System.Diagnostics.Process.Start(".\\rh.vbs");
                    runorsave = true;
                }
            }
            else
            {
                MessageBox.Show("خطا در ذخیره فایل اجرایی برنامه. قبل از ذخیره فایل اجرایی برنامه باید خطاهای برنامه بر طرف شوند.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void دربارهماToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Aboutus abfo = new Aboutus();
            abfo.ShowDialog();
        }

        private void ListShowErrors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listShowErrors.SelectedIndex != -1 && listShowErrors.Focus() == true)
            {
                btnErrorDetail.Enabled = true;
            }
            else
            {
                btnErrorDetail.Enabled = false;
            }
        }

        private void BtnErrorDetail_Click(object sender, EventArgs e)
        {
            if (listShowErrors.SelectedIndex != -1)
            { 
                EBshow = listShowErrors.Items[listShowErrors.SelectedIndex].ToString();
                EBback = listBackErrors.Items[listShowErrors.SelectedIndex].ToString();
                int ierrrr = 0;
                string tmperrrr = "";
                while (EBback[ierrrr].ToString() != "$")
                {
                    tmperrrr += EBback[ierrrr].ToString();
                    ierrrr++;
                }
                string vterrrr = (EBback[0].ToString() + EBback[1].ToString() + EBback[2].ToString()).ToString();
                switch (vterrrr)
                {
                    case "BTN":
                        EBrname = ("رویداد کلیک دکمه " + NameUserOBJ(tmperrrr).ToString());
                        break;
                        
                    case "TXT":
                        EBrname = ("رویداد تغییر متن جعبه متنی " + NameUserOBJ(tmperrrr).ToString());
                        break;
                        
                    case "CHK":
                        EBrname = ("رویداد تغییر گزینه جعبه گزینه " + NameUserOBJ(tmperrrr).ToString());
                        break;
                }
                EBshdeti = false;
                ErrDeti ruerdd = new ErrDeti();
                ruerdd.ShowDialog();
                if (EBshdeti == true)
                {
                    tabControlMain.SelectTab(tabP);
                    comboSelectEvent.Text = EBrname;
                    ierrrr = 3;
                    tmperrrr = "";
                    while (EBback[ierrrr].ToString() != "$")
                        ierrrr++;
                    ierrrr++;
                    while (EBback[ierrrr].ToString() != "$")
                    {
                        tmperrrr += EBback[ierrrr].ToString();
                        ierrrr++;
                    }
                    listMainProgram.SelectedIndex = Convert.ToInt32(tmperrrr);
                }
            }
            else
            {
                btnErrorDetail.Enabled = false;
            }
        }
    }
}
