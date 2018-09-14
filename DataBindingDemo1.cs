// 18-7 資料繫結---DataBindingDemo1.sln




using System.Data;
using System.Data.SqlClient;

namespace DataBindingDemo1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection())
            {
              //-------- 資料庫改一下 ---------------------
                cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + 
                    "AttachDbFilename=|DataDirctory|ch18DB.mdf;" + 
                    "Integrated Security=True";
                SqlDataAdapter daEmployee = new SqlDataAdapter("Select * From 員工 Order By 編號 DESC", cn);
                DataSet ds = new DataSet();
                daEmployee.Fill(ds, "員工");

                //ComboBox 控制項資料繫結
                comboBox1.DataSource = ds;
                comboBox1.DisplayMember = "員工.編號";

                //TextBox控制項資料繫結
                textBox1.DataBindings.Add("Text", ds, "員工.姓名");
                textBox2.DataBindings.Add("Text", ds, "員工.電話");
                textBox3.DataBindings.Add("Text", ds, "員工.薪資");

                //DataGridView控制項資料繫結
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "員工";

            }
        }
    }
}

//----- 如何將 DataTable進行關聯 ------------------------------




