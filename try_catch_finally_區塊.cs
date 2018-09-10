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
