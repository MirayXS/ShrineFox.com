<%@ Page Title="Browse" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Browse.aspx.cs" Inherits="ShrineFox.com.Browse" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<asp:PlaceHolder ID="Sidebar" runat="server"></asp:PlaceHolder>
<a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> <i class="fa fa-angle-right" aria-hidden="true"></i> <a href="https://shrinefox.com/browse"><%: Page.Title %></a> <asp:PlaceHolder ID="Navigation" runat="server"></asp:PlaceHolder>
<h1>Browse</h1>
<asp:PlaceHolder ID="Warning" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="Pagination1" runat="server"></asp:PlaceHolder>
<div class="container">
  <div class="columns">
      <!--Posts-->
      <asp:PlaceHolder ID="Posts" runat="server"></asp:PlaceHolder>
  </div>
</div>
<asp:PlaceHolder ID="Pagination2" runat="server"></asp:PlaceHolder>
</asp:Content>