<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsDetail.ascx.cs" Inherits="FirstDemo.Custom.Widget.News.NewsDetail" %>

<div id="colorlib-blog nopadding">
    <div class="container">
        <div class="row row-pt-sm">
            <div class="col">
                <article class="animate-box">
                    <div class="entry">
                        <h2 id="LblTitle" runat="server"></h2>
                        <p class="meta-2">
                            <span>
                                <asp:Image ID="ImgCategoryLogo" runat="server" Height="90px" Width="120px"></asp:Image>
                            </span>
                            <span>
                                <i class="icon-calendar2"></i>
                                <asp:Literal ID="LitDate" runat="server"></asp:Literal>
                            </span>
                            <span>
                                <i class="icon-user"></i>
                                <asp:Literal ID="LitCategory" runat="server"></asp:Literal>
                            </span>
                            <span>
                                <i class="icon-dropbox"></i>
                                <asp:Literal ID="LitCategoryDescription" runat="server"></asp:Literal>
                            </span>
                        </p>
                        <p>
                            <asp:Literal ID="LitContent" runat="server"></asp:Literal>
                        </p>
                        <asp:HyperLink runat="server" ID="HypNewsBack">Back</asp:HyperLink>
                    </div>
                </article>
            </div>
        </div>
    </div>
</div>
