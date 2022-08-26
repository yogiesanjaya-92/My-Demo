using System;
using System.Web.UI;
// Tambahan
using FirstDemo.Custom.Widget;
using FirstDemo.Custom.Widget.Post;

namespace FirstDemo
{
    public partial class User_Post : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WidgetRendering();
        }

        private void WidgetRendering()
        {
            MainNavigation mainNav = LoadControl("~/Custom/Widget/Navigation/MainNavigation.ascx") as MainNavigation;
            Master.FindControl("PlaceHolderHeaderTop").Controls.Add(mainNav);

            PostListing postListing = LoadControl("~/Custom/Widget/Post/PostListing.ascx") as PostListing;
            Master.FindControl("PlaceHolderMain").Controls.Add(postListing);

            FooterNavigation footerNav = LoadControl("~/Custom/Widget/Navigation/FooterNavigation.ascx") as FooterNavigation;
            Master.FindControl("PlaceHolderFooterNav").Controls.Add(footerNav);
        }
    }
}