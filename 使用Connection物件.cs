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

5.
