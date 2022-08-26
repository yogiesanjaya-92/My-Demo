<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostListing.ascx.cs" Inherits="FirstDemo.Custom.Widget.Post.PostListing" %>

<div id="colorlib-intro">
    <div class="container">
        <div class="row">
            <div class="row form-group group-header">
                <h1>
                    <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label>
                </h1>
                <h2>
                    <span><i class="icon-calendar2"></i>
                        <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label>
                    </span>
                </h2>
                <asp:DropDownList ID="ddlPost" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPost_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
            </div>

            <div class="row form-group">
                <iframe
                    src="http://www.youtube.com/embed/VIDEO_ID"
                    frameborder="0" runat="server" id="FrmPostYoutube" align="middle" class="form-control is-video"></iframe>
            </div>

            <div class="row form-group">
                <asp:TextBox ID="txtContent" runat="server" ReadOnly="True" TextMode="MultiLine" ViewStateMode="Enabled" cols="30" Rows="30" CssClass="form-control" Style="resize: none"></asp:TextBox>
            </div>
        </div>
    </div>
</div>
