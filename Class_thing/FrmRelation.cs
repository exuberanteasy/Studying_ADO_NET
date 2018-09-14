// FrmRelation
// 20180914 上課的東西

namespace ADO.NET._4._Disconnected_離線環境_DataSet
{
    public partial class FrmRelation : Form
    {
        public FrmRelation()
        {
            InitializeComponent();
        }

        private void categoriesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.categoriesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.northwindDataSet);

        }

        private void FrmRelation_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'northwindDataSet.Products' 資料表。您可以視需要進行移動或移除。
            this.productsTableAdapter.Fill(this.northwindDataSet.Products);
            // TODO: 這行程式碼會將資料載入 'northwindDataSet.Categories' 資料表。您可以視需要進行移動或移除。
            this.categoriesTableAdapter.Fill(this.northwindDataSet.Categories);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.categoriesBindingSource;
            this.dataGridView1.DataMember = "FK_Products_Categories";

            //==================================
            NorthwindDataSet.CategoriesRow parentrow = this.northwindDataSet.Categories[this.categoriesBindingSource.Position];
            DataRow[] childRows = parentrow.GetChildRows("FK_Products_Categories");

            this.listBox1.Items.Clear();

            foreach (DataRow row in childRows)
            {
                this.listBox1.Items.Add(row["CategoryID"] + "_" + row["ProductName"]);
            }



        }
    }
}
