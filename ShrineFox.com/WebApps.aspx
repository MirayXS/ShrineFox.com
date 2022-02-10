<%@ Page Title="Web Apps" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebApps.aspx.cs" Inherits="ShrineFoxCom.WebApps" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="navipath">
	<a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> 
	<i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %>
</div>
<h1><%: Page.Title %></h1>
Here you can find various web utilities I've developed for Persona modding.
<br><hr>
<br>
<!--Posts-->
<div class="columns">
	<asp:PlaceHolder ID="CardsPlaceholder" runat="server"></asp:PlaceHolder>
</div>
</asp:Content>
