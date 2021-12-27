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
    public partial class CustomerForm : Form
    {
        SqlConnection connection = new SqlConnection(Utils.conn);
        int id;
        DataTable table;

        public CustomerForm()
        {
            InitializeComponent();

            loadgrid();
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
            SqlDataAdapter adapter = new SqlDataAdapter("select * from customer", connection);
            table = new DataTable();
            adapter.Fill(table);

            dataGridView1.DataSource = table;
        }

        bool val()
        {
            if(textBox2.TextLength < 1 || textBox3.TextLength < 1 || textBox4.TextLength < 1)
            {
                MessageBox.Show("All Fields Must be Filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        void clear()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        void search()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from customer where name_customer like '%" + textBox1.Text + "%'", connection);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (val())
            {
                string com = "insert into customer values('" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "')";
                try
                {
                    Command.exec(com);
                    MessageBox.Show("Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(""+ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                loadgrid();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if(id != 0)
            {
                if (val())
                {
                    string com = "update customer set name_customer = '" + textBox2.Text + "', phone_number_customer = '" + textBox3.Text + "', address_customer = '" + textBox4.Text + "' where id_customer = " + id;
                    try
                    {
                        Command.exec(com);
                        MessageBox.Show("Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(""+ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    loadgrid();
                }
            }
            else
            {
                MessageBox.Show("Select an Item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (id != 0)
            {
                DialogResult res = MessageBox.Show("Are you sure to delete " + dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    string com = "delete from customer where id_customer =" + id;
                    try
                    {
                        Command.exec(com);
                        MessageBox.Show("Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(""+ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    loadgrid();
                }
            }
            else
            {
                MessageBox.Show("Select an Item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
        }
    }
}
