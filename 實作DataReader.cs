入門練習(一): 實作 DataReader 與範例

using System.Data;
using System.Data.SqlClient;   // 搭配MS SQL Server 資料庫

namespace GoDataReader
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // SqlConnection Conn = new SqlConnection("資料庫連接字串");
            SqlConnection Conn = new SqlConnection("Data Source =.; Initial Catalog = Northwind; Integrated Security = True");
            
            Conn.Open();  //-- (1), 連接 DB ---------------------
            SqlCommand cmd = new SqlCommand("select * from customers", Conn);
            SqlDataReader dr = cmd.ExecuteReader();
            //----(2). 執行SQL指令，取出資料 ------------------------

            GridView1.DataSource = dr;   // ---- (3).資料連接(資料綁定)------
            GridView1.DataBind();

            cmd.Cancel();   // ---- (4).釋放資源，關閉資料庫的連接 ------
            dr.Close();
            Conn.Close();
        }
    }
}

