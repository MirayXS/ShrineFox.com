<%@ Page Title="Get Started" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" Async="true" AutoEventWireup="true" EnableViewState="true" CodeBehind="GetStarted.aspx.cs" Inherits="ShrineFoxCom.GetStarted" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="navipath">
		<a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> 
		<i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %>
	</div>
	<h1><%: Page.Title %></h1>
	<br><asp:PlaceHolder ID="lastUpdated" runat="server"></asp:PlaceHolder>
	<br><br>
    <asp:PlaceHolder ID="MainPlaceHolder" runat="server"></asp:PlaceHolder>
</asp:Content>