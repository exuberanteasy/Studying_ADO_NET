//練習
//III 老師出的作業



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
        
        
        
        
        // 結束 ****** ADO.NET 2.0   - SqlDataAdapter ***************************************************
        
        
        
        
        
    }
}
