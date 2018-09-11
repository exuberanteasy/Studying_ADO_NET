.NextResult()，傳回多個結果



using System.Web.Configuration;

namespace GoDataReader
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["存在Web.config檔案裡面的連接字串"]
                .ConnectionString);
            Conn.Open();

            //===兩句SQL指令記得加分號=======
            SqlCommand cmd = new SqlCommand("Select id, title from test; Select id, author from test_talk", Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //====將資料呈現在畫面上============
            //===== 讀取時若還有其他的 Result Set，則 DataReader的 .NextResult() 會傳回 true。 =====
            do
            {
                Response.Write("<br>資料庫欄位名稱:" + dr.GetName(0) + dr.GetName(0));
                while (dr.Read())  //---巢狀迴圈--
                {
                    Response.Write("<hr>" + dr.GetSqlInt32(0) + "<br>" + dr.GetSqlString(1));
                    //===用.GetSqlxxx方法來擷取資料，效率會更好 ======
                }
            } while (dr.NextResult());
            //=== 依序往下讀取另一個 Result Set，直到沒有其他 Result Set為止 ====
            //=====若沒有其他的Result Set 就讓 dr.NextResult() = false，迴圈便會停止。 ==========
            cmd.Cancel();  //===關閉資源&資料庫的連線===
            dr.Close();
            Conn.Close();
        }
    }
}
