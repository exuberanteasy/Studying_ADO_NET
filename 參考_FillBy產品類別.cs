// 同學版



using System.Data;
using System.Data.SqlClient;
using RelationsDemo1.Properties;

namespace RelationsDemo1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.categoriesTableAdapter1.FillBy產品類別(this.myNorthwind1.Categories);
            this.dgvCategory.DataSource = (this.myNorthwind1.Categories);

            this.categoriesTableAdapter1.FillBy產品類別(this.myNorthwind1.Categories);
            this.dgvCategory.DataSource = (this.myNorthwind1.Categories);
            //    using (SqlConnection cn = new SqlConnection(Settings.Default.GoCnn)
            //    {
            //        //cn.ConnectionString = @"Data source=(LocalDB)\MSSQLLocalDB;" +
            //        //                      "AttachDbFilename=|DataDirectory|ch18DB.mdf;" +
            //        //                      "Integrated Security=Trun";
            //        //DataSet ds = this.
            //        //SqlDataAdapter daCategory = new SqlDataAdapter("SECECT * FROM 產品類別", cn);
            //        //daCategory.Fill(ds, "產品類別");
            //        //SqlDataAdapter daProduct = new SqlDataAdapter("SECECT * FROM 產品資料", cn);
            //        this.categoriesTableAdapter1.FillBy產品類別(this.myNorthwind1.Categories)
            //        this.dgvCategory.DataSource = this.myNorthwind1.Categories.ToString();
            //        //daProduct.Fill(ds, "產品資料");
            //        //ds.Relations.Add("FK_產品資料_產品類別",
            //        //    ds.Tables["產品類別"].Columns["類別編號"],
            //        //    ds.Tables["產品資料"].Columns["類別編號"]);
            //        //dgvCategory.DataSource = ds;
            //        //dgvCategory.DataMember = "產品類別";
            //        //dgvCategory.Dock = DockStyle.Top;  //dgvCategory停駐在表單上方
            //        //dgvProduct.DataSource = ds;
            //        //dgvProduct.DataMember = "產品類別.FK_產品資料_產品類別";
            //        //dgvProduct.Dock = DockStyle.Fill; //dgvProduct填滿整個表單
            //}
        }
    }
}
    



