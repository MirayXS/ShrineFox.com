<%@ Page Title="Browse" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Browse.aspx.cs" Inherits="ShrineFoxCom.Browse" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="navipath">
		<a href="/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> 
		<i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %>
	</div>
	<h1><%: Page.Title %></h1>
	<asp:PlaceHolder ID="Notices" runat="server"></asp:PlaceHolder>
	<asp:PlaceHolder ID="LastUpdated" runat="server"></asp:PlaceHolder>
	<asp:PlaceHolder ID="QueryOptions" runat="server"></asp:PlaceHolder>
	<asp:PlaceHolder ID="Pagination1" runat="server"></asp:PlaceHolder>
	<div class="container">
	  <div class="columns">
		  <!--Posts-->
		  <asp:PlaceHolder ID="Posts" runat="server"></asp:PlaceHolder>
	  </div>
	</div>
	<asp:PlaceHolder ID="Pagination2" runat="server"></asp:PlaceHolder>
</asp:Content>