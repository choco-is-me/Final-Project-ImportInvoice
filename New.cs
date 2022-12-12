using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ImportInvoice
{
    public partial class New : Form
    {
        SqlConnection con = new SqlConnection("Data Source=CHOCO;Initial Catalog=Shop;Integrated Security=SSPI;");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        int PID = 0;

        public SqlConnection Con { get => con; set => con = value; }

        public New()
        {
            InitializeComponent();
            DisplayData();
        }
        private void DisplayData()
        {
            Con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from Products", Con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            Con.Close();
        }
        private void ClearData()
        {
            textPid.Text = "";
            textPname.Text = "";
            textBrand.Text = "";
            textPrice.Text = "";
            textQuantity.Text = "";
            textCategory.Text = "";
            textDescription.Text = "";
            textSid.Text = "";
            textRol.Text = "";
            PID = 0;
        }
        //Add button event
        private void Button1_Click(object sender, EventArgs e)
        {
            if (textPid.Text != "" && textPname.Text != "" && textBrand.Text != "" && textPrice.Text != "" && textQuantity.Text != "" && textCategory.Text != "" && textDescription.Text != "" && textSid.Text != "" && textRol.Text != "")
            {
                cmd = new SqlCommand("SET IDENTITY_INSERT [dbo].[Products] ON; INSERT INTO Products(PID, PName, Brand, UnitPrice, UnitsInStock, Category, Description, SID, ROL) VALUES(@PID, @PName, @Brand, @UnitPrice, @UnitsInStock, @Category, @Description, @SID, @ROL); SET IDENTITY_INSERT [dbo].[Products] OFF", Con);
                Con.Open();
                cmd.Parameters.AddWithValue("@PID", textPid.Text);
                cmd.Parameters.AddWithValue("@PName", textPname.Text);
                cmd.Parameters.AddWithValue("@Brand", textBrand.Text);
                cmd.Parameters.AddWithValue("@UnitPrice", textPrice.Text);
                cmd.Parameters.AddWithValue("@UnitsInStock", textQuantity.Text);
                cmd.Parameters.AddWithValue("@Category", textCategory.Text);
                cmd.Parameters.AddWithValue("@Description", textDescription.Text);
                cmd.Parameters.AddWithValue("@SID", textSid.Text);
                cmd.Parameters.AddWithValue("@ROL", textRol.Text);
                cmd.ExecuteNonQuery();
                Con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                this.Hide();
                Receipt re = new Receipt();
                re.Proid.Text = this.textPid.Text;
                re.Proname.Text = this.textPname.Text;
                re.Br.Text = this.textBrand.Text;
                re.Pr.Text = this.textPrice.Text;
                re.Quan.Text = this.textQuantity.Text;
                re.Cat.Text = this.textCategory.Text;
                re.Des.Text = this.textDescription.Text;
                re.Sup.Text = this.textSid.Text;
                re.ShowDialog();
                re.Close();
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }
            textPid.Focus();
        }
        //Delete button event
        private void Button3_Click(object sender, EventArgs e)
        {
            if (PID != 0)
            {
                cmd = new SqlCommand("delete Products where PID=@PID", Con);
                Con.Open();
                cmd.Parameters.AddWithValue("@PID", PID);
                cmd.ExecuteNonQuery();
                Con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
            textPid.Focus();
        }
        //Choose Row By Using Mouse Button Event
        private void DataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            PID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textPname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBrand.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textQuantity.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            textDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            textSid.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            textRol.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            textPid.Focus();
        }
    }
}
