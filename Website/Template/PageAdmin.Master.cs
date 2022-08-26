using System;
using System.Web.UI;

namespace FirstDemo
{
    public partial class PageAdmin : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                Label2.Text = "Welcome, " + Session["New"].ToString();
            }
            else
            {
                Response.Redirect("/Custom/Page/User_Home.aspx");
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Session["New"] = null;
            Response.Redirect("/Custom/Page/User_Home.aspx");
        }

    }
}