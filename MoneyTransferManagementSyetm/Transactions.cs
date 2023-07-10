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
    public partial class Transactions : Form
    {
        public Transactions()
        {
            InitializeComponent();
            CityLbl.Text = Login.UserCity;
            Sname = Login.UserName;
            DisplaySent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\User\Documents\Money Transfer Mngment System.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplaySent()
        {
            Con.Open();
            string Query = "Select * from SendTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SendDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Search()
        {
            
            
            if (SearchTb.Text == "")
            {
                MessageBox.Show("Enter a Sender Name");
            }
            else 
            {
                Con.Open();
                string Query = "Select * from SendTbl where SenderName ='" + SearchTb.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                SendDGV.DataSource = ds.Tables[0];
                Con.Close();
            }
        }
        string Sname;
        public static string UserCity = "";
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DisplaySent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 650, 450);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            if (SCodeTb.Text == "" || SNameTb.Text == "" || RecTb.Text == "" || AmtTb.Text == "")
            {
                MBox Obj = new MBox();
                Obj.Alert("Missing Information");
            }
            else
            {
                try
                {
                    int Total;
                    Double Rate = Convert.ToDouble(RateTb.Text) / 100;
                    Double Fees = Convert.ToInt32(AmtTb.Text) * Rate;
                    Total = Convert.ToInt32(AmtTb.Text) + Convert.ToInt32(Fees);
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into SendTbl(SCode,SenderName,ReceiverName,SAmt,STotal,SDate,RCity,SCity,Collected) values(@SC,@SN,@RN,@SA,@ST,@SD,@RC,@SCi,@SCo)", Con);
                    cmd.Parameters.AddWithValue("@SC", SCodeTb.Text);
                    cmd.Parameters.AddWithValue("@SN", SNameTb.Text);
                    cmd.Parameters.AddWithValue("@RN", RecTb.Text);
                    cmd.Parameters.AddWithValue("@SA", AmtTb.Text);
                    cmd.Parameters.AddWithValue("@ST", Total);
                    cmd.Parameters.AddWithValue("@SD", SDate.Value.Date);
                    cmd.Parameters.AddWithValue("@RC", CityCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SCi", CityLbl.Text);
                    cmd.Parameters.AddWithValue("@SCo", "No");
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Money Sent");
                    Con.Close();
                    DisplaySent();
                    //Reset();
                }
                catch (Exception)
                {
                    MessageBox.Show("Money Sent");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Search();
            SearchTb.Text = "";   
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("MoneyTransferManagementSyetm", new Font("Averia", 14, FontStyle.Bold), Brushes.Red, new Point(240, 20));
            e.Graphics.DrawString("Send And Receive Your Money All Over India", new Font("Averia", 8, FontStyle.Italic), Brushes.DarkViolet, new Point(200, 40));
            e.Graphics.DrawString("______________________________________________________________________________________________________________________", new Font("Averia", 10, FontStyle.Bold), Brushes.Black, new Point(0, 50));

            e.Graphics.DrawString("Sender Name", new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(26, 80));
            e.Graphics.DrawString(SNameTb.Text, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, 100));
            e.Graphics.DrawString("Receiver Name", new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(450, 80));
            e.Graphics.DrawString(RecTb.Text, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(450, 100));
            e.Graphics.DrawString("Sender Address", new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(26, 140));
            e.Graphics.DrawString(CityLbl.Text, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, 160));
            e.Graphics.DrawString("Receiver Address", new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(450, 140));
            e.Graphics.DrawString(CityCb.SelectedItem.ToString(), new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(450, 160));
            e.Graphics.DrawString("Sending Date", new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(26, 200));
            e.Graphics.DrawString(SDate.Value.Date.ToString(), new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, 220));
            e.Graphics.DrawString("Amount Sent", new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(450, 200));
            e.Graphics.DrawString("Rs "+AmtTb.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(450, 220));
            e.Graphics.DrawString("Secret Code", new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(265, 270));
            e.Graphics.DrawString(SCodeTb.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(260, 280));
            e.Graphics.DrawString("_______________________________________________________________________________________________",new Font("Averia", 10,FontStyle.Bold), Brushes.Black, new Point(0,300));
            e.Graphics.DrawString("Note: The Serect has to be kept secret and it should be given to the receiver only.", new Font("Century Gothic", 8, FontStyle.Regular), Brushes.Black, new Point(10, 320));
            e.Graphics.DrawString("Customer Signature:", new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(10, 345));
            e.Graphics.DrawString("Agent Signature:", new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Black, new Point(450, 345));
            e.Graphics.DrawString("Money Transfer", new Font("Century Gothic", 8, FontStyle.Italic), Brushes.Red, new Point(250, 430));

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            UserCity = CityLbl.Text;
            Receives Obj = new Receives();
            Obj.Show();
            this.Hide();
        }

        private void Transactions_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Agents Obj = new Agents();
            Obj.Show();
            this.Hide();
        }
        private void Reset()
        {
            SCodeTb.Text = "";
            SNameTb.Text = "";
            RecTb.Text = "";
            AmtTb.Text = "";
            SDate.Text = "";
            CityCb.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
