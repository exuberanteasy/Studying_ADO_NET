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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //使用 using 敘述建立 SqlConnection 物件 cn
            using (SqlConnection cn = new SqlConnection())
            {
                // 建立字串指定 ch16DB.mdf 資料庫
                cn.ConnectionString= @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                                      "AttachDbFilename=|DataDirectory|ch16DB.mdf;" +
                                      "Integrated Security=True";
                cn.Open();
                //將輸入的姓名指定給 searchName 字串變數
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

                    txtShow.Text += "中文: " + dr["中文"].ToString() + Environment.NewLine;

                    txtShow.Text += "English" + dr["English"].ToString() + Environment.NewLine;

                    txtShow.Text += "數學" + dr["數學"].ToString();
                }
                else  // 若沒有該筆紀錄則執行else下面敘述
                {
                    txtShow.Text = "找不到這個學生的成績!";
                }
            }
        }

        
    }
}
