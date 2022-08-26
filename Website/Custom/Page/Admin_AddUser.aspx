<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_AddUser.aspx.cs" Inherits="FirstDemo.Admin_AddUser" MasterPageFile="~/Template/PageAdmin.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            height: 26px;
        }
        .auto-style9 {
            height: 26px;
            width: 291px;
        }
        .auto-style10 {
            height: 20px;
            width: 493px;
            margin: 0px 0px;
            float: right;
        }
        .auto-style11 {
            width: 465px;
        }
        .auto-style12 {
            height: 20px;
            width: 291px;
            margin: 0px 0px;
            float: right;
        }
        .auto-style13 {
            width: 291px;
        }
        .auto-style14 {
            width: 243px;
        }
        .auto-style15 {
            height: 26px;
            width: 243px;
        }
        .auto-style16 {
            width: 415px;
        }
    </style>
</asp:Content>

<asp:Content ID = "Content2" ContentPlaceHolderID=ContentPlaceHolder1 runat=server>  

        <div class="auto-style1">
            
            <div class="auto-style2">
          
                <asp:HiddenField ID="HiddenField1" runat="server" />
                
                <table class="auto-style16">
                    <tr>
                        <td class="auto-style14">Id :</td>
                        <td class="auto-style13">
                            <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True" BackColor="#EAEAEA"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style15">User Name :</td>
                        <td class="auto-style9">
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td>
                        <td class="auto-style2">
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="Admin" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style14">Email :</td>
                        <td class="auto-style13">
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style14">Password :</td>
                        <td class="auto-style13">
                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style14">Country :</td>
                        <td class="auto-style13">
                            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style14">
                            <asp:Button ID="Button1" runat="server" Text="Save" OnClick="Button1_Click" />
                        </td>
                        <td class="auto-style12">
                            <asp:Button ID="Button2" runat="server" Text="Delete" OnClick="Button2_Click" />
                        </td>
                        <td class="auto-style11">
                            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Clear" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style14"></td>
                        <td class="auto-style9">
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                            <br />
                            <asp:Label ID="Label2" runat="server"></asp:Label>
                        </td>
                        <td class="auto-style10"></td>
                    </tr>
                </table>
                <br />

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" Allowpaging="true" PageSize="7" OnPageIndexChanging="GridView1_PageIndexChanging" >
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                    <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
                    <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
                    <asp:BoundField DataField="Admin" HeaderText="Admin" SortExpression="Admin" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Id") %>' OnClick="lnk_OnClick">View</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            </div>
        </div>

</asp:Content>
