如何使用 Connection物件
連接資料庫

using System.Data.OleDb;

string cnStr = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = 資料庫真實路徑";

//宣告 cn 為 OleDbConnection資料庫連接物件。
OleDbConnection cn;

//建立 OleDbConnection資料庫連接物件
//建立cn為OleDbConnection物件並指定cnStr為資料庫的連接字串。
cn = new OleDbConnection(cnStr);

//使用 Open(); 開啟與資料庫的連接。
cn.Open();

//完成資料庫存取後再使用Close方法關閉與資料庫的連接
cn.Close();

* 若 uid 與 pwd 都不加，可改用「Integrated Security=True」，表示使用目前登入系統的 Windows帳號來連接 SQL Server。

string cnStr = "Server=Server1; database=Northwind; uid=sa; pwd=1234;";


cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + 
                       "AttachDbFilename=|DataDirectory|Northwind.mdf;" +
                       "Integrated Security=True";
