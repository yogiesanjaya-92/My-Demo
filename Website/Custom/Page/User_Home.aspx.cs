using System;
using System.Web.UI;
using FirstDemo.Custom.Widget;
using FirstDemo.Custom.Widget.Homepage;

namespace FirstDemo
{
    public partial class User_Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WidgetRendering();
        }

        private void WidgetRendering()
        {
            MainNavigation mainNav = LoadControl("~/Custom/Widget/Navigation/MainNavigation.ascx") as MainNavigation;
            Master.FindControl("PlaceHolderHeaderTop").Controls.Add(mainNav);

            MainBanner mainBanner = LoadControl("~/Custom/Widget/Homepage/MainBanner.ascx") as MainBanner;
            Master.FindControl("PlaceHolderMain").Controls.Add(mainBanner);

            FooterNavigation footerNav = LoadControl("~/Custom/Widget/Navigation/FooterNavigation.ascx") as FooterNavigation;
            Master.FindControl("PlaceHolderFooterNav").Controls.Add(footerNav);
        }
    }
}