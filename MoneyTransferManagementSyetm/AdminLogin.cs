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

namespace MoneyTransferManagementSyetm
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {

        }

        private void AdminLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\User\Documents\Money Transfer Mngment System.mdf;Integrated Security=True;Connect Timeout=30");

        private void LoginBtn_Click_1(object sender, EventArgs e)
        {
            if (PasswordTb.Text == "")
            {
                MBox Obj = new MBox();
                Obj.Alert("Enter The Password");
            }
                
            else if (PasswordTb.Text == "Password")
            {
                Agents obj = new Agents();
                obj.Show();
                this.Hide();
            }
        }

        private void PasswordTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
