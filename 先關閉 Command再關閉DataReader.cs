先關閉 Command 再關閉 DataReader
// 不注意的話會浪費很多資源喔。


if (dr != null)
{
  cmd.Cancel();
  //----- 關閉 DataReader 之前，一定要先「取消」SqlCommand ---------------
  dr.Close();
}

如果您不想自己手動來撰寫關閉資源的程式碼，可以參考後續的 using...區塊
usin...區塊在結束時會自動關閉資源，十分便利。
