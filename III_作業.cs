//練習
//III 老師出的作業

//FrmOverview.cs
using System.Data;
using System.Data.SqlClient;

namespace ADO.NET.Starter
{
    public partial class FrmOverview : Form
    {
        public FrmOverview()
        {
            InitializeComponent();
            this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;

            //Fill()
            //....
        }

        // Load Data 用
        private void button1_Click(object sender, EventArgs e)
        {

            //Connected 連線環境 - Connected  實體 DB

            //Step 1: SqlConnection
            //Step 2: SqlCommand
            //Step 3: SqlDataReader
            //Step 4: Control UI

            SqlConnection conn = null;
            SqlCommand command = null;
            try
            {
                //在 屬性 => 設定
                conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
                conn.Open();
                command = new SqlCommand("Select * from Products", conn);
                SqlDataReader dataReader = command.ExecuteReader();

                this.listBox1.Items.Clear(); //把listBox 清掉??

                while (dataReader.Read())
                {
                    //this.listBox1.Items.Add(dataReader["UnitPrice"] + "\t" + dataReader["ProductName"]);
                    //string s = string.Format("{0:c2}\t{1, -50} {2:yyyy/MM/dd} {3}", dataReader["UnitPrice"], dataReader["ProductName"], DateTime.Now, 200);
                    string s = $"{dataReader["UnitPrice"],10:c2}\t{dataReader["productName"],-50} {DateTime.Now:yyyy/MM/dd}";
                    this.listBox1.Items.Add(s);
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();

                    //釋放 System.ComponentModel.Component 所使用的所有資源。
                    conn.Dispose();
                }
                // MessageBox.Show("done");
            }           
        }
        
        // Notes 用
        private void button2_Click(object sender, EventArgs e)
        {
            //SqlDataReader dataReader = new SqlDataReader();
            Graphics g = this.CreateGraphics(); //new Graphics();
        }
        
        // using...  語法糖
        private void button3_Click(object sender, EventArgs e)
        {
            //Connected 連線環境 - Connected  實體 DB

            //Step 1: SqlConnection
            //Step 2: SqlCommand
            //Step 3: SqlDataReader
            //Step 4: Control UI

            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True"))
                {
                    using(SqlCommand command = new SqlCommand("Select * From Products", conn))
                    {
                        conn.Open();
                        SqlDataReader dataReader = command.ExecuteReader();

                        this.listBox1.Items.Clear(); //清掉 listBox1
                        while (dataReader.Read())
                        {
                            string s = $"{dataReader["UnitPrice"],10:c2}\t{dataReader["ProductName"],-50} {DateTime.Now:yyyy/MM/dd}";
                            this.listBox1.Items.Add(s);
                        }
                    } //auto command.Dispose()
                } //auto call conn.Close(); conn.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        // test using
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = null;

                using (conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True"))
                {
                    conn.Disposed += Conn_Disposed; //按 Tab 產生
                    using (SqlCommand command = new SqlCommand("Select * From products", conn))
                    {
                        conn.Open();
                        MessageBox.Show(conn.State.ToString());
                        SqlDataReader dataReader = command.ExecuteReader();

                        this.listBox1.Items.Clear();

                        while (dataReader.Read())
                        {
                            string s = $"{dataReader["UnitPrice"],10:c2}\t{dataReader["ProductName"],-50} {DateTime.Now:yyyy/MM/dd}";
                            this.listBox1.Items.Add(s);
                        }
                    } //auto command.Dispose()
                } // auto call conn.Close();  conn.Dispose(); this.button1.PerformClick()
                MessageBox.Show(conn.State.ToString());

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //產生這玩意
        private void Conn_Disposed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            MessageBox.Show("conn Dispose");
        }
        
        
        // 開始 ****** ADO.NET 1.0   - SqlDataAdapter *****************************************************
        // DisConnected => Load Data ???
        private void button5_Click(object sender, EventArgs e)
        {
            //DisConnected 離線環境 -  DataSet

            //Step 1:SqlConnection
            //Step 2: SqlDataAdapter
            //Step 3: DataSet
            //Step 4: Control UI

            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter("Select * From Products", conn);
            DataSet ds = new DataSet();

            adapter.Fill(ds); //Auto conn.open() => SqlCommand.executeXXX()...SqlDataReader...=> conn.Close()

            this.dataGridView1.DataSource = ds.Tables[0];
        }
        
        // Products unitPrice > 30
        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter("Select * From Products where UnitPrice > 30", conn);
            DataSet ds = new DataSet();

            adapter.Fill(ds);  //Auto conn.Open() => SqlCommand.executeXXX()..sqlDataReader... => conn.Close()

            this.dataGridView1.DataSource = ds.Tables[0];
        }
        // 結束 ****** ADO.NET 1.0   - SqlDataAdapter ****************************************************
        
