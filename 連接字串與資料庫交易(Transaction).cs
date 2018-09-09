連接字串與資料庫交易(Transaction)

// Transactions.aspx.cs

// 重點!!: System.Transactions(命名空間)必須在VS裡面自己手動「加入參考」才可以，否則的話，即使自己在後置程式碼的最上方，加入命名空間仍會發生錯誤。

//=====================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;  //=== 自己手動將 System.Transactions.DLL 加入參考
using System.Data;
using System.Data.SqlClient;

namespace GoSqlDataSource.fonts
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (SqlConnection Conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager
                .ConnectionStrings["存在Web.Config檔裡面的連接字串"].ConnectionString))
            {
                //===連接資料庫的連接字串 ConnectionString ===
                Conn.Open();

                SqlCommand myCommand = Conn.CreateCommand();
                SqlTransaction myTrans;

                //==== 交易 ======
                myTrans = Conn.BeginTransaction();

                //Must assign both transaction object and connection to Command
                //object for a pending local transaction.myCommand.Connection = Conn;
                myCommand.Transaction = myTrans;

                try
                {
                    //==執行兩行 SQL指令，新增資料 =====
                    myCommand.CommandText = "Insert into test(test_time, title, summary) VALUES (getdate(), '雙重 Catch #1--Transaction', '雙重 Catch #1--Transaction')";
                    myCommand.ExecuteNonQuery();

                    myCommand.CommandText = "Insert into test(test_time, title, summary) VALUES (getdate(), '雙重 Catch #2--Transaction', '雙重 Catch #2--Transaction')";
                    myCommand.ExecuteNonQuery();
                    //==================================

                    myTrans.Commit();
                    Response.Write("兩筆資料新增成功! ");
                }
                catch (Exception ex)
                {
                    Response.Write("Commit Exception Type: " + ex.GetType().ToString() + "<br>");
                    Response.Write(" Message: " + ex.Message.ToString() + "<hr><br>");

                    //===雙重 Catch，獲取例外狀況。======
                    try
                    {
                        myTrans.Rollback(); //--失敗的話，執行Rollback
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as a closed connection.
                        Response.Write("Rollback Exception Type: " + ex2.GetType().ToString() + "<br>");
                        Response.Write("  Message: " + ex2.Message.ToString());

                        // throw;
                    }
                    // throw;
                }

            } //--關閉 DB 的連接 (using)
        }
    }
}

//===================== SqlTransaction類別的方法 ============================
∥ Commit     =>   認可資料庫交易。( 覆寫DbTransaction.Commit() )。
∥ CreateObjRef  => 建立包含所有關資訊的物件，這些資訊是產生用來與遠端物件通訊的所需 Proxy (繼承自 MarshaByRefObject )。
∥ Dispose   => 多載。
∥ Equals  => 判斷指定的 Object 和目前的 Object 是否相等 (繼承自 Object )。
∥ Finalize  =>  在記憶體回收(GC) 回收 Object 前，允許 Object 嘗試釋放資源並執行其他清除作業(繼承自 Object)。
∥ GetHashCode  =>  做為特定型別的雜奏函式(繼承自 Object )。
∥ GetLifetimeService  =>  擷取控制這個執行個體存留期(Lifetime)原則的目前存留期服務物件(繼承自 MarshaByRefObject )。
∥ GetType   =>   取得目前執行個體的 Type (繼承自 Object )。
∥ IntializeLifetimeService => 取得存留期服務物件來控制這個執行個體的存留期原則(繼承自 MarshaByRefObject)。
∥ MemberwiseClone   => 多載。
∥ Rollback  =>  多載。從暫停狀態復原交易。
∥ Save  =>  建立交易中的儲存點 (可用來復原部分的交易)，以及指定儲存點名稱。



//===================== SqlTransaction類別的方法 ============================ End
