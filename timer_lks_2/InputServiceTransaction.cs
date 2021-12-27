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
    public partial class InputServiceTransaction : Form
    {
        SqlConnection connection = new SqlConnection(Utils.conn);
        int id_customer, id_header;

        public InputServiceTransaction()
        {
            InitializeComponent();
            loadserv();
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

        void change()
        {
            int total1 = 0;
            int total2 = 0;

            for(int i = 0; i < dataGridView1.RowCount -1; i++)
            {
                total1 += Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                total2 += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
            }
            lbltotal1.Text = total2.ToString();
            lbltotal2.Text = total1.ToString();

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
            DialogResult result = MessageBox.Show("Are you sure to Log out ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MainLogin main = new MainLogin();
                this.Hide();
                main.ShowDialog();
            }
        }

        void loadserv()
        {
            string com = "select * from service";
            
            comboBox1.DataSource = Command.getData(com);
            comboBox1.DisplayMember = "name_service";
            comboBox1.ValueMember = "id_service";
        }

        void clear()
        {
            comboBox1.Text = "";
            numericUpDown1.Value = 0;
            txtaddress.Text = "";
            lblpeople.Text = "People";
            textBox1.Text = "";
            lbltotal1.Text = "0";
            lbltotal2.Text = "0";
            dataGridView1.Rows.Clear();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select * from service where id_service =" + comboBox1.SelectedValue, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                var id = Convert.ToInt32(reader["id_service"]);
                var name = reader["name_service"].ToString();
                var price = Convert.ToInt32(reader["price_unit_service"]);
                var estimate = Convert.ToInt32(reader["estimation_duration_service"]);
                connection.Close();

                int rows = dataGridView1.Rows.Add();
                dataGridView1.Rows[rows].Cells[0].Value = id;
                dataGridView1.Rows[rows].Cells[1].Value = name;
                dataGridView1.Rows[rows].Cells[2].Value = numericUpDown1.Value;
                dataGridView1.Rows[rows].Cells[3].Value = estimate;
                dataGridView1.Rows[rows].Cells[4].Value = Convert.ToInt32(numericUpDown1.Value) * price;
            }
            catch (Exception)
            {

                throw;
            }

            change();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select top(1) * from customer where phone_number_customer like '%" + textBox1.Text + "%' order by id_customer desc", connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    lblpeople.Text = reader["name_customer"].ToString();
                    id_customer = Convert.ToInt32(reader["id_customer"]);
                    txtaddress.Text = reader["address_customer"].ToString();
                    lblpeople.Text = reader["name_customer"].ToString();

                }
                connection.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            AddCustomer add = new AddCustomer();
            this.Hide();
            add.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow != null)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
            change();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if(dataGridView1.RowCount > 1 )
            {
                if(id_customer != 0)
                {
                    DateTime est = DateTime.Now.AddHours(Convert.ToInt32(lbltotal2.Text));
                    string com = "insert into header_transaction values(" + Model.id + ", " + id_customer + ", '" + DateTime.Now + "', '" + est + "')";
                    try
                    {
                        Command.exec(com);

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message + "header");
                    }

                    SqlCommand command = new SqlCommand("select top (1) * from header_transaction order by id_header_transaction desc", connection);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        reader.Read();
                        if (reader.HasRows)
                        {
                            id_header = Convert.ToInt32(reader["id_header_transaction"]);

                            try
                            {
                                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                                {
                                    string comm = "insert into detail_transaction(id_service, id_header_transaction, price_detail_transaction, total_unit_detail_transaction) values(" + Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value) + ", " + id_header + ", " + Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value) + ", " + Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value) + ")";
                                    Command.exec(comm);
                                }
                                MessageBox.Show("Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message + "+detail");
                            }
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("read " + ex);
                    }
                    clear();
                }
                else
                {
                    MessageBox.Show("Select A Customer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Select Service", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
        }
    }
}
