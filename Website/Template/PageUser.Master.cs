using System;
using System.Web.UI;
using FirstDemo.Custom.Widget;

namespace FirstDemo
{
    public partial class PageUser : MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            WidgetRendering();

            if (Session["New"] != null)
            {
                LblUser.Text = "Welcome, " + Session["New"].ToString();
                BtnLogin.Visible = false;
                BtnLogout.Visible = true;
            }
            else
            {
                LblUser.Text = "Welcome, Guest";
                BtnLogin.Visible = true;
                BtnLogout.Visible = false;
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Custom/Page/User_Login.aspx");
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Session["New"] = null;
            Response.Redirect("/Custom/Page/User_Home.aspx");
        }

        private void WidgetRendering()
        {
            //MainNavigation mainNav = LoadControl("~/Custom/Widget/Navigation/MainNavigation.ascx") as MainNavigation;
            //PlaceHolderHeader.Controls.Add(mainNav);

            //FooterNavigation footerNav = LoadControl("~/Custom/Widget/Navigation/FooterNavigation.ascx") as FooterNavigation;
            //PlaceHolderFooter.Controls.Add(footerNav);
        }
    }
}