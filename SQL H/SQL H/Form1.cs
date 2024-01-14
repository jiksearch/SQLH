using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SQL_H
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            try
            {
                var connStrBldr = new System.Data.SqlClient.SqlConnectionStringBuilder();
                connStrBldr.DataSource = textBoxServer.Text.Trim();

                connStrBldr.IntegratedSecurity = false;
                connStrBldr.UserID = textBoxUser.Text.Trim();
                connStrBldr.Password = textBoxPass.Text.Trim();

                using (SqlConnection sqlCon = new SqlConnection(connStrBldr.ToString()))
                {
                    sqlCon.Open();
                    //SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT  sys.databases.name,  CONVERT(VARCHAR,SUM(size)*8/1024)+' MB' AS [TotalDiskSpace]  FROM sys.databases JOIN sys.master_files ON  sys.databases.database_id=sys.master_files.database_id GROUP BY sys.databases.name  ORDER BY sys.databases.name ", sqlCon);
                    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT   sys.databases.[name], IIF([type]= 0 , 'Main','log') AS Type,  CONVERT(VARCHAR,(size*8/1024))+'MB' AS Size FROM sys.databases JOIN sys.master_files ON  sys.databases.database_id=sys.master_files.database_id ORDER BY sys.databases.name ", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dataGridView1.DataSource = dtbl;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var connStrBldr = new System.Data.SqlClient.SqlConnectionStringBuilder();
                connStrBldr.DataSource = textBoxServer.Text.Trim();

                connStrBldr.IntegratedSecurity = false;
                connStrBldr.UserID = textBoxUser.Text.Trim();
                connStrBldr.Password = textBoxPass.Text.Trim();

                using (SqlConnection sqlCon = new SqlConnection(connStrBldr.ToString()))
                {
                    sqlCon.Open();
                    SqlDataAdapter sqlDa = new SqlDataAdapter("select [name], type,  CONVERT(VARCHAR,(size*8/1024))+'MB' AS Size   from sys.database_files", sqlCon);
                    DataTable dtbl = new DataTable();
                    sqlDa.Fill(dtbl);
                    dataGridView1.DataSource = dtbl;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

