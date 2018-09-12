* 手動撰寫 GridView 各種功能(DataReader + SqlCommand)
* 後置程式碼(一): 自己寫 DBInit 副程式與 Page_Load 事件

using System.Web.Configuration;
using System.Data.SqlClient;

namespace Go_GridView
{
    public partial class About : Page
    {
        //----- 這一段程式很常被用到，所以獨立寫成一個DBInit副程式 -----
        protected void DBInit()
        {
            Boolean haveRec = false;
            int p = Convert.ToInt32(Request["p"]);  // p 就是「目前在第幾頁?」

            //---- ADO.NET / DataReader----(start)----
            //資料庫的連線字串，已經事先寫好，存放在Web.Config檔案裡。
            SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["存放在>......."].ConnectionString);
            //---- 不需要使用多重結果集(MARS); MultipleActiveResultSets=True ----------
            Conn.Open();

            SqlCommand cmd = new SqlCommand("select count(id) from test", Conn);

            //SQL指令共撈到多少筆(列)紀錄。RecordCount資料總筆(列)數
            int RecordCount = (int)cmd.ExecuteScalar();
            cmd.Cancel();
            //=== ADO.NET / DateReader ===(End)======

            int PageSize = GridView2.PageSize;
            //每頁展示幾筆紀錄?直接給GridView的PageSize屬性決定。
            //如果撈不到紀錄，程式就結束。---- Start -------
            if (RecordCount == 0)
            {
                Response.Write("<h2>抱歉!無法找到您需要的紀錄!</h2>");
                Conn.Close();
                Response.End();
            } //----如果撈不到紀錄，程式就結束。----End-------

            //Pages 紀錄的總頁數。搜尋到的所有紀錄，共需「幾頁」才能全部呈現?
            int Pages = ((RecordCount + PageSize) - 1) / (PageSize); //除去，取得「商」。
            //----部分程式省略----請參閱本範例電子檔。------------------------

            int NowPageCount = 0;  //NowPageCount，目前這頁的紀錄
            if (p > 0)
            {
                NowPageCount = (p - 1) * PageSize;
                // PageSize，每頁展示幾筆紀錄(上面設定過了)
            }
            Response.Write("<h3>搜尋資料庫: &nbsp; &nbsp;(共計" + RecordCount + "筆 / 共需" + Pages + "頁)</h3>");
            Response.Write("<hr width='97%' size='1'>");

            //===組合SQL指令=====
            SqlDataReader dr = null;

            string SqlStr = "Select test_time, id, title, summary from test Order By id";
            SqlStr += " OFFSET " + (NowPageCount) + "ROWS FETCH NEXT " + (PageSize) + " ROWS ONLY";

            //==== SQL 2012指令的OFFSET...FETCH ======
            Response.Write(SqlStr + "< br />");

            SqlCommand cmd1 = new SqlCommand(SqlStr, Conn);
            dr = cmd1.ExecuteReader();

            while (dr.Read())
            {
                haveRec = true;
                GridView2.DataSource = dr;
                GridView2.DataBind();
            }
            cmd1.Cancel();
            dr.Close();
            Conn.Close();
            Conn.Dispose();

            if (haveRec)  //分頁功能
            {
                if (Pages > 0)
                {
                    //---有傳來「頁數(p)」，而且頁數正確(大於零)，出現<上一頁>、<下一頁>這些功能------
                    //---部分程式省略--------
                }
            }
            //--------

            // IsNumeric Function，檢查是否為整數型態? return trun or false static bool IsNumeric(object Expression)
            // {
            //     //-------程式碼省略----------
            // }



        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DBInit();
            }
        }
    }
}

//================================================================
注意!!
這是重點!
在 Web畫面上"做的任何動作"，例如: 按下任何一個ASP.NET Button按鈕......等等。
都會引起PostBack(回傳)進而重新觸發Page_Load 事件。
所以，我們才會在裡面，設計一段 if(!Page.PostBack)或是 IF Not Page.PostBack 判斷式來判斷「網頁是否第一次被執行?」這個觀念很重要






