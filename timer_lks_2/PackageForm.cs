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
    public partial class PackageForm : Form
    {
        int id_package;
        int id_detail;
        public PackageForm()
        {
            InitializeComponent();
            loadpackage();
            loadserv();

            panelup.Visible = false;
            btnsv.Visible = false;
            btncanc.Visible = false;
            panelins.Visible = false;
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

        void loadpackage()
        {
            string com = "select * from package";
            dataGridView1.DataSource = Command.getData(com);
        }

        void dgv2()
        {
            string com = "select * from detail_package join service on detail_package.id_Service = service.id_Service where id_package = " + id_package;
            dataGridView2.DataSource = Command.getData(com);
        }

        void loadserv()
        {
            string com = "select * from service";
            comboBox1.DataSource = Command.getData(com);
            comboBox1.DisplayMember = "name_service";
            comboBox1.ValueMember = "id_Service";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string com = "select * from package where name_package like '%" + textBox1.Text + "%'";
            dataGridView1.DataSource = Command.getData(com);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            id_package = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

            dgv2();
        }

        private void btnup_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Select an item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                panelup.Visible = true;
                btnsv.Visible = true;
                btncanc.Visible = true;
                btnup.Visible = false;
                btndel.Visible = false;

                txtname.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txtxdesc.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }
        }

        private void btncanc_Click(object sender, EventArgs e)
        {
            panelup.Visible = false;
            btnsv.Visible = false;
            btncanc.Visible = false;
            id_package = 0;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.CurrentRow.Selected = true;
            id_detail = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[2].Value);
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if(dataGridView2.CurrentRow != null)
            {
                DialogResult result = MessageBox.Show("Are you sure to delete " + dataGridView2.SelectedRows[0].Cells[7].Value.ToString(), "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if(result == DialogResult.Yes)
                {
                    string com = "delete from detail_package where id_detail_package = " + id_detail;
                    try
                    {
                        Command.exec(com);
                        MessageBox.Show("Successfully deleted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgv2();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Select an item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Select an item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure to delete " + dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                    string com = "delete from detail_package where id_package = " + id_package;
                    string comm = "delete from package where id_package = " + id_package;
                    try
                    {
                        Command.exec(com);
                        Command.exec(comm);
                        MessageBox.Show("Successfully deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadpackage();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(""+ex, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }
            }
        }

        private void btnsv_Click(object sender, EventArgs e)
        {
            if(txtname.TextLength < 1 || txtxdesc.TextLength < 1)
            {
                MessageBox.Show("Text field must be filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string com = "update package set name_package = '" + txtname.Text + "', description_package = '" + txtxdesc.Text + "' where id_package =" + id_package;
                try
                {
                    Command.exec(com);
                    MessageBox.Show("Successfully Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadpackage();
                    panelup.Visible = false;
                    btnsv.Visible = false;
                    btncanc.Visible = false;
                    id_package = 0;
                }
                catch (Exception ex)
                {

                    MessageBox.Show("" + ex, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            panelup.Visible = true;
            panelins.Visible = true;
            btnsv.Visible = true;
            btncanc.Visible = true;
            btnup.Visible = false;
            btndel.Visible = false;

            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows != null)
            {
                string com = "insert into detail_package values(" + Convert.ToInt32(comboBox1.SelectedValue) + ", " + id_package + ", " + Convert.ToInt32(numericUpDown1.Value) + ")";
                try
                {
                    Command.exec(com);
                    MessageBox.Show("Successfully inserted", "infomartion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(""+ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string com = "insert into package values ('" + txtname.Text + "', " + Convert.ToInt32(txtprice.Text) + ", '" + txtxdesc.Text + "', " + Convert.ToInt32(numericUpDown2.Value) + ")";
            try
            {
                Command.exec(com);
                MessageBox.Show("Successfully inserted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panelup.Visible = false;
                btnsv.Visible = false;
                btncanc.Visible = false;
                panelins.Visible = false;
                loadpackage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
