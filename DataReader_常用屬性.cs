DataReader 常用屬性

//===== Depth_深度 ==========================================================================
* 是一個取得值，表示目前"列(Row,紀錄)"的巢狀深度。
* 型別為 System.Int32(整數)代表目前資料列(Row，紀錄)的巢狀深度。最外面的資料表深度為零。
* .NET Framework Data Provider for SQL Server不支援巢狀，永遠傳回"零"。


using System.Web.Configuration;
using System.Data.SqlClient;

namespace test_DataReader
{
    public partial class DataReader_Depth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection Conn = new SqlConnection(WebConfigurationManager
                .ConnectionStrings["testConnectionString"].ConnectionString);
            Conn.Open();  //---- 連接 DB -----

            SqlCommand cmd = new SqlCommand("select id, test_time, summary, author from test", Conn);

            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();
            Response.Write("SqldataReader的 .Depth屬性--" + dr.Depth);
            //--- SqlDataReader的 .Depth屬性--目前資料列的巢狀深度。----------
            
            cmd.Cancel();
            dr.Close();
            Conn.Close();
        }
    }
}

//===== FieldCount 屬性 ==========================================================================
using System.Web.Configuration;
using System.Data.SqlClient;

namespace test_DataReader
{
    public partial class DataReader_Depth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection Conn = new SqlConnection(WebConfigurationManager
                .ConnectionStrings["存在Web.Config檔案裡面的 DB 連接字串"].ConnectionString);
            Conn.Open();  //---- 連接 DB -----

            SqlCommand cmd = new SqlCommand("select id, test_time, summary, author from test", Conn);

            SqlDataReader dr = cmd.ExecuteReader();

            dr.Read();
            Label1.Text = "test資料表，共有幾個欄位?......" + dr.FieldCount.ToString();
            //------ SqlDataReader的 .FieldCount 屬性--目前一筆紀錄的資料行(欄位、Column)數。 ----
            
            cmd.Cancel();
            dr.Close();
            Conn.Close();
        }
    }
}








