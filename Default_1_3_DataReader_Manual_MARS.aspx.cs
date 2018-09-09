//Case Study: 留言版


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


namespace GoSqlDataSource
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //-------- 連接資料庫，並且啟動 MARS ---------
            SqlConnection Conn = new SqlConnection("server = localhost; uid = 帳號; pwd = 密碼; database = 資料庫名稱; MultipleActiveResultSets = true");

            SqlDataReader dr  = null;
            SqlDataReader dr2 = null;

            SqlCommand cmd  = new SqlCommand("select id, title from test", Conn);
            SqlCommand cmd2 = null;

            try
            {
                Conn.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Response.Write("<p>" + dr["id"] + " / <b>" + dr["title"] + "</b></p>");
                        Response.Write("<blockquote><font size = 2 color=gray>");

                        //-------- 啟動 MARS 之後(MultipleActiveResultSets = true) ----
                        //-----第一個 DataReader(變數名稱dr)尚未關閉，就直接使用第二個DataReader(變數名稱dr2)-----

                        //列出每一篇的「讀者留言」
                        cmd2 = new SqlCommand("select test_id, article from test_talk where test_id = " + dr["id"], Conn);
                        dr2 = cmd2.ExecuteReader();

                        if (dr2.HasRows)
                        {
                            while (dr2.Read())
                            {
                                Response.Write("==讀者留言==<br>");
                                Response.Write(dr2["test_id"] + " / " + dr2["article"] + "<br>");
                            }
                        }
                        Response.Write("</font></blockquote>");  
                    }
                }
            }
            finally
            {
                if (dr != null)
                {
                    cmd.Cancel();  //---關閉 DataReader 之前，先取消 SqlCommand
                    dr.Close();
                }
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                    Conn.Dispose();
                }
            }
        }
    }
}

//=== 註解: ========================================= 
// <blockquote>...</blockquote> 段落巷內縮排。












