<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsListing.ascx.cs" Inherits="FirstDemo.Custom.Widget.News.NewsListing" %>

<script>
    function checkNextPage() {
        if ($("#HypNewsMaxPage").html() != 0) {
            if ($("#DdlNewsPage").val() == $("#HypNewsMaxPage").html()) {
                return false;
            }
        }
    }

    function checkPrevPage() {
        if ($("#HypNewsMaxPage").html() != 0) {
            if ($("#DdlNewsPage").val() == 1) {
                return false;
            }
        }
    }
</script>

<div id="colorlib-blog">
    <div class="container">
        <div class="sorting">
            <p>This is ny web news page. If you like to publish your news here plese submit in this <a href="https://forms.gle/z2oZ2wJaQUXbcuGK8" target="_blank" rel="noreferrer noopener">form.</a></p>
            <label>Category :</label>
            <asp:DropDownList runat="server" ID="DdlNewsCategory" OnSelectedIndexChanged="DdlNewsCategory_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static"></asp:DropDownList>
            <label>Sort By:</label>
            <asp:DropDownList runat="server" ID="DdlNewsSort" OnSelectedIndexChanged="DdlNewsSort_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static">
                <asp:ListItem Value="Newest">Sort Newest</asp:ListItem>
                <asp:ListItem Value="Oldest">Sort Oldest</asp:ListItem>
                <asp:ListItem Value="A-Z">Sort A-Z</asp:ListItem>
                <asp:ListItem Value="Z-A">Sort Z-A</asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="row row-pt-sm">
            <asp:Repeater runat="server" ID="RptNewsList" OnItemDataBound="RptNewsList_ItemDataBound">
                <ItemTemplate>
                    <div class="col-md-6">
                        <article class="animate-box">
                            <asp:HyperLink ID="HypImageLink" runat="server" CssClass="blog-img text-center">
                                    <span class="icon"><i class="icon-search2"></i></span>
                            </asp:HyperLink>

                            <div class="entry">
                                <h2>
                                    <asp:HyperLink ID="HypNewsLink" runat="server">
                                        <label id="LblTitle" runat="server"></label>
                                    </asp:HyperLink>
                                </h2>
                                <p class="meta-2">
                                    <span><i class="icon-calendar2"></i>
                                        <asp:Literal ID="LitDate" runat="server"></asp:Literal>
                                    </span>
                                </p>
                                <p>
                                    <asp:Literal ID="LitContent" runat="server"></asp:Literal>
                                </p>
                            </div>
                        </article>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    <br />
                    <h2 id="EmptyNews" runat="server" visible='<%# RptNewsList.Items.Count == 0 %>'>No News Found</h2>
                </FooterTemplate>
            </asp:Repeater>
        </div>
</div>

<nav class="pagination" role="navigation">
        <ul>
            <asp:LinkButton runat="server" ID="LnkNewsPrev" OnClick="LnkNewsPrev_Click" OnClientClick="return checkPrevPage();">Prev
            </asp:LinkButton>
            <li>
                <asp:DropDownList runat="server" ID="DdlNewsPage" OnSelectedIndexChanged="DdlNewsPage_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static"></asp:DropDownList>
            </li>
            <li>
                <span>Of</span>
            </li>
            <li>
                <asp:HyperLink runat="server" ID="HypNewsMaxPage" ClientIDMode="Static"></asp:HyperLink>
            </li>
            <asp:LinkButton runat="server" ID="LnkNewsNext" OnClick="LnkNewsNext_Click" OnClientClick="return checkNextPage();">Next
            </asp:LinkButton>
        </ul>
    </nav>
</div>
