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
    public partial class ViewBook : Form
    {
        public ViewBook()
        {
            InitializeComponent();
        }

        private void ViewBook_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LMSConnectionString"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "SELECT * FROM Newbook";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        int bid;
        Int64 rowid;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
              if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
              {
                bid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
              }
        

            panel2.Visible = true;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["LMSConnectionString"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "SELECT * FROM Newbook WHERE bid=@bid";
            cmd.Parameters.AddWithValue("@bid", bid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

      
            rowid = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
  
            txtBname.Text = ds.Tables[0].Rows[0][1].ToString();
            txtAuthor.Text = ds.Tables[0].Rows[0][2].ToString();
            txtPublication.Text = ds.Tables[0].Rows[0][3].ToString();
            txtPdate.Text = ds.Tables[0].Rows[0][4].ToString();
            txtPrice.Text = ds.Tables[0].Rows[0][5].ToString();
            txtQty.Text = ds.Tables[0].Rows[0][6].ToString(); 

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Unsaved Data Will Be Lost.", "Are you Sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                panel2.Visible = false;
            }

        }

        private void txtBookName_TextChanged(object sender, EventArgs e)
        {
            if(txtBookName.Text != "")
            {
                String BookName = txtBookName.Text;

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["LMSConnectionString"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT * FROM Newbook WHERE bName LIKE @BookName";
                cmd.Parameters.AddWithValue("@BookName", $"%{BookName}%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

            }
            else
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["LMSConnectionString"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT * FROM Newbook";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtBookName.Clear();
            panel2.Visible = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Data Will Be Updated. Confirm?", "Success", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
           {
            String bname = txtBname.Text;
            String bAuthor = txtAuthor.Text;
            String publication = txtPublication.Text;
            String pdate = txtPdate.Text;
            Int64 price = Int64.Parse(txtPrice.Text);
            Int64 quan = Int64.Parse(txtQty.Text);

            SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["LMSConnectionString"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "UPDATE Newbook SET bName = @bname ,bAuthor= @bAuthor, bPubl= @publ, bDate= @pdate, bPrice=@price, bQuan=@quan WHERE bid= @rowid ";
                
                cmd.Parameters.AddWithValue("@bname", bname);
                cmd.Parameters.AddWithValue("@bAuthor", bAuthor);
                cmd.Parameters.AddWithValue("@publ", publication);
                cmd.Parameters.AddWithValue("@pdate", pdate);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@quan", quan);
                cmd.Parameters.AddWithValue("@rowid", rowid);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

                ViewBook_Load(this, null);            
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Data Will Be Deleted. Confirm?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {              
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["LMSConnectionString"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "DELETE FROM Newbook WHERE bid=@rowid";
                cmd.Parameters.AddWithValue("@rowid", rowid);


                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                ViewBook_Load(this, null);
            }
        }
        
        private void Crtl_Keydown1(object sender, KeyEventArgs e)// Move to next textbox when enter key or arrow key is pressed
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

        private void txt_Price_Qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Error, this box cannot contain letters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
