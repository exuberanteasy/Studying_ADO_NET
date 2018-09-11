Case Study(二): 計算食物卡路里(可複選、加總)



using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CaseStudy
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["total"] == null) 
            {
                ViewState["total"] = 0; //---- ViewState是網頁程式的狀態管理，請參閱ASP.NET 書籍 ------
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //----點選任何一個子選項，底下的ListBox就會出現，並立刻計算卡路里----
            ListBox1.Items.Add(DropDownList1.SelectedItem.Text + "@" + DropDownList1.SelectedValue);

            Label1.Text = "<font color = blue>" + DropDownList1.SelectedValue + "</font>";
            //-----被選取的這項食物的卡路里-------

            ViewState["total"] = Convert.ToInt32(ViewState["total"]) +
            Convert.ToInt32(DropDownList1.SelectedValue);
            Label2.Text = ViewState["total"].ToString();
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //-----點選ListBox1的子選項，可以刪除之。 -------------
            int word_length = ListBox1.SelectedItem.Text.Length - (ListBox1.SelectedItem.Text.IndexOf("@", 0) + 1);
            Label1.Text = "<font color=red> - " + Right(ListBox1.SelectedItem.Text, word_length) + "</font>";
            //---- 被選取的這項食物的卡路里 -------

            ViewState["total"] = Convert.ToInt32(ViewState["total"]) - Convert
                   .ToInt32(Right(ListBox1.SelectedItem.Text, word_length));
            Label2.Text = ViewState["total"].ToString();

            ListBox1.Items.Remove(ListBox1.SelectedItem.Text);
            //--- 移除ListBox1「被選到的」子選項 --------
        }

        //================================================================
        public static string Right(string sSource, int iLength)
        {
            return sSource.Substring(iLength > sSource.Length ? 0 : sSource.Length - iLength);
            //throw new NotImplementedException();
        }
        // 可以在C#程式最上方加入 using Microsoft.VisualBasic; 命名空間，就能直接在C#程式裡面，使用VB的函數。


        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}

//=====================================================
* 切記!請啟動 DropDownList控制項的AutoPostBack功能。





