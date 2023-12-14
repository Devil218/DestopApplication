using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRCDESTOPAPPLICATION
{
    public partial class RegistrationFrom : Form
    {
        string conString =  ConfigurationManager.ConnectionStrings["MyconString"].ConnectionString;
        public RegistrationFrom()
        {
            InitializeComponent();
        }
        private readonly int _id = 0;
        private bool _isUpdate = false;

        public int id
        {
            get;set;
        }

        public bool IsUpdate
        {
            get; set;
        }

        string Gender = string.Empty;
        private DataTable populateCombobox()
        {
            DataTable dt = new DataTable();
            using(SqlConnection con = new SqlConnection(conString))
            {

                using (SqlCommand cmd = new SqlCommand("getCity", con))
                {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader(); 
                dt.Load(reader);
                con.Close();    

                }
                City_comboBox.DataSource = dt;  
                City_comboBox.DisplayMember = "City_Name";
                City_comboBox.ValueMember = "City_Id";   
               
            }
            return dt;
        }

        private void RegistrationFrom_Load(object sender, EventArgs e)
        {
            populateCombobox();
        }

        private void LoadDataforUpdate()
        {
            //DataTable dt = new  DataTable();
            //using(SqlConnection con = new SqlConnection(conString))
            //{
            //    using(SqlCommand cmd = new SqlCommand())
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection cons = new SqlConnection(conString))
            {
                    cons.Open();
                using (SqlCommand cmd = new SqlCommand("RegistrationDeatils", cons))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(@"@FirstName", textBox1.Text.ToString());
                    cmd.Parameters.AddWithValue(@"LastName", textBox2.Text.ToString());
                    cmd.Parameters.AddWithValue(@"Gender",Gender);
                    cmd.Parameters.AddWithValue(@"is_skill_c#", checkBox1.Checked);
                    cmd.Parameters.AddWithValue(@"is_skill_js", checkBox2.Checked);
                    cmd.Parameters.AddWithValue(@"is_skill_ReactJs", checkBox3.Checked);
                    cmd.Parameters.AddWithValue(@"City_Id", City_comboBox.SelectedValue);
                    cmd.Parameters.AddWithValue(@"DOB", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue(@"User_Image", getPhoto());

                    int a =cmd.ExecuteNonQuery();
                if(a>0)
                MessageBox.Show("Data Succesfully Added");
                    else
                    {
                        MessageBox.Show("Data Filed");
                    }
                    cons.Close();
                }


            }

            


        }

        private byte[] getPhoto()
        {
            MemoryStream stream = new MemoryStream();   
            pictureBox1.Image.Save(stream,pictureBox1.Image.RawFormat);

            return  stream.GetBuffer();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Gender = "Male";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Gender = "Female";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd/mm/yyyy";
        }

        private void dob_keyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Back)
            {
                dateTimePicker1.CustomFormat = "dd/mm/yyyy";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();   

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog.FileName);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection();
            //string query = "delete from RegisterFrom where userId=@UserId";
            //SqlCommand cmd = new SqlCommand(query,con);
            //cmd.Parameters.AddWithValue(@"UserId", id);

            //con.Open();

            //int a = cmd.ExecuteNonQuery();
            //if(a>0) {
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
