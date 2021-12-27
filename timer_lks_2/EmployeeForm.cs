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
    public partial class EmployeeForm : Form
    {
        SqlConnection connection = new SqlConnection(Utils.conn);
        DataTable table;
        int id;
        public EmployeeForm()
        {
            InitializeComponent();

            lbltime.Text = DateTime.Now.ToString("dddd, dd-MM-yyyy HH:mm:ss");
            lblname.Text = Model.name;
            loadgrid();
            loadjob();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EmployeeForm employeeForm = new EmployeeForm();
            this.Hide();
            employeeForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ServiceForm form = new ServiceForm();
            this.Hide();
            form.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PackageForm form = new PackageForm();
            this.Hide();
            form.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            InputServiceTransaction transaction = new InputServiceTransaction();
            this.Hide();
            transaction.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            InputPackageTransaction transaction = new InputPackageTransaction();
            this.Hide();
            transaction.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ViewTransaction transaction = new ViewTransaction();
            this.Hide();
            transaction.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            CustomerForm form = new CustomerForm();
            this.Hide();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to exit ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainLogin main = new MainLogin();
            this.Hide();
            main.ShowDialog();
        }

        void loadgrid()
        {
            string com = "select * from employee join job on employee.id_job = job.id_job";
            dataGridView1.DataSource = Command.getData(com);
            dataGridView1.Columns[2].Visible = false;
        }
        void loadjob()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from job", connection);
            DataTable data = new DataTable();
            adapter.Fill(data);

            comboBox1.DataSource = data;
            comboBox1.DisplayMember = "name_job";
            comboBox1.ValueMember = "id_job";
        }
        bool val()
        {
            if(txtname.TextLength < 1 || txtemail.TextLength < 1 || txtaddress.TextLength < 1 || txtpass.TextLength < 1 || textBox6.TextLength < 1 || txtphone.TextLength < 1 || txtxsalary.TextLength < 1 || comboBox1.Text.Length < 1 || dateTimePicker1.Value == null)
            {
                MessageBox.Show("All FIeld Must be Filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(textBox6.Text != txtpass.Text)
            {
                MessageBox.Show("Confirm Password Doesn't same with password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        bool val_up()
        {
            if (txtname.TextLength < 1 || txtemail.TextLength < 1 || txtaddress.TextLength < 1 || txtphone.TextLength < 1 || txtxsalary.TextLength < 1 || comboBox1.Text.Length < 1 || dateTimePicker1.Value == null)
            {
                MessageBox.Show("All FIeld Must be Filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if(id == 0)
            {
                MessageBox.Show("Please select an item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;

        }

        void clear()
        {
            txtname.Text = "";
            txtemail.Text = "";
            txtaddress.Text = "";
            txtpass.Text = "";
            textBox6.Text = "";
            txtphone.Text = "";
            txtxsalary.Text = "";
            comboBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        void search()
        {
            string com = "select * from employee join job on employee.id_job = job.id_job where name_employee like '%" + textBox1.Text + "%'";
            dataGridView1.DataSource = Command.getData(com);

            dataGridView1.Columns[2].Visible = false;

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (val())
            {
                string com = "insert into employee values(" + comboBox1.SelectedValue + ", '"+txtpass.Text+"', '" + txtname.Text + "', '" + txtemail.Text + "', '" + txtaddress.Text + "', '" + txtphone.Text + "', '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "', " + Convert.ToDecimal(txtxsalary.Text) + ")";
            
                try
                {
                    Command.exec(com);

                    MessageBox.Show("Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                loadgrid();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
            txtname.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtemail.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            txtaddress.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            txtphone.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[7].Value);
            txtxsalary.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();

            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[10].Value.ToString();
            comboBox1.SelectedValue = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[9].Value);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if(val_up())
            {
                string com = "update employee set id_job = " + comboBox1.SelectedValue + ", name_employee = '" + txtname.Text + "', email_employee = '" + txtemail.Text + "', address_employee = '" + txtaddress.Text + "', phone_number_employee = '" + txtphone.Text + "', date_of_birth_employee = '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "', salary_Employee = " + Convert.ToDecimal(txtxsalary.Text) + " where id_employee = " + id;
                try
                {
                    Command.exec(com);
                    MessageBox.Show("Success", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(""+ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                loadgrid();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if(id != 0)     
            {
                DialogResult result = MessageBox.Show("Are you sure to delete " + dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string com = "delete from employee where id_employee = " + id;
                    try
                    {
                        Command.exec(com);
                        MessageBox.Show("Success", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    clear();
                    loadgrid();
                }
            }
            else
            {
                MessageBox.Show("Please select an item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                txtpass.PasswordChar = '\0';
                textBox6.PasswordChar = '\0';
            }
            else
            {
                txtpass.PasswordChar = '*';
                textBox6.PasswordChar = '*';
            }
        }
    }
}
