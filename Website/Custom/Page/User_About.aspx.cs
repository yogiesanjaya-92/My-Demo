using System;
using System.Web.UI;
using FirstDemo.Custom.Widget;

namespace FirstDemo
{
    public partial class User_About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WidgetRendering();
        }

        private void WidgetRendering()
        {
            MainNavigation mainNav = LoadControl("~/Custom/Widget/Navigation/MainNavigation.ascx") as MainNavigation;
            Master.FindControl("PlaceHolderHeaderTop").Controls.Add(mainNav);

            FooterNavigation footerNav = LoadControl("~/Custom/Widget/Navigation/FooterNavigation.ascx") as FooterNavigation;
            Master.FindControl("PlaceHolderFooterNav").Controls.Add(footerNav);
        }
    }
}