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
    public partial class ServiceForm : Form
    {
        SqlConnection connection = new SqlConnection(Utils.conn);
        DataTable table;
        int id;
        public ServiceForm()
        {
            InitializeComponent();
            loadgrid();
            loadcategory();
            loadunit();

            lbltime.Text = DateTime.Now.ToString("dddd, dd-MM-yyyy HH:mm:ss");
            lblname.Text = Model.name;
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
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from service join unit on service.id_unit = unit.id_unit join category on service.id_category = category.id_category", connection);
            table = new DataTable();
            adapter.Fill(table);

            dataGridView1.DataSource = table;
        }

        bool val()
        {
            if (textBox2.TextLength < 1 || textBox3.TextLength < 1 || comboBox1.Text.Length < 1 || comboBox2.Text.Length < 1 || numericUpDown1.Value < 1)
            {
                MessageBox.Show("All Fields Must be Filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        void loadcategory()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from category", connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            comboBox1.DataSource = data;
            comboBox1.DisplayMember = "name_category";
            comboBox1.ValueMember = "id_category";
        }

        void loadunit()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from unit", connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            comboBox2.DataSource = data;
            comboBox2.DisplayMember = "name_unit";
            comboBox2.ValueMember = "id_unit";
        }

        void clear()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            numericUpDown1.Value = 0;
        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from service join unit on service.id_unit = unit.id_unit join category on service.id_category = category.id_category where name_service like '%" + textBox1.Text + "%'", connection);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (val())
            {
                string com = "insert into service values(" + Convert.ToInt32(comboBox1.SelectedValue) + ", " + Convert.ToInt32(comboBox2.SelectedValue) + ", '" + textBox2.Text + "', " + Convert.ToInt32(textBox3.Text) + ", " + numericUpDown1.Value + ")";
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[2].Value);
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
            comboBox2.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            numericUpDown1.Value = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[5].Value);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if(id != 0)
            {
                if (val())
                {
                    string com = "update service set id_category = " + Convert.ToInt32(comboBox1.SelectedValue) + ", id_unit = " + Convert.ToInt32(comboBox2.SelectedValue) + ", name_service = '" + textBox2.Text + "', price_unit_service = " + Convert.ToInt32(textBox3.Text) + ", estimation_duration_service = " + Convert.ToInt32(numericUpDown1.Value) + " where id_service = " + id;
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
                else
                {
                    MessageBox.Show("Please select an Item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if(id != 0)
            {
                DialogResult result = MessageBox.Show("Are you sure to delete " + dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if(result == DialogResult.Yes)
                {
                    string com = "delete from service where id_service = " + id;
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
            else
            {
                MessageBox.Show("Please Select an Item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}
