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

        //Windows 驗證
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
                conn.Open();
                MessageBox.Show("successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        //SQL Server 驗證
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source = .; Initial Catalog=Northwind; User ID = sa; Password = sa");
                conn.Open();
                MessageBox.Show("successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        // .Config  組態檔 - Class Settings
        private void button3_Click(object sender, EventArgs e)
        {
            //    MessageBox.Show(Settings.Default.MyNWConnectionString);
            //    //Settings.Default.MyNWConnectionString = "hjj";
            //    MessageBox.Show(Settings.Default.count.ToString());

            try
            {
                using (SqlConnection conn = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    using(SqlCommand command = new SqlCommand("select * from Products", conn))
                    {
                        conn.Open();
                        using(IDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                this.listBox1.Items.Add(dataReader["ProductName"]);
                            }
                        } 
                    } // command.Dispose()
                } // auto conn.close(); conn.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        // .Config  組態檔 - ConfigurationManager
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["ADO.NET.Properties.Settings.NorthwindConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand command = new SqlCommand("select * from Products", conn))
                    {
                        conn.Open();
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                this.listBox1.Items.Add(dataReader["ProductName"]);
                            }
                        }  //command.Dispose();
                    }  // auto conn.Close();  conn.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        //加密 組態檔連接字串
        private void button58_Click(object sender, EventArgs e)
        {
            try
            {
                //加密
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConfigurationSection section = config.Sections["connectionStrings"];
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                config.Save();
                MessageBox.Show("加密成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // throw;
            }
        }
        
        //解密 組態檔連接字串
        private void button59_Click(object sender, EventArgs e)
        {
            try
            {
                //解密
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConfigurationSection section = config.Sections["connectionStrings"];
                section.SectionInformation.UnprotectSection();
                config.Save();

                MessageBox.Show("解密成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        //SQL Server Local DB
        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                string connString = Settings.Default.MyLocalDB;
                using(SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand command = new SqlCommand("select * from MyTable", conn))
                    {
                        conn.Open();
                        using(SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                this.listBox1.Items.Add(dataReader["aaa"]);
                            }
                        }
                    } // command.Dispose()
                } // auto conn.Close(); conn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        //SQL Server  Local DB 資料庫檔案 - 絕對路徑
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string connString = @"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\ADO.NET\ADO.NET\ADO.NET\bin\Debug\MyAlbum.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using(SqlCommand command = new SqlCommand("Select * from MyTable", conn))
                    {
                        conn.Open();
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                this.listBox1.Items.Add(dataReader["City"]);
                            }
                        } //command.Dispose()
                    } // auto conn.Close(); conn.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }    
        }
        
        //SQL Server  Local DB 資料庫檔案 - 相對路徑  - |DataDirectory|
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string connString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MyAlbum.mdf;
Integrated Security=True; Connect Timeout=30";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand command = new SqlCommand("Select * From MyTable", conn))
                    {
                        conn.Open();
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                this.listBox1.Items.Add(dataReader["City"]);
                            }
                        }
                    } //command.Dispose()
                } // auto conn.Close(); conn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        //SQL Server  Local DB 資料庫檔案 - 相對路徑  - sqlConnectionStringBuilder
        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = @"(LocalDB)\MSSQLLocalDB";
            builder.AttachDBFilename = Application.StartupPath + @"\MyAlbum.mdf";
            builder.IntegratedSecurity = true;

            using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("Select * From MyTable", conn))
                {
                    conn.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        this.listBox1.Items.Clear();

                        while (dataReader.Read())
                        {
                            this.listBox1.Items.Add(dataReader["City"]);
                        }
                    }

                }
            }
        }
        
        //StateChange Event => Connected
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["ADO.NET.Properties.Settings.NorthwindConnectionString"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.StateChange += Conn_StateChange;
                    using (SqlCommand command = new SqlCommand("Select * From Products", conn))
                    {
                        conn.Open();
                        using (SqlDataReader dataReader = command.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                this.listBox2.Items.Add(dataReader["ProductName"]);
                            }
                        }
                    } //command.Dispose()
                } // auto conn.Close(); conn.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        //  DisConnected - 離線 DataSet   =>   2. 0 XXXTableAdapter
        private void button10_Click(object sender, EventArgs e)
        {
            this.productsTableAdapter1.Connection.StateChange += Conn_StateChange;

            this.productsTableAdapter1.Fill(this.northwindDataSet1.Products);
            this.dataGridView1.DataSource = this.northwindDataSet1.Products;
        }
        
        // DisConnected - 離線 DataSet  =>  1.0 SqlTableAdapter
        private void button8_Click(object sender, EventArgs e)
        {
            // 應該是因為有更好的方法，所以老師沒有實作
        }
        
        //連線數 Label3  =>  Pool #1 NW - 100 connection open
        private void button9_Click(object sender, EventArgs e)
        {
            SqlConnection[] conns = new SqlConnection[100];
            SqlDataReader[] dataReaders = new SqlDataReader[100];

            for (int i = 0; i < conns.Length; i++)
            {
                conns[i] = new SqlConnection(Settings.Default.NorthwindConnectionString);
                conns[i].Open();

                this.label3.Text = (i + 1).ToString();
                Application.DoEvents();

                SqlCommand command = new SqlCommand("select * from products", conns[i]);

                dataReaders[i] = command.ExecuteReader();  // new SqlDataReader();

                while (dataReaders[i].Read())
                {
                    this.listBox3.Items.Add(dataReaders[i]["ProduvtName"]);
                }
            }
        }
        
        //連線數 Label3  =>  Pool #2 AW - 200 connection open
        private void button14_Click(object sender, EventArgs e)
        {
            const int MAXPoolSize = 200;
            SqlConnection[] conns = new SqlConnection[MAXPoolSize];
            SqlDataReader[] dataReaders = new SqlDataReader[MAXPoolSize];

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Settings.Default.NorthwindConnectionString);
            builder.InitialCatalog = "AdventureWorks";
            builder.MaxPoolSize = MAXPoolSize;
            builder.ConnectTimeout = 2;
            //MessageBox.Show(builder.ConnectionString);

            for (int i = 0; i < conns.Length; i++)
            {
                conns[i] = new SqlConnection(builder.ConnectionString);
                conns[i].Open();

                this.label3.Text = (i + 1).ToString();
                Application.DoEvents();

                SqlCommand command = new SqlCommand("select * from Production.Product", conns[i]);

                dataReaders[i] = command.ExecuteReader(); //new SqlDataReader();

                while (dataReaders[i].Read())
                {
                    this.listBox3.Items.Add(dataReaders[i]["Name"]);
                }    
            }
        }
        
        // Pooling(共用連接 / 集區)  提高 AP 效能   =>   label1  =>  Pool = true
        private void button15_Click(object sender, EventArgs e)
        {
            SqlConnection[] conns = new SqlConnection[100];
            SqlDataReader[] dataReaders = new SqlDataReader[100];

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Settings.Default.NorthwindConnectionString);
            builder.Pooling = true;

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            for (int i = 0; i < conns.Length - 1; i++)
            {
                conns[i] = new SqlConnection(builder.ConnectionString);
                conns[i].Open();

                //this.label3.Text = (i + 1).toString();
                //Application.DoEvents();

                SqlCommand command = new SqlCommand("Select * from products", conns[i]);

                dataReaders[i] = command.ExecuteReader();  //new SqlDataReader();

                while (dataReaders[i].Read())
                {
                    this.listBox3.Items.Add(dataReaders[i]["ProductName"]);
                }

                conns[i].Close();  // conn Return to POOL
            }
            watch.Stop();

            this.label1.Text = watch.Elapsed.TotalSeconds.ToString();

            //DateTime T1 = DateTime.Now;
            //T1.Subtract(T2)
        }
        
         // Pooling(共用連接 / 集區)  提高 AP 效能   =>   label1  =>  Pool = false
        private void button16_Click(object sender, EventArgs e)
        {
            SqlConnection[] conns = new SqlConnection[100];
            SqlDataReader[] dataReaders = new SqlDataReader[100];

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(Settings.Default.NorthwindConnectionString);
            builder.Pooling = false;

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            for (int i = 0; i < conns.Length - 1; i++)
            {
                conns[i] = new SqlConnection(builder.ConnectionString);
                conns[i].Open();

                //this.label3.Text = (i + 1).ToString();
                //Application.DoEvents();

                SqlCommand command = new SqlCommand("Select * From products", conns[i]);

                dataReaders[i] = command.ExecuteReader();

                while (dataReaders[i].Read())
                {
                    this.listBox3.Items.Add(dataReaders[i]["ProductName"]);
                }
                conns[i].Close();  // conn NOT Return to POOL
            }
            watch.Stop();

            this.label2.Text = watch.Elapsed.TotalSeconds.ToString();
        }
        
        //sqlException
        private void button23_Click(object sender, EventArgs e)
        {
            string connString = "Data Source=.;Initial Catalog=Northwindxxx;Integrated Security=True";
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connString);

            SqlCommand command = new SqlCommand("Select * from Products", conn);
            SqlDataReader dr = null;

            try
            {
                conn.Open();
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    this.comboBox2.Items.Add(dr["ProductName"]);
                }

                this.comboBox2.SelectedIndex = 0;
            }
            catch (SqlException ex)
            {
                string s = "";
                for (int i = 0; i <= ex.Errors.Count -1; i++)
                {
                    s += string.Format("{0}: {1}", ex.Errors[i].Number, ex.Errors[i].Message) + Environment.NewLine;
                }
                MessageBox.Show("error count:" + ex.Errors.Count + Environment.NewLine + s);
                //throw;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (command != null) command.Dispose();

                if(conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
        
        
        
        
        
        
        
        
        
        
        
    }
}






