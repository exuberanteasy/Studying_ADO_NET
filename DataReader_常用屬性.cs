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

//===== HasRows ===============================
* 以我們常見的會員登入為例，當會員輸入帳號、密碼之後，我們該怎麼確認資料庫裡面是否真的有這名會員呢?
  HasRows屬性如果回應true，表示會員資料表裡面真有這筆紀錄，真的有這個帳號。

using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace test_DataReader
{
    public partial class DataReader_Depth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        //HasRows
        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["存放在Web.Config檔裡面的資料庫連接字串"].ConnectionString);
            Conn.Open();

            //下面這行這種寫法會受到攻擊
            //SqlCommand cmd = new SqlCommand("select * from db_user where name = '" + TextBox1.Text + "' and password = '" + TextBox2.Text + "'", Conn);

            //建議使用參數來做，以免受到攻擊。本書另有專文解說。
            SqlCommand cmd = new SqlCommand("select * from db_user wher name = @name and password =  @passwd", Conn);

            cmd.Parameters.Add("@name", SqlDbType.VarChar, 50);
            cmd.Parameters["@name"].Value = TextBox1.Text;

            cmd.Parameters.Add("@passwd", SqlDbType.VarChar, 256);
            cmd.Parameters["@passwd"].Value = TextBox2.Text;

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                Label1.Text = "Login Success! 登入成功!";

                cmd.Cancel();
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                    Conn.Dispose();
                }
                // Respose.Redirect("---URL---");
                //---通過帳號、密碼的檢查之後，導向到會員專屬的網頁(後台管理的網頁)。----
            }
            else
            {
                Label1.Text = "Bye~~~~~。<font color=red>登入失敗!!</font>";

                cmd.Cancel();
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                    Conn.Dispose();
                }
            }
        }
    }
}
//---補充說明: ------------------
* 關於SQL Injection(資料隱碼、數據注入) 攻擊與 XSS 攻擊。建議使用SqlCommand「參數」的寫法，便可以初步地防範一些攻擊。
//**** 重點!  使用參數，防範SQL Injection攻擊 **********




