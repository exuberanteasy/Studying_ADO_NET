巢狀的(多個) using... 區塊

using System.Web.Configuration;
using System.Data.SqlClient;

namespace GoDataReader
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string SQLstr = "select * From test";

            using (SqlConnection Conn = new SqlConnection(WebConfigurationManager
                .ConnectionStrings["存在Web.Config檔案裡面的連接字串，例如命名為testConnectionString"]
                .ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(SQLstr, Conn);
                Conn.Open();

                //===== 設定SQL指令**** 巢狀Using *****==================
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    GridView1.DataSource = dr;
                    GridView1.DataBind();

                    cmd.Cancel();
                    dr.Close();
                } //=====處置DataReader****(巢狀 using )*****===========
                // 連線(Conn)會 自動關閉(.Close) & 處置(Dispose)。因為它是由 Using來開啟，End Using 會自動關閉與處置之。
            }
        }
    }
}
