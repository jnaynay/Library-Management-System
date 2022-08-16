using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace LibaryMS
{
    public partial class IsuueBooks : Form
    {
        public IsuueBooks()
        {
            InitializeComponent();
        }

        private void IsuueBooks_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LMSConnectionString"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();

            cmd = new SqlCommand("select bName from NewBook", con);
            SqlDataReader Sdr = cmd.ExecuteReader();

            while (Sdr.Read())
            {
                for (int i = 0; i < Sdr.FieldCount; i++)
                {
                    comboBoxBooks.Items.Add(Sdr.GetString(i));
                }
            }
            Sdr.Close();
            con.Close();
        }

        int count;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtEnrollment.Text != "")
            {
                String eid = txtEnrollment.Text;
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["LMSConnectionString"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "select * from NewStudent where enroll = '" + eid + "'";
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataSet DS = new DataSet();
                DA.Fill(DS);


                //-------------------------------------------------------------------------
                //nup va puem me juk hua nai enrollement
                cmd.CommandText = "select count(std_enroll) from ISBook where std_enroll = '" + eid + "' and book_return_date is null";
                SqlDataAdapter DA1 = new SqlDataAdapter(cmd);
                DataSet DS1 = new DataSet();
                DA.Fill(DS1);

                count = int.Parse(DS1.Tables[0].Rows[0][0].ToString());

                //-------------------------------------------------------------------------

                if (DS.Tables[0].Rows.Count != 0)
                {
                    txtName.Text = DS.Tables[0].Rows[0][1].ToString();
                    txtDep.Text = DS.Tables[0].Rows[0][3].ToString();
                    txtSem.Text = DS.Tables[0].Rows[0][4].ToString();
                    txtContact.Text = DS.Tables[0].Rows[0][5].ToString();
                    txtEmail.Text = DS.Tables[0].Rows[0][6].ToString();
                }
                else
                {
                    txtName.Clear();
                    txtDep.Clear();
                    txtSem.Clear();
                    txtContact.Clear();
                    txtEmail.Clear();
                    MessageBox.Show("Invalid Enrollement No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private void btnIssueBook_Click(object sender, EventArgs e)
        {   

            if (txtName.Text != "")
            {
                
                if (comboBoxBooks.SelectedIndex != -1 && count <= 2)
                {
                    String enroll = txtEnrollment.Text;
                    String sname = txtName.Text;
                    String sdep = txtDep.Text;
                    String sem = txtSem.Text;
                    Int64 contact = Int64.Parse(txtContact.Text);
                    String email = txtEmail.Text;
                    String bookname = comboBoxBooks.Text;
                    String bookIssueDate = dateTimePicker1.Text;

                    String eid = txtEnrollment.Text;
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["LMSConnectionString"].ConnectionString;

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = cmd.CommandText = "insert into ISBook (std_enroll ,std_name, std_dep, std_sem, std_contact, std_email, book_name, book_issue_date) values(@enroll, @sname, @dep, @sem, @contact, @email, @bname, @isdate)";
                    cmd.Parameters.AddWithValue("@enroll", enroll);
                    cmd.Parameters.AddWithValue("@sname", sname);
                    cmd.Parameters.AddWithValue("@dep", sdep);
                    cmd.Parameters.AddWithValue("@sem", sem);
                    cmd.Parameters.AddWithValue("@contact", contact);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@bname", bookname);
                    cmd.Parameters.AddWithValue("isdate", bookIssueDate);
                    

                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Book Issued. ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBoxBooks.Text = null;
                    txtEnrollment.Text = null;
                    txtEnrollment.Text = enroll;
                }
                else
                {
                    MessageBox.Show("No Book Selected. OR Maximum number of book has been ISSUED", "Unable to terminate process", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Enter Valid Enrollement No", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

         private void txtEnrollment_TextChanged_1(object sender, EventArgs e)
        {
                 if (txtEnrollment.Text == "")
               {
                txtName.Clear();
                txtDep.Clear();
                txtSem.Clear();
                txtContact.Clear();
                txtEmail.Clear();

                  }
              }

        
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtEnrollment.Clear();
        }

        private void txtExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close this tab?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void txtEnrollment_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(this, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