        // 開始 ****** ADO.NET 2.0   - SqlDataAdapter ****************************************************
        // Load Data - Products
        private void button8_Click(object sender, EventArgs e)
        {
            //2.0 xxxTableAdapter
            //Fill()=>Auto conn.open()=>SqlCommand.executeXXX()..sqlDataReader...=>conn.Close()
            
            this.productsTableAdapter1.Fill(this.northwindDataSet1.Products);
            this.dataGridView2.DataSource = this.northwindDataSet1.Products;
        }
        
        //Categories
        private void button22_Click_1(object sender, EventArgs e)
        {
            this.categoriesTableAdapter1.Fill(this.northwindDataSet1.Categories);
            this.dataGridView2.DataSource = this.northwindDataSet1.Categories;
        }
        
        //Customers
        private void button23_Click(object sender, EventArgs e)
        {
            // this.customersTableAdapter1.Fill(this.northwindDataSet1.Customers);
            this.dataGridView2.DataSource = this.northwindDataSet1.Customers;
        }
        
        //Products unitPrice > 30
        private void button7_Click(object sender, EventArgs e)
        {
            bool b = true;
            b = false;

            bool? b1;
            b1 = true;
            b1 = false;
            b1 = null;
            
            this.productsTableAdapter1.FillByUnitPrice(this.northwindDataSet1.Products, 30);
            this.dataGridView2.DataSource = this.northwindDataSet1.Products;
        }
        
        // Insert Product
        private void button9_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.InsertProduct("xxx", true);
        }
        
        //Update Products
        private void button10_Click(object sender, EventArgs e)
        {
            // RowState.... I/D/U
            this.productsTableAdapter1.Update(this.northwindDataSet1.Products);
        }
        // 結束 ****** ADO.NET 2.0   - SqlDataAdapter ***************************************************
        
        
        //Load Categories
        private void button12_Click(object sender, EventArgs e)
        {
            this.categoriesTableAdapter1.Fill(this.northwindDataSet1.Categories);
            // this.dataGridView3.DataSource = this.northwindDataSet1.Categories;

            this.bindingSource1.DataSource = this.northwindDataSet1.Categories;
            this.dataGridView3.DataSource = this.bindingSource1;

            //==============================================================
            this.bindingNavigator1.BindingSource = this.bindingSource1;

            //==============================================================
            this.textBox1.DataBindings.Add("Text", this.bindingSource1, "CategoryName");
            this.pictureBox1.DataBindings.Add("Image", this.bindingSource1, "Picture", true);
        }
        
        //移至最前
        private void button13_Click(object sender, EventArgs e)
        {
            //this.bindingSource1.MoveFirst();
            this.bindingSource1.Position = 0;
        }
        
        //移至上一頁
        private void button14_Click(object sender, EventArgs e)
        {
            //this.bindingSource1.MovePrevious();
            this.bindingSource1.Position -= 1;
        }
        
