Case Study(一): 計算食物卡路里，Execute Scalar()



using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CaseStudy
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //SqlConnection Conn = new SqlConnection(WebConfigurationManager
                     .ConnectionStrings["testConnectionString"].ConnectionString);
            //這一行 "Data Source=.;Initial Catalog=Food_Calorie;Integrated Security=True" 不知道是什麼問題 ??
            SqlConnection Conn = new SqlConnection(WebConfigurationManager
                     .ConnectionStrings["Data Source=.;Initial Catalog=Food_Calorie;Integrated Security=True"].ConnectionString);
            SqlDataReader dr = null;

            string sqlstr = "select Food_Calorie from Food_Calorie where id = " + DropDownList1.SelectedValue;
            SqlCommand cmd = new SqlCommand(sqlstr, Conn);

            try
            {
                Conn.Open();
                int food_calorie = (int)cmd.ExecuteScalar();  //--- 執行SQL指令，取出資料 ---------

                Label1.Text = (Convert.ToInt32(TextBox1.Text) * food_calorie).ToString();
            }
            catch (Exception ex)
            {
                Response.Write("Error Message------" + ex.ToString() + "<HR/>");
                throw;
            }
            finally
            {
                if (dr != null)
                {
                    cmd.Cancel();  //-----關閉DataReader之前，一定要先「取消」 SqlCommand --------------
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
