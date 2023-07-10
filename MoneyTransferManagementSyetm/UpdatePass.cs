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
    public partial class UpdatePass : Form
    {
        public UpdatePass()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Agents obj = new Agents();
            obj.Show();
            this.Hide();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\User\Documents\Money Transfer Mngment System.mdf;Integrated Security=True;Connect Timeout=30");

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (PasswordTb.Text == "")
            {
                MBox Obj = new MBox();
                Obj.Alert("Enter New Password");
            }
            else if (PasswordTb.Text == "Password1")
            {
                Agents obj = new Agents();
                obj.Show();
                this.Hide();
                {
                    try
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("update AdminTbl set Adpass=@Ap where AdId=@Admkey", Con);
                        cmd.Parameters.AddWithValue("@Ap", PasswordTb.Text);

                        cmd.Parameters.AddWithValue("@Admkey", 1);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Password Updated Successfully");
                        Con.Close();
                        AdminLogin Obj = new AdminLogin();
                        obj.Show();
                        this.Hide();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Password Updated Successfully");
                    }
                }
            }

        }

        private void PasswordTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void UpdatePass_Load(object sender, EventArgs e)
        {

        }
    }
}
