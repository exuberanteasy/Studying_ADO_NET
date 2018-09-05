如何使用 DataReader 物件讀取資料表紀錄

using System.Data.SqlClient;

namespace DataReaderDemo1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection())  //當離開using時，cn物件會自動釋放資料庫連接資源
            {
                cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                                      "AttachDbFilename=|DataDirectory|ch16DB.mdf;" +
                                      "Integrated Security=True";
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM 成績單", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    textBox1.Text += Environment.NewLine + 
                                     Environment.NewLine;
                }
                while(dr.Read())
                {
                    for(int i = 0; i<dr.FieldCount; i++)
                    {
                        textBox1.Text += dr[i].ToString() + "\t";
                    }
                    textBox1.Text += Environment.NewLine;
                }
            }
        }
    }
}

//==========================================================================================

while (dr.Read())
{
    textBox1.Text += dr["學號"].ToString() + "\t";
    textBox1.Text += dr["姓名"].ToString() + "\t";
    textBox1.Text += dr["中文"].ToString() + "\t";
    textBox1.Text += dr["數學"].ToString() + "\t";
    textBox1.Text += Environment.NewLine;
}

//=========================================================================================
while (dr.Read())
{
    for (int i =0; i<dr.FieldCount; i++)
    {
        textBox1.Text += dr.GetValue(i).ToString() + "\t";
    }
    textBox1.Text += Environment.NewLine;
}





