using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timer_lks_2
{
    public partial class ViewTransaction : Form
    {
        int id_header, id_detail;
        public ViewTransaction()
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
            string com = "select header_transaction.*, customer.name_customer, employee.name_employee from header_transaction join customer on header_transaction.id_customer = customer.id_customer join employee on header_transaction.id_employee = employee.id_employee";
            dataGridView1.DataSource = Command.getData(com);
        }

        void loadgrid2()
        {
            string com = "select * from detail_transaction where id_header_transaction = " + id_header;
            dataGridView2.DataSource = Command.getData(com);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string com = "select header_transaction.*, customer.name_customer, employee.name_employee from header_transaction join customer on header_transaction.id_customer = customer.id_customer join employee on header_transaction.id_employee = employee.id_employee where name_customer like '%" + textBox1.Text + "%'";
            dataGridView1.DataSource = Command.getData(com);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            id_header = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[2].Value);
            loadgrid2();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if(dataGridView2.CurrentRow.Selected != null)
            {
                string com = "update detail_transaction set complete_datetime_detail_transaction = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where id_detail_transaction = " + id_detail;
                try
                {
                    Command.exec(com);
                    MessageBox.Show("Success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                loadgrid2();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.CurrentRow.Selected = true;
            id_detail = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[2].Value);
        }
    }
}
