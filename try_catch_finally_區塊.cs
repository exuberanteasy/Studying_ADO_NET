使用 try...catch...finally 區塊
// 5-21
//--- Default_1_0_DataReader_Manual.aspx -------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//---------記得在後置程式碼(Code Behine)的最上方，把所需要用到的命名空間(NameSpace)寫進來
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;  //---

namespace GoDataReader
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //=== 微軟SDK文件的範本 ===================================================

            //---- 上面已經事先寫好 NameSpace ----- Using System.Web.Configuration; -------------------
            //--- 或是寫成下面這一行(連接資料庫) --------------
            //ConnectionStrings["存在Web.Config檔案裡面的字串，例如 testConnectionString"]
            SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString);

            //----- 連接 DB 的連接字串 --------------------------------------
            SqlDataReader dr = null;
            SqlCommand cmd;
            cmd = new SqlCommand("select id, test_time, summary, author from test", Conn);

            try
            {
                //----- 以下程式，只放「執行期間」的指令! -----------------
                Conn.Open(); //--- 連接 DB ---------
                dr = cmd.ExecuteReader();  //---執行SQL指令，取出資料 ---------
                GridView1.DataSource = dr;
                GridView1.DataBind();  //--- 資料繫結 --------
            }
            catch (Exception ex)
            {
                //---- 如果程式有錯誤或是例外狀況，將執行這一段 ------------------
                Response.Write("Error Message----" + ex.ToString() + "<HR/>");
                throw;
            }
            finally
            {
                if (dr != null)
                {
                    cmd.Cancel();
                    //------ 關閉 DataReader 之前，一定要先「取消」 SqlCommand ------------
                    dr.Close();
                }
                if (Conn.State == /*System.Data.*/ConnectionState.Open)
                {
                    Conn.Close();
                    Conn.Dispose();
                }
            }
        }
    }
}

//=====================================================================================
* 上面的程式碼中，必須在關閉 DataReader(dr.Close())之後，才能關閉資料庫連接(Conn.Close()),這個順序不要搞錯。
* DataReader的 .Close()方法為了輸出參數、傳回值和 RecordsAffected 填入值(影響了幾列資料? 通常在新增、刪除、修改的SQL指令上)，
  將會增加許多時間來關閉 SqlDataReader。此舉在大量或複雜SQL查詢時，處理時間會更加明顯。
* 如果這一次查詢的傳回值與資料數量並不顯著，建議您先使用 SqlCommand物件的 .Cancel()方法(先把 Command 物件取消後)，
  在呼叫 DataReader的 .Close()，可降低關閉 SqlDataReader的時間。 如此一來可提升效率。
  

















