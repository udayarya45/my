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
    public partial class bloodgroupmaster : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public bloodgroupmaster()
        {
            MsgBox.backColour = Color.WhiteSmoke;
            MsgBox.buttonBackColour = Color.Red;
            MsgBox.buttonForeColour = Color.WhiteSmoke;
            MsgBox.buttonStyle = FlatStyle.Flat;
            MsgBox.titleBackColour = Color.Red;
            MsgBox.titleForeColour = Color.WhiteSmoke;
            MsgBox.messageBoxStyle = FlatStyle.Flat;
            MsgBox.foreColour = Color.Red;

            MsgBoxRetType.backColour = Color.WhiteSmoke;
            MsgBoxRetType.buttonBackColour = Color.Red;
            MsgBoxRetType.buttonForeColour = Color.WhiteSmoke;
            MsgBoxRetType.buttonStyle = FlatStyle.Flat;
            MsgBoxRetType.titleBackColour = Color.Red;
            MsgBoxRetType.titleForeColour = Color.WhiteSmoke;
            MsgBoxRetType.messageBoxStyle = FlatStyle.Flat;
            MsgBoxRetType.foreColour = Color.Red;
            InitializeComponent();
            Cancil_form();
            InitGrid();
        


        }

        
        private void Cancil_form()
        {
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
            txtstatename.Text = "";
            //txtAlias.Text = "";

        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["BG_ID"].Visible=false;
            dataGridView1.Columns["BG_DESC"].Width = 660;
            dataGridView1.Columns["BG_DESC"].HeaderText = "Blood Group";
           
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from BG_T where BG_DESC like '" + txtstatename.Text + "%' ORDER BY BG_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btn_create_Click(object sender, EventArgs e)
       
        {
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str= sc.DataSource.ToString();
            if (txtstatename.Text == "")
            {
               MyMessageBox.ShowBox("blood Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("Insert_blood '" + txtstatename.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                       MyMessageBox.ShowBox("blood group Created...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                       MyMessageBox.ShowBox("This blood group already exist");
                    }
                    
                }
                catch (Exception ex)
                { }
                finally
                {
                    sc.Close();
                }
            }
        
        
        }

        private void btnUpdate_Click(object sender, EventArgs e)
       
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtstateid.Text == "")
            {
               MyMessageBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            if (txtstatename.Text == "")
            {
               MyMessageBox.ShowBox("blood group Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("Update_tblblood '" + txtstateid.Text + "','" + txtstatename.Text + "'", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                       MyMessageBox.ShowBox("blood group Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                       MyMessageBox.ShowBox("This blood group already exist");
                    }

                }
                catch (Exception ex)
                { }
                finally
                {
                    sc.Close();
                }
            
        }

        
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
                    if (txtstateid.Text == "")
                    {
                       MyMessageBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM BG_T WHERE BG_ID=" + txtstateid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                       MyMessageBox.ShowBox(" blood group Deleted Successfully!!!");
                        Cancil_form();
                        InitGrid();
                    }
                    else
                    {
                       MyMessageBox.ShowBox(" Deleting Error!!!");
                    }
                }
                catch (Exception ex)
                { }
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

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtstatename.Text = dataGridView1.CurrentRow.Cells["BG_DESC"].Value.ToString();
               // txtAlias.Text = dataGridView1.CurrentRow.Cells["alias"].Value.ToString();
                txtstateid.Text = dataGridView1.CurrentRow.Cells["BG_ID"].Value.ToString();
                btnUpdate.Enabled = true;
                btn_delete.Enabled = true;
            }
        }

        private void txtstatename_KeyUp(object sender, KeyEventArgs e)
        {
            LoadGrid();
        }

        

    }
}
