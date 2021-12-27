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

namespace timer_lks_2
{
    public partial class MainLogin : Form
    {
        SqlConnection connection = new SqlConnection(Utils.conn);
        SqlCommand command;
        public MainLogin()
        {
            InitializeComponent();
        }

        bool val()
        {
            if(textBox1.TextLength < 1 || textBox2.TextLength < 1)
            {
                MessageBox.Show("All Fields Must be Filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (val())
            {
                command = new SqlCommand("select * from employee where email_employee = '" + textBox1.Text + "' and password_employee = '" + textBox2.Text + "'", connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        Model.id = Convert.ToInt32(reader["id_employee"]);
                        Model.name = reader["name_employee"].ToString();
                        Model.phone = reader["phone_number_employee"].ToString();
                        Model.id_job = Convert.ToInt32(reader["id_job"]);
                        Model.email = reader["email_employee"].ToString();

                        MainForm form = new MainForm();
                        this.Hide();
                        form.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Can't Find User", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    connection.Close();
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to close ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
