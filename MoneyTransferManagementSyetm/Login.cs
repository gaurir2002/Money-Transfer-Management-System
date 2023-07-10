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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\User\Documents\Money Transfer Mngment System.mdf;Integrated Security=True;Connect Timeout=30");
        public static string UserName = "",UserCity="";
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || PasswordTb.Text == "" || CityCb.SelectedIndex ==-1)
            {
                MBox Obj = new MBox();
                Obj.Alert("Enter Both UserName and Password");
            }
            else 
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(@"select count(*) from AgentTbl where AName='" + UnameTb.Text + "' and APass='" + PasswordTb.Text + "'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    UserName = UnameTb.Text;
                    UserCity = CityCb.SelectedItem.ToString();
                    Transactions Obj = new Transactions();
                    Obj.Show();
                    this.Hide();
                    Con.Close();
                }
                else 
                {
                    MessageBox.Show("Wrong UserName or Password");
                }              
                Con.Close();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            AdminLogin Obj = new AdminLogin();
            Obj.Show();
            this.Hide();
        }
    }
}
