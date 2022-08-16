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
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close this tab?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtEnrollment.Clear();
            txtDepartment.Clear();
            txtSemester.Clear();
            txtContact.Clear();
            //txtEmail.Clear();

            txtEmail.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtEnrollment.Text != "" && txtDepartment.Text != "" && txtSemester.Text != "" && txtContact.Text != "" && txtEmail.Text != "")
            {

                String sname = txtName.Text;
                String enroll = txtEnrollment.Text;
                String dep = txtDepartment.Text;
                String sem = txtSemester.Text;
                String email = txtEmail.Text; 
                Int64 contact = Int64.Parse(txtContact.Text);


                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["LMSConnectionString"].ConnectionString;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                con.Open();
                //cmd.CommandText = "insert into NewStudent (sname,enroll,dep,sem,contact,email) values ('" + name + "','" + enroll + "','" + dep + "','" + sem + "'," + mobile + ",'" + email + "')";

                //using parameterize query or store procedure...
                cmd.CommandText = "INSERT INTO NewStudent (sname,enroll,dep,sem,contact,email) VALUES (@sn, @en, @dep, @sem, @ct, @em) ";
                cmd.Parameters.AddWithValue("@sn", sname);
                cmd.Parameters.AddWithValue("@en", enroll);
                cmd.Parameters.AddWithValue("@dep", dep);
                cmd.Parameters.AddWithValue("@sem", sem);
                cmd.Parameters.AddWithValue("@ct", contact);
                cmd.Parameters.AddWithValue("@em", email);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please Fill Empty Fields", "Suggest", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //[3] 
        private void Ctrl_KeyDown1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                this.SelectNextControl((Control)sender, false, true, true, true);
            }
            else if (e.KeyCode == Keys.Down || (e.KeyCode == Keys.Enter))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            
        }   //END[3]
                
        // [4] Input Validation = contact must be numeric.
        

        private void txtContact_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Error, this box cannot contain letters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }// END [4]
        }
    }
}