        //移至下一頁
        private void button15_Click(object sender, EventArgs e)
        {
            //this.bindingSource1.MoveNext();
            this.bindingSource1.Position += 1;
            //this.label4.Text = (this.bindingSource1.Position + 1) + "/" + this.bindingSource1.Count;
        }
        
        //移至最後一頁
        private void button16_Click(object sender, EventArgs e)
        {
            //this.bindingSource1.MoveLast();
            this.bindingSource1.Position = this.bindingSource1.Count - 1;
        }
        
        //在bindingSource1 上按2下 => 
        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            this.label4.Text = $"{this.bindingSource1.Position + 1 }/ {this.bindingSource1.Count}";
        }
        
        //FrmTool
        private void button17_Click(object sender, EventArgs e)
        {
            FrmTool f = new FrmTool();
            f.Show();
        }
        
        //DataSet結構 => Load Data
        private void button18_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(this.northwindDataSet1.Tables.Count.ToString());

            this.categoriesTableAdapter1.Fill(this.northwindDataSet1.Categories);
            this.productsTableAdapter1.Fill(this.northwindDataSet1.Products);
            this.customersTableAdapter1.Fill(this.northwindDataSet1.Customers);

            this.dataGridView4.DataSource = this.northwindDataSet1.Categories;
            this.dataGridView5.DataSource = this.northwindDataSet1.Products;
            this.dataGridView6.DataSource = this.northwindDataSet1.Customers;

            this.listBox2.Items.Clear();

            for(int i=0; i <= this.northwindDataSet1.Tables.Count-1; i++)
            {
                DataTable table = this.northwindDataSet1.Tables[i];
                this.listBox2.Items.Add(table.TableName);

                // table.Columns  資料行 (Column Schma)

                this.listBox2.Font = new Font("標楷體", 10, FontStyle.Bold);
                string s = "";
                for(int column = 0; column <= table.Columns.Count-1; column++)
                {
                    s += $"{table.Columns[column].ColumnName,-30}";
                }
                this.listBox2.Items.Add(s);

                // ToDo ......
                // table.Rows  資料列(資料記錄)
                for(int row = 0; row <= table.Rows.Count-1; row++)
                {
                    this.listBox2.Items.Add(table.Rows[row]);  //.ToString();

                    // DataRow dr = table.Rows[row];
                    // dr[0]
                    // table.Rows[row][0]

                    s = "";
                    for(int column=0; column <= table.Columns.Count-1; column++)
                    {
                        s += $"{table.Rows[row][column],-30}";
                    }
                    this.listBox2.Items.Add(s);
                }
                this.listBox2.Items.Add("=========================================");
            }
        }
        
        //測試抓取記錄
        private void button19_Click(object sender, EventArgs e)
        {
            //ProductRow - 強型別 - Strong type (具型別)
            MessageBox.Show(this.northwindDataSet1.Products[0].ProductName);

            //======================================================================
            //compiler OK; Run time error - DataRow - 弱型別 weak (不具型別)
            MessageBox.Show(this.northwindDataSet1.Products.Rows[0]["ProductName"].ToString());
            MessageBox.Show(this.northwindDataSet1.Products.Rows[0][1].ToString());
        }
        
        //Write XML
        private void button20_Click(object sender, EventArgs e)
        {
            this.northwindDataSet1.Products.WriteXml("Products.xml", XmlWriteMode.WriteSchema);
        }
        
        //Read XML
        private void button21_Click(object sender, EventArgs e)
        {
            this.northwindDataSet1.Products.Clear();
            this.northwindDataSet1.Products.ReadXml("Products.xml");
            this.dataGridView5.DataSource = this.northwindDataSet1.Products;
        }

        
        
        
    }
}


//========================================================================================================
//FrmSqlConnection.cs
namespace Starter

{
    public partial class FrmSqlConnection : Form
    {
        public FrmSqlConnection()
        {
            InitializeComponent();
            this.tabControl1.SelectedIndex = 1;
        }
    }
}






