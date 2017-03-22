using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace MINI_Telephone_Diary
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=RAJA;Initial Catalog=PhoneEntry;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxMobile.Clear();
            textBoxEmail.Clear();
            comboBox1.SelectedIndex = -1;
            textBoxFirstName.Focus();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Mobiles(First, Last, Mobile, Email, Category) VALUES('" + textBoxFirstName.Text + "','" + textBoxLastName.Text + "','" + textBoxMobile.Text + "','" + textBoxEmail.Text + "','" + comboBox1.Text + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Successfully Saved.");
            display();
        }

        void display()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select *  from Mobiles", con);
            DataTable dt = new DataTable();

            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
            }
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            display();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBoxFirstName.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBoxLastName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBoxMobile.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBoxEmail.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString(); 
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Mobiles WHERE (Mobile = '"+textBoxMobile.Text+"')", con);
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Delete Successfully.");
            display();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Mobiles SET First='"+textBoxFirstName.Text+"', Last='"+textBoxLastName.Text+"', Mobile='"+textBoxMobile.Text+"', Email='"+textBoxEmail.Text+"', Category='"+comboBox1.Text+"' WHERE Mobile = '" + textBoxMobile.Text + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Updated Successfully.");
            display();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("select *  from Mobiles where Mobile like '"+ textBoxSearch.Text +"%' ", con);
            DataTable dt = new DataTable();

            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
            }
        }
    }
}
