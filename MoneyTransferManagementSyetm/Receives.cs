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
    public partial class Receives : Form
    {
        public Receives()
        {
            InitializeComponent();
            DisplayRec();
            CityLbl.Text = Transactions.UserCity;
        }
       
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\User\Documents\Money Transfer Mngment System.mdf;Integrated Security=True;Connect Timeout=30");
        int Amt;
        private void CheckCode()
        {
            if (SCodeTb.Text == "")
            {
                MessageBox.Show("Enter The Code");
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(@"select count(*) from SendTbl where SCode='" + SCodeTb.Text + "' and Collected='" + "No" + "' and RCity='" + CityLbl.Text + "'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    try
                    {
                       // Con.Open();
                        string query = "select * from SendTbl where SCode='" + SCodeTb.Text + "'";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        DataTable dt1 = new DataTable();
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmd);
                        sda1.Fill(dt1);
                        foreach (DataRow dr in dt1.Rows)
                        {
                            SNameLbl.Text = dr["SenderName"].ToString();
                            RNameLbl.Text = dr["ReceiverName"].ToString();
                            Amt = Convert.ToInt32(dr["SAmt"].ToString());
                            AmtLbl.Text = "RS:" + dr["SAmt"].ToString();
                            SentDateLbl.Text = dr["SDate"].ToString();
                            CityLbl.Text = dr["RCity"].ToString();
                            SCityLbl.Text = dr["SCity"].ToString();
                        }
                        Con.Close();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
                else 
                {
                    MessageBox.Show("No Transaction for this Code");
                    Con.Close();
                }
            }
        }
                    
        private void Receives_Load(object sender, EventArgs e)
        {
            TodayLbl.Text = TodayDate.Value.Date.ToString();
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
        private void DisplayRec()
        {
            Con.Open();
            string Query = "Select * from ReceiveTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ReceiveDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CheckCode();
        }
        private void Reset()
        {
            SNameLbl.Text = "";
            RNameLbl.Text = "";
            AmtLbl.Text = "";
            SentDateLbl.Text = "";
            SCodeTb.Text = "";
            SCityLbl.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void UpdateSend()
        {
           
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update SendTbl set Collected=@Ac where SCode=@Skey", Con);
                    cmd.Parameters.AddWithValue("@Ac", "Yes");
                    
                    cmd.Parameters.AddWithValue("@Skey", SCodeTb.Text);
                    cmd.ExecuteNonQuery();
                   // MessageBox.Show("Agent Edit Successfully");
                    Con.Close();
                   // DisplayAgents();
                    Reset();
                }
                catch (Exception)
                {
                    MessageBox.Show("Agent Edit Successfully");
                }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (SCodeTb.Text == "" || RNameLbl.Text == "")
            {
                MBox Obj = new MBox();
                Obj.Alert("Missing Information");
            }
            else
            {
                try
                {

                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into ReceiveTbl(SCode,SName,RName,STotal,SDate,RDate,RCity,SCity) values(@SC,@SN,@RN,@ST,@SD,@RD,@RC,@SCi)", Con);
                    cmd.Parameters.AddWithValue("@SC", SCodeTb.Text);
                    cmd.Parameters.AddWithValue("@SN", SNameLbl.Text);
                    cmd.Parameters.AddWithValue("@RN", RNameLbl.Text);
                    cmd.Parameters.AddWithValue("@ST", Amt);
                    cmd.Parameters.AddWithValue("@SD", SentDateLbl.Text);
                    cmd.Parameters.AddWithValue("@RD", TodayLbl.Text);
                    cmd.Parameters.AddWithValue("@RC", CityLbl.Text);
                    cmd.Parameters.AddWithValue("@SCi", SCityLbl.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Money Received");
                    Con.Close();
                    DisplayRec();
                     UpdateSend();
                    //Reset();
                }
                catch (Exception)
                {
                    MessageBox.Show("Money Received");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Transactions Obj = new Transactions();
            Obj.Show();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {

        }

        private void btnClose_Click_2(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
