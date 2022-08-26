<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormFeedback.ascx.cs" Inherits="FirstDemo.Custom.Widget.Form.FormFeedback" %>

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
                <h2>Give Your Feedback Here</h2>
                <div class="row form-group">
                    <div class="col-md-12">
                        <asp:Label ID="lblName" runat="server" Text="Name* : "></asp:Label>
                        <asp:TextBox ID="txtName" runat="server" placeholder="Your full name" CssClass="form-control" OnKeyDown="preventDefault();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVName" runat="server" ErrorMessage="This field is required !" ControlToValidate="txtName" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row form-group">
                    <div class="col-md-12">
                        <asp:Label ID="lblOrganisation" runat="server" Text="Organisation* : "></asp:Label>
                        <asp:TextBox ID="txtOrganisation" runat="server" placeholder="Your organisation name" CssClass="form-control" OnKeyDown="preventDefault();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVOrganisation" runat="server" ErrorMessage="This field is required !" ControlToValidate="txtOrganisation" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row form-group">
                    <div class="col-md-12">
                        <asp:Label ID="lblPosition" runat="server" Text="Position* : "></asp:Label>
                        <asp:TextBox ID="txtPosition" runat="server" placeholder="Your current position" CssClass="form-control" OnKeyDown="preventDefault();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVPosition" runat="server" ErrorMessage="This field is required !" ControlToValidate="txtPosition" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row form-group">
                    <div class="col-md-12">
                        <asp:Label ID="lblRate" runat="server" Text="Rate Us : "></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlSatisfaction" AutoPostBack="false" ClientIDMode="Static" CssClass="form-control">
                            <asp:ListItem Value="Best" Selected="True">Best</asp:ListItem>
                            <asp:ListItem Value="Good">Good</asp:ListItem>
                            <asp:ListItem Value="Bad">Bad</asp:ListItem>
                            <asp:ListItem Value="Worst">Worst</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row form-group">
                    <div class="col-md-12">
                        <asp:Label ID="lblEmail" runat="server" Text="Email* : "></asp:Label>
                        <asp:TextBox ID="txtEmail" runat="server" TextMode="SingleLine" placeholder="Your email address" CssClass="form-control" OnKeyDown="preventDefault();"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVEmail" runat="server" ErrorMessage="This field is required !" ControlToValidate="txtEmail" ForeColor="Red" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REVEmail" runat="server" ErrorMessage="Invalid Email Format !" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail" ForeColor="Red" SetFocusOnError="true" Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                </div>

                <div class="row form-group">
                    <div class="col-md-12">
                        <asp:Label ID="lblUsability" runat="server" Text="Usability : "></asp:Label>
                        <asp:RadioButtonList ID="rblUsability" runat="server" CssClass="form-control" Style="display: inline-table">
                            <asp:ListItem Value="Useful" Selected="True">Useful</asp:ListItem>
                            <asp:ListItem Value="Sometimes">Sometimes</asp:ListItem>
                            <asp:ListItem Value="Useless">Useless</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>

                <div class="row form-group">
                    <div class="col-md-12">
                        <asp:Label ID="lblComment" runat="server" Text="Your Comment : "></asp:Label>
                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" cols="30" Rows="10" placeholder="Say something about us" Style="resize:none" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
</div>
