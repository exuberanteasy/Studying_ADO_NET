如何使用 using 與 Connection 物件連接資料庫

using System.Data.SqlClient;

namespace ConnectionDemo_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection() ) //建立 cn 為 SqlConnection物件
            {
                cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                                      "AttachDbFilename=|DataDirectory|Northwind.mdf;" +
                                      "Integrated Security=True";
                // 處理資料庫的敘述
                cn.Open();
                // 當離開 using 區塊時，馬上執行 Dispose()方法 釋放 Connection物件的 所佔用的資源
                if (cn.State == ConnectionState.Open)
                {
                    MessageBox.Show("資料庫已連接", "目前狀態");
                }
            }
        }
    }
}
