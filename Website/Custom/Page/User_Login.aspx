<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_Login.aspx.cs" Inherits="FirstDemo.User_Login" MasterPageFile="~/Template/PageUser.Master" %>

<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-- Google Tag Manager -->
    <script>(function (w, d, s, l, i) {
            w[l] = w[l] || []; w[l].push({
                'gtm.start':
                    new Date().getTime(), event: 'gtm.js'
            }); var f = d.getElementsByTagName(s)[0],
                j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
                    'https://www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
        })(window, document, 'script', 'dataLayer', 'GTM-MXS278W');</script>
    <!-- End Google Tag Manager -->

</head>

<body style="width: 423px">

    <!-- Google Tag Manager (noscript) -->
    <noscript>
        <iframe src="https://www.googletagmanager.com/ns.html?id=GTM-MXS278W"
            height="0" width="0" style="display: none; visibility: hidden"></iframe>
    </noscript>
    <!-- End Google Tag Manager (noscript) -->

    <form id="form1" runat="server" class="auto-style6">
        <div class="auto-style2">
            <strong><span class="auto-style1">LOGIN</span></strong><br />
            <table class="auto-style3">
                <tr>
                    <td class="auto-style9">User Name :</td>
                    <td class="auto-style10">
                    </td>
                    <td class="auto-style7">
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Please enter User Name" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style9">Password :</td>
                    <td class="auto-style10">
                    </td>
                    <td class="auto-style7">
                        <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="Please enter Password" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style11"></td>
                    <td class="auto-style12">
                    </td>
                    <td class="auto-style13">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Login" Width="63px" />
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Custom/Page/User_Register.aspx">New user register here</asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>--%>

<asp:Content ID="CntLogin" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <script>
        function preventDefault() {
            //$(document).on("keydown", ":input:not(textarea)", function(event) {}
            if (event.key == "Enter") {
                event.preventDefault();
            }
        }
    </script>

    <div id="colorlib-contact">
        <div class="container">
            <div class="row">
                <div class="col-md-10 col-md-offset-1 animate-box">
                    <h2>Login To Your Account</h2>
                    <div class="row form-group">
                        <div class="col-md-2">
                            <asp:Label ID="lblUserName" runat="server" Text="User name* : "></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtUserName" runat="server" placeholder="Your user name" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVUserName" runat="server" ControlToValidate="txtUserName" ErrorMessage="Please enter User Name" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row form-group">
                        <div class="col-md-2">
                            <asp:Label ID="lblPassword" runat="server" Text="Password* : "></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Your password" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Please enter Password" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-2">
                            <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" CssClass="btn btn-primary"/>
                        </div>
                        <div class="col-md-6">
                            <asp:HyperLink ID="hypRegister" runat="server" NavigateUrl="~/Custom/Page/User_Register.aspx">New user register here</asp:HyperLink>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
