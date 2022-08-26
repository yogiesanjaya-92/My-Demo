using System;
using System.Web.UI;
using FirstDemo.Custom.Widget;
using FirstDemo.Custom.Widget.Form;

namespace FirstDemo
{
    public partial class User_Feedback : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WidgetRendering();
        }

        private void WidgetRendering()
        {
            MainNavigation mainNav = LoadControl("~/Custom/Widget/Navigation/MainNavigation.ascx") as MainNavigation;
            Master.FindControl("PlaceHolderHeaderTop").Controls.Add(mainNav);

            FormFeedback feedBack = LoadControl("~/Custom/Widget/Form/FormFeedback.ascx") as FormFeedback;
            Master.FindControl("PlaceHolderMain").Controls.Add(feedBack);

            FooterNavigation footerNav = LoadControl("~/Custom/Widget/Navigation/FooterNavigation.ascx") as FooterNavigation;
            Master.FindControl("PlaceHolderFooterNav").Controls.Add(footerNav);
        }
    }
}