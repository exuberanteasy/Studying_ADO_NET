//練習

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
                MessageBox.Show("done");
            }
                      
        }
      }
    }
