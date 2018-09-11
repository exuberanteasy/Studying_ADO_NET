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

//===== FieldCount 屬性 ==================================================================================
* FieldCount 屬性是用來取得目前資料"列(Row，紀錄)"中的「資料行(Column，欄位)」數目。
* 此資料型別為 Sysytem.Int32(整數)。當找不到有效的資料錄集(Recordset)時則為0，否則為目前資料列(這一筆紀錄)中的資料行(欄位)數目。
  預設值為 -1。


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

//==== FieldCount_列出資料表的欄位數(資料行數目) =============================================
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace test_DataReader
{
    public partial class DataReader_Depth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection Conn = new SqlConnection(WebConfigurationManager
                .ConnectionStrings["存在Web.Config檔案裡面的 DB 連接字串"].ConnectionString);
            SqlDataReader dr = null;
            SqlCommand cmd = new SqlCommand("select id, test_time, summary, author from test", Conn);

            try
            {
                //以下程式，只放「執行期間」的指令!
                Conn.Open();  //---- 連接 DB -----
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    //每次讀取一列資料，直到完畢為止(EOF)。
                    for (int i = 0; i <= (dr.FieldCount -1); i++)
                    {
                        //--- SqlDataReader的 .FieldCount屬性--目前一筆紀錄的資料行(欄位、Column)數。-----
                        Label1.Text += dr[i].ToString() +  "<br />";
                    }
                    Label1.Text += "<hr />";
                }
            }
            catch (Exception ex)
            {
                //---如果有錯誤或是例外狀況，將執行這一段-----
                Response.Write("<b>Error Message---- </b>" + ex.ToString() + "<HR/>");
            }
            finally
            {
                if (dr != null)
                {
                    cmd.Cancel();
                    dr.Close();
                }
                if (Conn.State == System.Data.ConnectionState.Open)
                {
                    Conn.Close();
                    Conn.Dispose();
                }
            }
        }
    }
}








