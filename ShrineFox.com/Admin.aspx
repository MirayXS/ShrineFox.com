<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="ShrineFoxcom.Admin" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<asp:PlaceHolder ID="Sidebar" runat="server"></asp:PlaceHolder>
<a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> <i class="fa fa-angle-right" aria-hidden="true"></i> <a href="https://shrinefox.com/browse"><%: Page.Title %></a> <asp:PlaceHolder ID="Navigation" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="Warning" runat="server"></asp:PlaceHolder>
<asp:PlaceHolder ID="Placeholder" runat="server"></asp:PlaceHolder>
</asp:Content>