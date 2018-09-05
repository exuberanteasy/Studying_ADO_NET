//使用SQL語法注意事項

using System.Data.SqlClient;

namespace SqlStringDemo1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                                      "AttachDbFilename=|DataDirectory|ch16DB.mdf;" +
                                      "Integrated Security=True";

                cn.Open();

                string searchName = txtName.Text;


                //SELECT 敘述的查詢條件為姓名等於 searchName
                string selectCmd = "SELECT * FROM 成績單 WHERE 姓名 = '" +
                       searchName + "'";

                //建立 SqlCommand 物件 cmd
                SqlCommand cmd = new SqlCommand(selectCmd, cn);

                //傳回查詢結果的 SqlDataRadedr物件 dr
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())  //若有該筆紀錄則執行下面敘述
                {
                    txtShow.Text = "學號: " + dr["學號"].ToString() + Environment.NewLine;

                    txtShow.Text += "姓名: " + dr["姓名"].ToString() + Environment.NewLine;

                    txtShow.Text += "國文: " + dr["國文"].ToString() + Environment.NewLine;

                    txtShow.Text += "英文" + dr["英文"].ToString() + Environment.NewLine;

                    txtShow.Text += "數學" + dr["數學"].ToString();
                }
                else  // 若沒有該筆紀錄則執行else下面敘述
                {
                    txtShow.Text = "找不到這個學生的成績!";
                }
            }
            {

            }
        }
    }
}
//====================================================================
// 在 SQL語法中將連續兩個單引號「''」視為一個單引號，所以我們可以用 string 字串類別的 Replace方法將字串中的一個單引號取代為兩個單引號:
string slesectCmd = "SELECT * FROM 成績單 WHERE 姓名 = '"
                    + searchName.replace("'","''") + "'";
//修改為下列敘述即可
string SelectCmd = "SELECT * FROM 成績單 WHERE 姓名 = ' " + searchName.Replace("'", "''") + "'";


