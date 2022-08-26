<%@ Page Language="C#" MasterPageFile="~/Template/PageAdmin.Master" AutoEventWireup="true" CodeBehind="Admin_Feedback.aspx.cs" Inherits="FirstDemo.Admin_Feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <label>From :</label>
    <asp:TextBox ID="TxtDateFrom" runat="server" TextMode="Date"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RFVDateFrom" runat="server" ErrorMessage="This is required Field !" ControlToValidate="TxtDateFrom" ForeColor="Red" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
    <label>To :</label>
    <asp:TextBox ID="TxtDateTo" runat="server" TextMode="Date"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RFVDateTo" runat="server" ErrorMessage="This is required Field !" ControlToValidate="TxtDateTo" ForeColor="Red" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:Button ID="BtnFilter" runat="server" Text="Filter" OnClick="BtnFilter_Click" />
    <br />
    <asp:Button ID="BtnDownload" runat="server" Text="Download" OnClick="BtnDownload_Click" Enabled="false"/>
    <asp:GridView ID="GdvFeedback" runat="server" OnRowDataBound="GdvFeedback_RowDataBound" AutoGenerateColumns="False" HorizontalAlign="Center" AllowPaging="true" PageSize="15" OnPageIndexChanging="GdvFeedback_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="DateSubmitted" HeaderText="DateSubmitted" SortExpression="DateSubmitted" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Organisation" HeaderText="Organisation" SortExpression="Organisation" />
            <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Satisfaction" HeaderText="Satisfaction" SortExpression="Satisfaction" />
            <asp:BoundField DataField="Usability" HeaderText="Usability" SortExpression="Usability" />
            <asp:BoundField DataField="Comment" HeaderText="Comment" SortExpression="Comment" />
        </Columns>
    </asp:GridView>
</asp:Content>
