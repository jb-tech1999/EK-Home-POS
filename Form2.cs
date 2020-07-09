using EK_Home_POS.myCLass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EK_Home_POS
{
    public partial class frmMain : Form
    {
        //gets the username from the login form
        public frmMain(string username)
        {
            InitializeComponent();
            lblNaam.Text = "Welcome " + username;
        }
        MyClass mc = new MyClass();
        public int ID;
        public int updatedQuantity;
        public int quantity;

        //hides all panels not being used
        public void HidePanel()
        {
            if (pnlStock.Visible == true)
                pnlStock.Visible = false;
            if (pnlIngredients.Visible == true)
                pnlIngredients.Visible = false;
            if (pnlPackaging.Visible == true)
                pnlPackaging.Visible = false;
            if (pnlUsers.Visible == true)
                pnlUsers.Visible = false;
        }

        //clears the textboxes when called
        public void ClearText()
        {
            txtPassword.Text = txtUname.Text = txtDescription.Text = txtKode.Text = txtProduct.Text = txtQuantity.Text = txtDescriptionPack.Text = txtProductPack.Text = txtQuantityPack.Text = "";
        }

    
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin log = new frmLogin();
            log.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            HidePanel();
            pnlStock.Visible = true;
            dataGridView1.DataSource = mc.ShowUsers();
            dgvStock.DataSource = mc.ShowStock();
            dgvPackaging.DataSource = mc.ShowPackaging();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            HidePanel();
            pnlStock.Visible = true;
        }

        private void btnIngredients_Click(object sender, EventArgs e)
        {

            HidePanel();
            pnlIngredients.Visible = true;
        }

        private void btnPackaging_Click(object sender, EventArgs e)
        {
            HidePanel();
            pnlPackaging.Visible = true;
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            HidePanel();
            pnlUsers.Visible = true;
        }

        //adds a username and password to the database
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string uname = txtUname.Text;
            string pw = txtPassword.Text;
            if (mc.AddUser(uname, pw))
            {
                dataGridView1.DataSource = mc.ShowUsers();
                ClearText();
            }
            else
            {
                MessageBox.Show("Try again!");
            }
        }

        //Deletes a user in the database
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            string uname = txtUname.Text;
            if (mc.DeleteUser(uname))
            {
                MessageBox.Show("User has been removed");
                dataGridView1.DataSource = mc.ShowUsers();
                ClearText();
            }
            else
            {
                ClearText();
                MessageBox.Show("User could not be removed!");
            }
        }

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            //new stock is added to the database
            string product = txtProduct.Text;
            string description = txtDescription.Text;
            string kode = txtKode.Text;
            int quantity = int.Parse(txtQuantity.Text);

            if (mc.AddStock(product, description, kode, quantity))
            {
                ClearText();
                dgvStock.DataSource = mc.ShowStock();
                MessageBox.Show("Stock added!");
            }
            else
            {
                MessageBox.Show("Stock could not be added!");
                ClearText();
            }
        }

        private void dgvStock_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //data in datagridview is sent to the textboxes
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvStock.Rows[e.RowIndex];
                ID = int.Parse(row.Cells[0].Value.ToString());
                txtProduct.Text = row.Cells[1].Value.ToString();
                txtDescription.Text = row.Cells[2].Value.ToString();
                txtKode.Text = row.Cells[3].Value.ToString();
                quantity = int.Parse(row.Cells[4].Value.ToString());
            }
        }

        //Updates the quantity of stock
        private void btnUpdateUsers_Click(object sender, EventArgs e)
        {
            string product = txtProduct.Text;
            updatedQuantity = int.Parse(txtQuantity.Text) + quantity;
            if (mc.UpdateStock(ID, updatedQuantity))
            {
                ClearText();
                MessageBox.Show("Stock has been updated!");
                dgvStock.DataSource = mc.ShowStock();
                dgvStock.Update();
                dgvStock.Refresh();
            }
            else
            {
                ClearText();
                MessageBox.Show("Stock could not be updated");
            }
        }

        private void dgvPackaging_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //verander die name
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvStock.Rows[e.RowIndex];
                ID = int.Parse(row.Cells[0].Value.ToString());
                txtProduct.Text = row.Cells[1].Value.ToString();
                txtDescription.Text = row.Cells[2].Value.ToString();
                txtKode.Text = row.Cells[3].Value.ToString();
                quantity = int.Parse(row.Cells[4].Value.ToString());
            }
        }

        private void btnAddPack_Click(object sender, EventArgs e)
        {
            string product = txtProductPack.Text;
            string description = txtDescriptionPack.Text;
            int quantity = int.Parse(txtQuantityPack.Text);

            if (mc.AddPackaging(product, description, quantity))
            {
                ClearText();
                dgvPackaging.DataSource = mc.ShowPackaging();
                MessageBox.Show("Packaging added!");
            }
            else
            {
                MessageBox.Show("Packaging could not be added!");
                ClearText();
            }
        }
    }
}
