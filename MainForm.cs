using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRCDESTOPAPPLICATION
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

            string conString = ConfigurationManager.ConnectionStrings["MyconString"].ConnectionString;
        private void button1_Click(object sender, EventArgs e)
        {
            RegistrationFrom registration = new RegistrationFrom();
            registration.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("getAllDataDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    //con.Close();
                }

            }
            dvg.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            string query = "select * from RegisterFrom where FirstName like @FirstName + '%'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            sda.SelectCommand.Parameters.AddWithValue(@"FirstName", sechtxt.Text);
            DataTable dt = new DataTable();
            sda.Fill(dt);   
            dvg.DataSource = dt;    

        }

        private void dvg_doubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int selectedRow = dvg.Rows.GetFirstRow(DataGridViewElementStates.Selected);
            try
            {
            int id = Convert.ToInt16(dvg.Rows[selectedRow].Cells[0].Value.ToString());

            RegistrationFrom registrationFroms = new RegistrationFrom();

            registrationFroms.id = id;
            registrationFroms.IsUpdate = true;
            registrationFroms.ShowDialog();

            }
            catch (Exception)
            {

                throw;
            }







        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection();
            //string query = "delete from RegisterFrom where userId=@UserId";
            //SqlCommand cmd = new SqlCommand(query, con);
            //cmd.Parameters.AddWithValue(@"UserId",);

            //con.Open();

            //int a = cmd.ExecuteNonQuery();
            //if (a > 0)
            //{
            //    MessageBox.Show("Recoerd Delete Succesfully");

            //}

            //else
            //{
            //    MessageBox.Show("Record Are not Deleted");
            //}
            //con.Close();
        }
    }

}
    
