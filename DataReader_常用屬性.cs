DataReader 常用屬性

//===== Depth_深度 ==========================================================================
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

//===============================================================================
