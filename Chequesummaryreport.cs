 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sms.DAL;
using System.Data.SqlClient;
using MsgBoxRet;
using System.Net;
using System.IO;
using Microsoft.Office.Interop.Excel;
        

namespace sms
{
    public partial class Chequesummaryreport : Form
    {
        string hostel;
        string classname;
        SqlDataReader dr;
        public Chequesummaryreport()
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
            
        }

        private void bindclass()
        {

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select SEC_ID, (CLS_DESC + SEC_DESC) as Class  from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID order by ct.CLS_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SEC_ID"] = 0;
            row["Class"] = "Select";

            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbclass.DataSource = ds.Tables[0];
            cmbclass.DisplayMember = "Class";
            cmbclass.ValueMember = "SEC_ID";
        }
       private void Cancil_form()
        {
          
            cmbclass.Text = "-Select-";
         
            dateTimePicker1.Text = DateTime.Now.ToString();
            dateTimePicker2.Text = DateTime.Now.ToString();

        }
       private void btn_save_Click(object sender, EventArgs e)
       {
           bindData();
       }
       private void bindData()
       {

           {
               if (cmbclass.Text == "" || cmbclass.Text == "Select")
               {
                   classname = "%";
               }
               else
               {
                   classname = cmbclass.SelectedValue.ToString();
               }


               if (rdbothhostel.Checked)
               {
                   hostel = "%";
               }
               else
               {
                   if (radiohostler.Checked)
                   {
                       hostel = "Paid";

                   }
                   else
                   {
                       hostel = "Cancel";
                   }
               }
               ReportChequesummary.dt1 = dateTimePicker1.Value;
               ReportChequesummary.dt2 = dateTimePicker2.Value; 
               ReportChequesummary obj = new ReportChequesummary();

               obj.ShowDialog();

           }
       }

        private void btnCancel_Click(object sender, EventArgs e)
        {
          
            Cancil_form();
            
        }

  

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DateTime dt11 = Convert.ToDateTime(dateTimePicker1.Value, System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt12 = Convert.ToDateTime(dateTimePicker2.Value, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dc = new dataclass();
            SqlConnection conn = dc.con();
            SqlDataAdapter objCmd = new SqlDataAdapter("select * from View_chequebounce where  CBdate  between '" + dt11.ToString("MM/dd/yyyy") + "' and'"+dt12.ToString("MM/dd/yyyy")+"'", conn);
            DataSet dt = new DataSet();
            objCmd.Fill(dt);
            if (dt.Tables.Count > 0)
            {
                ExportDataSetToExcel(dt);

            }
        }

        private void ExportDataSetToExcel(DataSet ds)
        {
            
            try
            {
                Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = ExcelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);

                // Loop over DataTables in DataSet.
                DataTableCollection collection = ds.Tables;

                for (int i = collection.Count; i > 0; i--)
                {
                    Microsoft.Office.Interop.Excel.Sheets xlSheets = null;
                    Microsoft.Office.Interop.Excel.Worksheet xlWorksheet = null;
                    //Create Excel Sheets
                    xlSheets = ExcelApp.Sheets;
                    xlWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)xlSheets.Add(xlSheets[1],
                                   Type.Missing, Type.Missing, Type.Missing);

                    System.Data.DataTable table = collection[i - 1];
                    xlWorksheet.Name = table.TableName;

                    for (int j = 1; j < table.Columns.Count + 1; j++)
                    {
                        ExcelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;
                    }

                    // Storing Each row and column value to excel sheet
                    for (int k = 0; k < table.Rows.Count; k++)
                    {
                        for (int l = 0; l < table.Columns.Count; l++)
                        {
                            ExcelApp.Cells[k + 2, l + 1] =
                            table.Rows[k].ItemArray[l].ToString();
                        }
                    }
                    ExcelApp.Columns.AutoFit();
                }
                ((Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();
                ExcelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        


        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btMaximize_Click(object sender, EventArgs e)
        {
            
        }

        private void Receiptsummaryreport_Load(object sender, EventArgs e)
        {
            bindclass();
        }
    }
}