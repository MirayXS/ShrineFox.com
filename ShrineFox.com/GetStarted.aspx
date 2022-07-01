<%@ Page Title="Get Started" Language="C#" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" Async="true" AutoEventWireup="true" EnableViewState="true" CodeBehind="GetStarted.aspx.cs" Inherits="ShrineFoxCom.GetStarted" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="navipath">
		<a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> 
		<i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %>
	</div>
	<h1><%: Page.Title %></h1>
    <br>Before you can install mods from <a href="https://shrinefox.com/browse">ShrineFox.com/Browse</a>, 
    you must patch your game.
    <br>The fan-made patch for loading modded files is called <b>Mod Support</b>.
    <br><br>Follow the steps below. The page will walk you through the setup process.
	<br><br>
    <asp:PlaceHolder ID="MainPlaceHolder" runat="server"></asp:PlaceHolder>
</asp:Content>