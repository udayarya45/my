using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sms.DAL;
using sms.BLClass;
using System.Data.SqlClient;
using MsgBoxRet;

namespace sms
{
    public partial class Class : Form
    {
        public Class()
        {
            InitializeComponent();
            InitGrid();
        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["CLS_ID"].Visible = false;
            dataGridView1.Columns["CLS_DESC"].Width = 340;
            dataGridView1.Columns["CLS_SEQUENCE"].Width = 350;
            dataGridView1.Columns["CLS_DESC"].HeaderText = "Class";
            dataGridView1.Columns["CLS_SEQUENCE"].HeaderText = "Sequence";

        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();


            SqlDataAdapter da = new SqlDataAdapter("select CLS_DESC,CLS_SEQUENCE,CLS_ID from CLASS_T where CLS_DESC like '" + txtClassname.Text + "%' ORDER BY CLS_SEQUENCE", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void Cancil_form()
        {
            btn_create.Enabled = true;
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            txtsequence.Text = "";
            txtClassname.Text = "";

        }
        private void btn_create_Click(object sender, EventArgs e)
        {

            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (txtClassname.Text == "")
            {
               MyMessageBox.ShowBox("Class name should not be blank");
                return;
            }
            //SqlCommand com = new SqlCommand("Insert_Class_T '" + txtClassname.Text + "','" + txtsequence.Text + "','" + sms.Form1.GlobalLoginUserName.ToString() + "','" + dt1 + "','" + sc.WorkstationId + "'", sc);
            SqlCommand com = new SqlCommand("Insert_Class_T '" + txtClassname.Text + "','" + txtsequence.Text + "'", sc);
            SqlDataReader dr;
            sc.Open();
            try
            {
                dr = com.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                   MyMessageBox.ShowBox("Class Created...");
                    Cancil_form();
                    LoadGrid();
                }
                else
                {
                   MyMessageBox.ShowBox("This Class and Sequence Name already exist");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sc.Close();
            }
        }
       
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtclassid.Text == "")
            {
               MyMessageBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            if (txtClassname.Text == "")
            {
               MyMessageBox.ShowBox("Class Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("update_Class_T '" + txtclassid.Text + "','" + txtClassname.Text + "','" + txtsequence.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                       MyMessageBox.ShowBox("Class Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                       MyMessageBox.ShowBox("This Class Name already exist");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sc.Close();
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtclassid.Text == "")
                    {
                       MyMessageBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE_CLASS_T " + txtclassid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                       MyMessageBox.ShowBox(" Class Deleted Successfully!!!");
                        Cancil_form();
                        InitGrid();
                    }
                    else
                    {
                       MyMessageBox.ShowBox(" Class Already Used!!!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {

                txtClassname.Text = dataGridView1.CurrentRow.Cells["CLS_DESC"].Value.ToString();
                txtsequence.Text = dataGridView1.CurrentRow.Cells["CLS_SEQUENCE"].Value.ToString();
                txtclassid.Text = dataGridView1.CurrentRow.Cells["CLS_ID"].Value.ToString();
                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
        }

        private void Class_Load(object sender, EventArgs e)
        {
            LoadGrid();
            dataGridView1.Columns["CLS_SEQUENCE"].Width = 200;
            dataGridView1.Columns["CLS_DESC"].Width = 200;
            dataGridView1.Columns["CLS_ID"].Width = 0;
        }

        private void txtClassname_TextChanged(object sender, EventArgs e)
        {
            LoadGrid();
            dataGridView1.Columns["CLS_SEQUENCE"].Width = 200;
            dataGridView1.Columns["CLS_DESC"].Width = 200;
            dataGridView1.Columns["CLS_ID"].Width = 0;
        }

        private void txtClassname_Leave(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from Class_T where CLS_DESC='"+txtClassname.Text+"' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count>0)

           {
              MyMessageBox.ShowBox(" This Class is Already Exists!!!");
               btnUpdate.Enabled = false;
            
            
           }
          
        }

        private void txtsequence_Leave(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from Class_T where CLS_SEQUENCE='" + txtsequence.Text + "' ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
               MyMessageBox.ShowBox(" This Sequence is Already Exists!!!");
                btnUpdate.Enabled = false;



            }
        }
    }
}