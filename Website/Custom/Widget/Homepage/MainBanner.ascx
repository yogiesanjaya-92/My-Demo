<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainBanner.ascx.cs" Inherits="FirstDemo.Custom.Widget.Homepage.MainBanner" %>

<aside id="colorlib-hero">
    <div class="flexslider">
        <ul class="slides">
            <li style="background-image: url(/Html/images/img_bg_4.jpg);">
                <div class="overlay"></div>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-8 col-sm-12 col-md-offset-2 slider-text">
                            <div class="slider-text-inner text-center">
                                <asp:HyperLink ID="hypSlideFirst" runat="server" NavigateUrl="/Custom/Page/User_Post.aspx">
                                <h1>Post</h1>
                                <h2>See what we provide for you</h2>
                                </asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
            <li style="background-image: url(/Html/images/img_bg_2.jpg);">
                <div class="overlay"></div>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-8 col-sm-12 col-md-offset-2 slider-text">
                            <div class="slider-text-inner text-center">
                                <asp:HyperLink ID="hypSlideSecond" runat="server" NavigateUrl="/Custom/Page/User_News.aspx">
                                <h1>News</h1>
                                <h2>Read the latest news from us</h2>
                                </asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
            <li style="background-image: url(/Html/images/img_bg_3.jpg);">
                <div class="overlay"></div>
                <div class="container-fluids">
                    <div class="row">
                        <div class="col-md-8 col-sm-12 col-md-offset-2 slider-text">
                            <div class="slider-text-inner text-center">
                                <asp:HyperLink ID="hypSlideThird" runat="server" NavigateUrl="/Custom/Page/User_Feedback.aspx">
                                <h1>Feedback</h1>
                                <h2>Improve our quality with your comment</h2>
                                </asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
            <li style="background-image: url(/Html/images/img_bg_1.jpg);">
                <div class="overlay"></div>
                <div class="container-fluids">
                    <div class="row">
                        <div class="col-md-8 col-sm-12 col-md-offset-2 slider-text">
                            <div class="slider-text-inner text-center">
                                <asp:HyperLink ID="hypSlideFourth" runat="server" NavigateUrl="/Custom/Page/User_About.aspx">
                                <h1>About</h1>
                                <h2>Learn more anything about us</h2>
                                </asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </div>
</aside>
