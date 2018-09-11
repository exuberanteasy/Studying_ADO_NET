SQL指令的預存程序_Stored Procedure

* 妥善地使用預存程序，在執行速度與安全性上，都是一個不錯的考量。

using System.Web.Configuration;
using System.Data.SqlClient;

namespace GoDataReader
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection Conn = new SqlConnection("資料庫的連接字串");
            SqlDataReader dr = null;
            SqlCommand cmd;

            cmd = new SqlCommand("test_homepage", Conn);
            //==== 設定預存程序，名為test_homepage =========
            //===== 下面這是重點 =======================
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                Conn.Open();
                dr = cmd.ExecuteReader();  //---執行SQL指令，取出資料------

                GridView1.DataSource = dr;
                GridView1.DataBind();  //--- 資料繫結 ----
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

//============================================================
* 善用資料庫裡面的預存程序，可以增加速度，也避免SQL指令與程式碼寫在一起，讓程式碼的可讀性降低，
  進一步也能保護SQL指令與Table Schema的安全(因為不會曝露出來)。
  
* 很多程序設計師未必能寫出很棒的SQL指令、也不會設計完善的Table Schema，如果有一位專任的資料庫管理師(DBA)能處理這些事，
  甚至把程式裡面大部分的SQL指令給最佳化，並存成 預存程序 給程式碼呼叫，相信對於整個團隊的程式設計也有不少好處。











