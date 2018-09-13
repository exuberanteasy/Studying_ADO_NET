Connection 物件常用成員

using System.Data.SqlClient;


namespace test_connection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection();  //SqlConnection 物件 cn

        // 定義ShowConnection 方法用來在 txtShow 上顯示資料來源的相關資訊
        private void ShowConnection()
        {
            textBox1.Text = "連接字串: " + cn.ConnectionString + Environment.NewLine;
            textBox1.Text += "逾時秒數: " + cn.ConnectionTimeout + Environment.NewLine;
            textBox1.Text += "資料庫: " + cn.Database + Environment.NewLine;
            textBox1.Text += "資料來源: " + cn.DataSource + Environment.NewLine;
        }

        //表單載入時執行此事件
        private void Form1_Load(object sender, EventArgs e)
        {
            //設定連接字串，用來連接Northwind.mdf資料庫
            cn.ConnectionString = @"Data Source(LocalDB)\MSSQLLocalDB;" +
                                   "AttachDbFilename=|DataDirectory|Northwind.mdf;" +
                                   "Integrated Security=True";
            ShowConnection();
        }

        //按下 開啟 執行此事件
        private void button1_Click(object sender, EventArgs e)
        {
            //判斷目前是否為資料庫關閉連接狀態
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
                button1.Text = "關閉";
                textBox1.Text += "目前狀態:資料庫已連接" + Environment.NewLine;
            }
            //判斷目前是否為資料庫開啟連接狀態
            else if (cn.State == ConnectionState.Open)
            {
                cn.Close(); 
                button1.Text = "開啟";
                textBox1.Text += "目前狀態:資料庫已關閉" + Environment.NewLine;
            }
            ShowConnection();
        }
    }
}
