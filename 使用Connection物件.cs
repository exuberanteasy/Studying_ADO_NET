如何使用Connection物件

用來與資料來源之間建立連接

Case 01
引用 System.Data.OleDb命名空間(適用 Access & Excel 2003等以上版本)

1. using System.Data.OleDb;  //引用命名空間

2. 建立連接字串
宣告名稱為cnStr的字串變數，用來存放資料庫的連線字串，]並指定資料庫所在的真實路徑。
string cnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=資料庫真實路徑";

3. OleDbConnection cn;  //宣告 cn 為OleDbConnection資料庫連接物件

4. 建立 OleDbConnection 資料庫連接物件
cn = new OleDbConnection(cnStr);  //建立 cn 為 OleDbConnection物件 並指定 cnStr 為資料庫的連接字串。

5. cn.Open();  //使用 Open 方法開啟與資料酷的連接


6. cn.Close();  //完成資料庫存取後再使用 Close方法 關閉與資料庫連接
//======================================================================
//連接資料庫 範例
using System.Data.SqlClient;

namespace ConnectionDemo1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection();  //SqlConnection物件 cn

        //定義 ShowConnection方法 用來在 txtShow上 顯示資料來源的相關資訊
        private void ShowConnection()
        {
            txtShow.Text = "連接字串: " + cn.ConnectionString + Environment.NewLine;
            txtShow.Text += "逾時秒數: " + cn.ConnectionTimeout + Environment.NewLine;
            txtShow.Text += "資料庫: " + cn.Database + Environment.NewLine;
            txtShow.Text += "資料來源: " + cn.DataSource + Environment.NewLine;
        }

        //表單載入時執行此事件
        private void Form1_Load(object sender, EventArgs e)
        {
            // 設定連接字串，用來連接 Northwind.mdf 資料庫
            cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                                  "AttachDbFilename=|DataDirectory|Northwind.mdf;" +
                                  "Integrated Security=True";
                      //??? 這段有問題
                                  
            ShowConnection(); //呼叫ShowConnection方法
        }

        private void btnCnState_Click(object sender, EventArgs e)
        {
            //判斷資料庫有沒有開
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
                btnCnState.Text = "關閉";
                txtShow.Text += "目前狀態:資料庫已連接" + Environment.NewLine;
            }
            //判斷目前是否是開啟狀態
            else if (cn.State == ConnectionState.Open)
            {
                cn.Close();
                btnCnState.Text = "開啟";
                txtShow.Text += "目前狀態:資料已關閉" + Environment.NewLine;
            }
            ShowConnection();
        }        
    }
}
