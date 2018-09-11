多重結果作用集(MARS)與網路留言板(關聯式資料表)

* 注意，DataReader 開啟期間，每一次的資料庫連接(Connection) 只能供給一個 DataReader使用。
  必須等到原本使用的那一個 DataReader關閉後，才能執行Connection的任何命令，包括建立其他的 DataReader。
* 不遵守這規則就會出錯!

* 到了 .NET 2.0 以後，才逐步放寬這種限制。不過，有兩個限制:
=> 必須搭配 MS SQL2005 (或以後的新版本，如 SQL 2008 )。
=> 採用 MARS(多重作用結果集)。在資料來源(資料庫)的「連接字串」內，設定MultipleActiveResultSets = True。
=> 程式的「預設值」會停用MARS功能。必須自己動手將"MultipleActiveResultSets = True" 加入資料來源的連接字串，便可啟用該功能。
String connectionString = "Data Source = MSSQL1; ....資料庫連線字串...(省略)......; MultipleActiveResultSets = True";
