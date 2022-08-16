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
    public partial class AddBooks : Form
    {
        public AddBooks()
        {
            InitializeComponent();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBookName.Text != "" && txtAuthor.Text != "" && txtPublication.Text != "" && txtPrice.Text != "" && txtQty.Text != "")
            {

                string bname = txtBookName.Text;
                string bAuthor = txtAuthor.Text;
                string publication = txtPublication.Text;
                string pdate = dateTimePicker1.Text;
                Int64 price = Int64.Parse(txtPrice.Text);
                Int64 quan = Int64.Parse(txtQty.Text);
                 
                
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["LMSConnectionString"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                con.Open();
                // cmd.CommandText = "INSERT INTO Newbook (bName,bAuthor,bPubl,bDate,bPrice,bQuan) VALUES ('" + bname + "','" + bauthor + "','" + publication + "','" + pdate + "','" + price + "','" + quan + "')";
                
                //using parameterize query or store procedure...
                cmd.CommandText = "INSERT INTO Newbook (bName,bAuthor,bPubl,bDate,bPrice,bQuan) VALUES (@bn, @ba, @publ, @pdate, @price, @quan)";
                cmd.Parameters.AddWithValue("@bn", bname);
                cmd.Parameters.AddWithValue("@ba", bAuthor);
                cmd.Parameters.AddWithValue("@publ", publication);
                cmd.Parameters.AddWithValue("@pdate", pdate);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@quan", quan);


                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Data Saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBookName.Clear();
                txtAuthor.Clear();
                txtPublication.Clear();
                txtPrice.Clear();
                txtQty.Clear();
                
            }
            else
            {
                MessageBox.Show("Empty Field NOT Allowed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure? This will DELETE your Unsaved DATA.", "Are you Sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }




        //[3] Move to next textbox when enter key or arrow key is pressed

        private void CtrlKeyDown1(object sender, KeyEventArgs e)
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
        }

      

        // END [3]

        // [4] Input Validation = Price and Qty must be numeric data.
        private void txt_price_quan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Error, this box cannot contain letters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        
        // END [4]
    }
    }
    