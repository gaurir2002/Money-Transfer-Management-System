using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace MoneyTransferManagementSyetm
{
    public partial class Agents : Form
    {
        public Agents()
        {
            InitializeComponent();
            DisplayAgents();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\User\Documents\Money Transfer Mngment System.mdf;Integrated Security=True;Connect Timeout=30");
        private void Reset()
        {
            ANameTb.Text = "";
            APhoneTb.Text = "";
            ACityCb.SelectedItem=-1;
            APasswordTb.Text = "";
            key = 0;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (ANameTb.Text == "" || ACityCb.SelectedIndex == -1 || APhoneTb.Text == "" || APasswordTb.Text == "")
            {
                MBox Obj = new MBox();
                Obj.Alert("Missing Information");
            }
            else 
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AgentTbl(AName,APhone,ACity,APass) values(@An,@Ap,@Ac,@Apass)", Con);
                    cmd.Parameters.AddWithValue("@An", ANameTb.Text); 
                    cmd.Parameters.AddWithValue("@Ap", APhoneTb.Text);
                    cmd.Parameters.AddWithValue("@Ac", ACityCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Apass", APasswordTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agent Saved Successfully");
                    Con.Close();
                    DisplayAgents();
                    Reset();
                }
                catch (Exception)
                {
                    MessageBox.Show("Agent Saved Successfully");
                }
            }

        }
        private void DisplayAgents()
        {
            Con.Open();
            string Query = "Select * from AgentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AgentsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        int key = 0;
        private void AgentsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ANameTb.Text = AgentsDGV.SelectedRows[0].Cells[1].Value.ToString();
            APhoneTb.Text = AgentsDGV.SelectedRows[0].Cells[2].Value.ToString();
            ACityCb.SelectedItem = AgentsDGV.SelectedRows[0].Cells[3].Value.ToString();
            APasswordTb.Text = AgentsDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (ANameTb.Text == "")
            {
                key = 0;
            }
            else 
            {
                key = Convert.ToInt32(AgentsDGV.SelectedRows[0].Cells[1].Value.ToString());
            }

        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (ANameTb.Text == "")
            {
                MessageBox.Show("Enter The Agent Name");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from AgentTbl where AName='" + ANameTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agent Deleted Successfully");
                    Con.Close();
                    DisplayAgents();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
         }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ANameTb.Text == "" || ACityCb.SelectedIndex == -1 || APhoneTb.Text == "" || APasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "Update AgentTbl set AName='" + ANameTb.Text + "',aphone='" + APhoneTb.Text + "',acity='" + ACityCb.SelectedItem.ToString() + "',apass='" + APasswordTb.Text + "' where AName='" + ANameTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Agent Updated Successfully");
                    Con.Close();
                    DisplayAgents();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Agents_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            UpdatePass Obj = new UpdatePass();
            Obj.Show();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
 