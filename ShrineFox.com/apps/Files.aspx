<%@ Page Title="Files" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Files.aspx.cs" Inherits="ShrineFoxCom.Files" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="Sidebar" runat="server"></asp:PlaceHolder>
    <b><a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true""></i> ShrineFox.com</a> <i class="fa fa-angle-right" aria-hidden="true"></i> Apps <i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %></b>
    <h1><%: Page.Title %></h1>
    <div class="notices yellow">
        <p>
            Files necessary to actually run the games are <b>not included</b>.
            These are provided for modding research <b>only</b>.
        </p>
    </div>
    <h5><a href="https://pastebin.com/gkve0TmV">View Links</a></h5>
    Password is the name of this modding group (all lowercase).
    <br><br><br>
</asp:Content>