//如何使用 DataReader物件

//如何建立 DataReader物件

Case 01:
1. using System.Data.SqlClient;

2. SqlConnection cn = new SqlConnction("連接字串");

3. SqlCommand cmd;
   Sqlreader dr;
   
4. cmd = new SqlCommand("SQL命令或預存程序名稱", cn);

5. cn.Open();
   dr = cmd.ExecuteReader();  //使用

6. cn.Close();

Case 02:
using System.Data.OleDb;
OleDbConnection cn = new OleDbConnection("連接字串");
OleDbCommand cmd;
OleDbDataReader dr;
cmd = new OleDbCommand("SQL命令或預存程序名稱", cn);
cn.Open();
dr = cmd.ExecuteReader();
cn.Close();


//DataReader物件常用成員

//如何使用 DataReader物件讀取資料表紀錄

//








   
   
   把書上只要是程式碼的部分 都盡量打看看
