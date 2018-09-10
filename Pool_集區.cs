集區(Pool)的建立及指派

using System.Data.SqlClient;  //自行加入

private void button1_Click(object sender, EventArgs e)
{
    using (SqlConnection connection = new SqlConnection("Integrated Security=SSPI; Initial Catalog=Northwind"))
    {
        connection.Open();
        // 註: 開啟了第一個集區 A (Pool A)。
    }

    using (SqlConnection connection = new SqlConnection("Integrated Security=SSPI; Initial Catalog=pubs"))
    {
        connection.Open();
        // 註: 開啟了第二個集區 B (Pool B)。
        // 因為連接字串與上面集區 A 略有不同。
    }

    using (SqlConnection connection = new SqlConnection("Integrated Security=SSPI; Initial Catalog=Northwind"))
    {
        connection.Open();
        // 註: 連接字串與第一個相同，所以仍使用第一個集區 A (Pool A)。
    }
}
