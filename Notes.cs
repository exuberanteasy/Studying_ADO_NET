Note => Go
Noting

第一、連接資料庫(Connection)
{
    "server=資料庫主機名稱與IP位址; uid=帳號; pwd=密碼; database=資料庫名稱"
    以精靈步驟自動產生比較好
}
第二、執行SQL指令存取資料(又成分兩大類:取出資料，或是寫入資料)
{
    分兩種
    (1)一種是「Select」陳述句，用來撈出資料，就是資料的查詢與輸出。
    (2)另一種是資料的異動 => Insert、Update、Delete。
}
第三、自由發揮(通常這一段是畫面或流程的設計，或是直接交由控制項來呈現，如DataBinding)。
{
    呈現的好看一點
}
第四、關閉資源(如:關閉資料庫的連接)。
{
    (1)關閉資料庫的連接
    (2)釋放資源
}
//===================================================================================

ConnectionString_資料庫的連接字串
"Persist Security Info=false; Integrated Security=true;
Initial Catalog = SQLServer裡面的資料庫名稱，例如Northwind;server=(local)"
------------------------------------------------------------------
server=(local)，也可以寫成 server=.。 兩者都是代表本機的意思。

"Data Source=.\MSSQLSERVER2008; Initial Catalog=SQLServer裡面的資料庫名稱，例如Northwind;
Integrated Security=True;"
    
※ 註解(1):如果是連接SQL Server Express 版，請寫成"data source=.\SQLEXPRESS ...後續字串同上"。
※


Web.Config
