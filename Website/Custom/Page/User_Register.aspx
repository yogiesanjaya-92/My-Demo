<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_Register.aspx.cs" Inherits="FirstDemo.User_Register" MasterPageFile="~/Template/PageUser.Master" %>

<asp:Content ID="CntRegister" ContentPlaceHolderID="PlaceHolderMain" runat="server">
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
                    <h2>Register Your Account Here</h2>
                    <div class="row form-group">
                        <div class="col-md-12">
                            <asp:Label ID="lblUserName" runat="server" Text="User name* : "></asp:Label>
                            <asp:TextBox ID="txtUserName" runat="server" placeholder="Your user name" CssClass="form-control" OnKeyDown="preventDefault();"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVUserName" runat="server" ErrorMessage="User Name is required !" ControlToValidate="txtUserName" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row form-group">
                        <div class="col-md-12">
                            <asp:Label ID="lblEmail" runat="server" Text="Email* : "></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" TextMode="SingleLine" placeholder="Your email address" CssClass="form-control" OnKeyDown="preventDefault();"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVEmail" runat="server" ErrorMessage="Email address is required !" ControlToValidate="txtEmail" ForeColor="Red" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="REVEmail" runat="server" ErrorMessage="Invalid email format !" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ForeColor="Red" SetFocusOnError="true" Display="Dynamic"></asp:RegularExpressionValidator>
                        </div>
                    </div>

                    <div class="row form-group">
                        <div class="col-md-12">
                            <asp:Label ID="lblCountry" runat="server" Text="Country* : "></asp:Label>
                            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="false" ClientIDMode="Static" CssClass="form-control">
                                <asp:ListItem Value="Select Country" Selected="True">Select Country</asp:ListItem>
                                <asp:ListItem Value="INA">INA</asp:ListItem>
                                <asp:ListItem Value="USA">USA</asp:ListItem>
                                <asp:ListItem Value="UK">UK</asp:ListItem>
                                <asp:ListItem Value="UN">UN</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RFVCountry" runat="server" ControlToValidate="ddlCountry" ErrorMessage="Select a country name !" ForeColor="Red" InitialValue="Select Country"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row form-group">
                        <div class="col-md-12">
                            <asp:Label ID="lblPassword" runat="server" Text="Password* : "></asp:Label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Your password" CssClass="form-control" OnKeyDown="preventDefault();"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVPassword" runat="server" ErrorMessage="Password is required !" ControlToValidate="txtPassword" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row form-group">
                        <div class="col-md-12">
                            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password* : "></asp:Label>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="Confirm your password" CssClass="form-control" OnKeyDown="preventDefault();"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="Confirm Password is required !" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CVConfirmPassword" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="Both password must be same" ForeColor="Red"></asp:CompareValidator>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
