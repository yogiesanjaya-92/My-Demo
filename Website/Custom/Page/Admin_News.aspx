<%@ Page Title="" Language="C#" MasterPageFile="~/Template/PageAdmin.Master" AutoEventWireup="true" CodeBehind="Admin_News.aspx.cs" Inherits="FirstDemo.Admin_News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .auto-style2 {
            width: 100%;
        }

        .auto-style3 {
            width: 100px;
        }

        .auto-style4 {
            width: 100px;
            height: 23px;
        }

        .auto-style5 {
            height: 23px;
        }

        .auto-style6 {
            width: 646px;
        }

        .auto-style7 {
            height: 23px;
            width: 646px;
        }

        .auto-style8 {
            text-align: center;
            width: 646px;
        }

        .auto-style10 {
            width: 150px;
            height: 19px;
        }

        .auto-style12 {
            width: 150px;
        }
    </style>

    
    <script src="/Html/js/jquery/jquery-3.4.1.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_FuImages').change(function (event) {
                var tmppath = URL.createObjectURL(event.target.files[0]);
                $('#ContentPlaceHolder1_ImgCategoryLogo').fadeIn("fast").attr('src', tmppath);
            });
        });
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="PnlNewsContent" runat="server" Visible="true">
        <table class="auto-style2">
            <tr>
                <td class="auto-style12">Title :</td>
                <td class="auto-style6">
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style12">Date :</td>
                <td class="auto-style6">
                    <asp:TextBox ID="TextBox1" runat="server" TextMode="DateTime"></asp:TextBox>
                    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Set" />
                    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" OnSelectionChanged="Calendar1_SelectionChanged" Width="200px" Visible="False">
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                        <NextPrevStyle VerticalAlign="Bottom" />
                        <OtherMonthDayStyle ForeColor="#808080" />
                        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" />
                        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <WeekendDayStyle BackColor="#FFFFCC" />
                    </asp:Calendar>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style12">Category :</td>
                <td class="auto-style6">
                    <asp:DropDownList runat="server" ID="DdlNewsCategory" AutoPostBack="true" ClientIDMode="Static">
                        <%--<asp:ListItem>-</asp:ListItem>
                    <asp:ListItem>Sport</asp:ListItem>
                    <asp:ListItem>Technology</asp:ListItem>
                    <asp:ListItem>Weather</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style12">News</td>
                <td class="auto-style6">
                    <asp:TextBox ID="TextBox3" runat="server" Height="300px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style12">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style10"></td>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style5"></td>
            </tr>
            <tr>
                <td class="auto-style12">
                    <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Category" Width="144px" />
                </td>
                <td class="auto-style8">
                    <asp:Button ID="Button1" runat="server" Text="Post" OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" Text="Delete" OnClick="Button2_Click" />
                    <asp:Button ID="Button3" runat="server" Text="Clear" OnClick="Button3_Click" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style12">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <br />
        <br />

        <asp:HiddenField ID="HiddenField1" runat="server" />
        <div class="footer_nav">
            <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" HorizontalAlign="Center" AllowPaging="true" PageSize="3" OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="Id">
                <Columns>
                    <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                    <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                    <asp:BoundField DataField="CategoryName" HeaderText="Category" SortExpression="Category" />
                    <asp:BoundField DataField="News" HeaderText="News" SortExpression="News" />
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="lnk_OnClick">View</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>

    <asp:Panel ID="PnlNewsCategory" runat="server" Visible="false">
        <table class="auto-style2">
            <tr>
                <td class="auto-style3">ID :</td>
                <td class="auto-style6">
                    <asp:TextBox ID="TxtCategoryID" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVCategoryId" runat="server" ErrorMessage="Category Id is required !" ControlToValidate="TxtCategoryID" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">Name :</td>
                <td class="auto-style6">
                    <asp:TextBox ID="TxtCategoryName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVCategoryName" runat="server" ErrorMessage="Category Name is required !" ControlToValidate="TxtCategoryName" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">Logo :</td>
                <td class="auto-style6">
                    <asp:FileUpload ID="FuImages" runat="server" onchange="path()" Width="200px" />
                    <br />
                    <asp:Image ID="ImgCategoryLogo" runat="server" BorderStyle="Solid" Height="150px" Width="200px" ImageUrl="~/Image/no-image.jpg" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">Description</td>
                <td class="auto-style6">
                    <asp:TextBox ID="TxtCategoryDescription" runat="server" Height="150px" TextMode="MultiLine" Width="400px"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4"></td>
                <td class="auto-style7">&nbsp;</td>
                <td class="auto-style5"></td>
            </tr>
            <tr>
                <td class="auto-style12">
                    <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Content" Width="144px" CausesValidation="False"/>
                </td>
                <td class="auto-style8">
                    <asp:Button ID="BtnCategoryAdd" runat="server" Text="Add" OnClick="BtnCategoryAdd_Click" />
                    <asp:Button ID="BtnCategoryDelete" runat="server" Text="Delete" OnClick="BtnCategoryDelete_Click" />
                    <asp:Button ID="BtnCategoryClear" runat="server" Text="Clear" OnClick="BtnCategoryClear_Click" CausesValidation="False"/>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <br />
        <br />

        <div class="footer_nav">
            <asp:GridView ID="GdvCategory" runat="server" OnRowDataBound="GdvCategory_RowDataBound" AutoGenerateColumns="False" HorizontalAlign="Center" AllowPaging="true" PageSize="3" OnPageIndexChanging="GdvCategory_PageIndexChanging" DataKeyNames="CategoryID">
                <Columns>
                    <asp:BoundField DataField="CategoryID" HeaderText="Id" SortExpression="Id" />
                    <asp:BoundField DataField="CategoryName" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="CategoryLogo" HeaderText="Logo" SortExpression="Logo" />
                    <asp:BoundField DataField="CategoryDescription" HeaderText="Description" SortExpression="Description" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LBtnCategory" runat="server" CommandArgument='<%# Eval("CategoryID") %>' OnClick="LBtnCategory_Click" CausesValidation="False">View</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
</asp:Content>
