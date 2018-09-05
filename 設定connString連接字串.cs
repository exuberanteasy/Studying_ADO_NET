//設定connString連接字串

Data Soure=(LocalDB)
\MSSQLLocalDB;
AttachDbFilename=|DataDirectory|
Northwind.mdf;
Integrated Security=True
//===========================================================
//Form1
using System.Data.SqlClient;

namespace connectionDemo3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ConnString))
            {
                cn.Open();
                MessageBox.Show("連接資料庫: " + cn.Database, "Form1狀態");
            }
        }
    }
}


//========================================================
//Form2
using System.Data.SqlClient;

namespace connectionDemo3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            using( SqlConnection cn = new SqlConnection(Properties.Settings.Default.ConnString))
            {
                cn.Open();
                MessageBox.Show("連接資料庫: " + cn.Database, "Form2狀態");
            }
        }
    }
}
//=====================================================
