//如何使用 DataReader物件


//如何建立 DataReader物件
1. using System.Data.SqlClient;

2. SqlConnection cn = new SqlConnction("連接字串");

3. SqlCommand cmd;
   Sqlreader dr;
   
4. cmd = new SqlCommand("SQL命令或預存程序名稱", cn);

5. cn.Open();
   dr = cmd.ExecuteReader();  //使用
   
   
   把書上只要是程式碼的部分 都打看看
