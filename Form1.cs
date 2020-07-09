using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EK_Home_POS.myCLass;

namespace EK_Home_POS
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

         private void btnLogin_Click(object sender, EventArgs e)
        {
            string uname = txtUsername.Text;
            string pw = txtPassword.Text;
            var mc = new MyClass();


            if (mc.CheckUser(uname, pw))
            {
                this.Hide();
                txtUsername.Text = txtPassword.Text = "";
                frmMain m = new frmMain(uname);
                m.Show();
            }
            else
            {
                MessageBox.Show("Check your username and password!");
            }
        }
    }
}
