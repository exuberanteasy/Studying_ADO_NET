使用 using... 區塊，自動關閉資源


//---- 可以巢狀使用 using -----------------
using...
{
    using...
    {

    }
}
//---- 結束 using ，自動處置(Dispose) 它控制的所有資源 ------------------

//===============================================================
using System.Web.Configuration;
using System.Data; 
using System.Data.SqlClient;

namespace GoDataReader
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string SQLstr = "Select * From test";

            using (SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["存在Web.config檔案裡面的連接字串，例如命名為testConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SQLstr, Conn);
                Conn.Open(); //-- 連接 DB ---
                //--- 後面「不」需要關閉的動作(Conn.Close())，因為using...End Using 會自己處理資源的後續動作。 --------------

                //----- 設定SQL指令 --------
                SqlDataReader dr = cmd.ExecuteReader();

                //---- 自由發揮，將資料呈現在畫面上 -----------------------------
                //把DataReader獲得的資料，跟GridView控制項繫結(.DataBind()) 在一起。
                //GridView控制項就會自動把資料展現在畫面上了。
                GridView1.DataSource = dr;
                GridView1.DataBind();

                cmd.Cancel();
                dr.Close();  //--- 關閉 DataReader -----
            }
            //---關閉資源 & 資料庫連線 ------
            //----資料庫連線(Conn)會「自動關閉」。因為它是由 Using 來開啟，End Using 會自動關閉之。 ----------------------
        }
    }
}

//=======================================================================================
補充說明: 
* 如果在 using 結束之後，又自己寫上 .Dipose()，嚴重的話可能會降低效能! 這種"重複釋放"記憶體的動作可能會導致駔理時間多出 80%。
  使用uing時不可不慎。
* using 不會處理例外狀況。 如果要自行處理例外，則只能回歸傳統的 try...catch...finally。
* 嚴格來說，比較建議使用 try...catch...來寫。或是同時採用 using 與 try 兩個區塊混合，才能防堵例外狀況的發生。


//=== ex: ====
try
{
    using
    { 
        .....如果遇到例外狀況，還能靠try區塊來了解。..............
    }
}
catch
{
   ......
}
    










