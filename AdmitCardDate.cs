using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MsgBoxRet;
using sms.DAL;
using sms.BLClass;
using System.Collections;

namespace sms
{
    public partial class AdmitCardDate : Form
    {
        public string classn;

        public string examn;
        //MsgBox msg = new MsgBox(Color.Red,Color.WhiteSmoke,Color.WhiteSmoke,Color.Red,Color.Red,Color.WhiteSmoke,FlatStyle.Flat);
        //int tmpCount;
        public AdmitCardDate()
        {
            MsgBox.backColour = Color.WhiteSmoke;
            MsgBox.buttonBackColour = Color.Red;
            MsgBox.buttonForeColour = Color.WhiteSmoke;
            MsgBox.buttonStyle = FlatStyle.Flat;
            MsgBox.titleBackColour = Color.Red;
            MsgBox.titleForeColour = Color.WhiteSmoke;
            MsgBox.messageBoxStyle = FlatStyle.Flat;
            MsgBox.foreColour = Color.Red;
            InitializeComponent();
            //tmpCount = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CryAdmitCard.dt1 = dateTimePicker1.Value;
            CryAdmitCard.dt2 = dateTimePicker2.Value;
            CryAdmitCard.classn = cmbClass.SelectedValue.ToString();
            CryAdmitCard.examn = cmbExam.Text;
            CryAdmitCard podv = new CryAdmitCard();

            podv.ShowDialog();
        }

      
        private void bindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC +' '+ SEC_DESC) as Class ,* from View_fillfeecollection order by SEC_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "-Select-";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "Class";
            cmbClass.ValueMember = "SEC_ID";
            //dataclass dc = new dataclass();
            //SqlConnection sc = dc.con();
            //SqlDataAdapter da = new SqlDataAdapter(" select * from class-t", sc);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //DataRow row = ds.Tables[0].NewRow();
            //row["CLS_DESC"] = "Select";
            //row["CLS_ID"] = 0;
            //ds.Tables[0].Rows.InsertAt(row, 0);

            //cmbClass.DataSource = ds.Tables[0];
            //cmbClass.DisplayMember = "CLS_DESC";
            //cmbClass.ValueMember = "CLS_ID";
        }
        private void bindExam()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from EXAMINATION_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["EXAM_DESC"] = "Select";
            row["EXAM_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbExam.DataSource = ds.Tables[0];
            cmbExam.DisplayMember = "EXAM_DESC";
            cmbExam.ValueMember = "EXAM_ID";
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AdmitCardDate_Load(object sender, EventArgs e)
        {
            
            bindClass();
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();


        }

       

        private void cmbClass_Leave(object sender, EventArgs e)
        {
            bindExam();

        }

       

       
        }
}