using System;
using System.Web.UI;
// Tambahan
using FirstDemo.Custom.Widget;
using FirstDemo.Custom.Widget.News;

namespace FirstDemo
{
    public partial class User_News : Page
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

            NewsListing newsList = LoadControl("~/Custom/Widget/News/NewsListing.ascx") as NewsListing;
            NewsDetail newsDetail = LoadControl("~/Custom/Widget/News/NewsDetail.ascx") as NewsDetail;

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                Master.FindControl("PlaceHolderMain").Controls.Add(newsDetail);
            }
            else
            {
                Master.FindControl("PlaceHolderMain").Controls.Add(newsList);
            }
        }

    }
}