先關閉 Command 再關閉 DataReader
// 不注意的話會浪費很多資源喔。


if (dr != null)
{
  cmd.Cancel();
  //----- 關閉 DataReader 之前，一定要先「取消」SqlCommand ---------------
  dr.Close();
}
