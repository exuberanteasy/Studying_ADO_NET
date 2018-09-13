如何使用應用程式組態檔存取資料庫的連接字串

//--------FileName:Form1.cs--------------------------
using System.Data.SqlClient;

namespace ConnectionDemo3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.connString))
            {
                cn.Open();
                MessageBox.Show("連接資料庫: " + cn.Database, "Form1狀態");
            }
        }
    }
}

//--------FileName:Form2.cs--------------------------
using System.Data.SqlClient;

namespace ConnectionDemo3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.connString))
            {
                cn.Open();
                MessageBox.Show("連接資料庫:" + cn.Database, "Form2狀態");
            }
        }
    }
}

//----Step 06 ----

