﻿<%@ Page Title="Projects" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="ShrineFoxCom.Projects" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="navipath">
	<a href="/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> 
	<i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %>
</div>
<h1><%: Page.Title %></h1>
Here you can learn more about the status of work-in-progress software I'm developing.
<br><hr>
<br>
<!--Posts-->
<div class="columns">
	<asp:PlaceHolder ID="CardsPlaceholder" runat="server"></asp:PlaceHolder>
</div>
<script src="https://p.trellocdn.com/embed.min.js"></script>
</asp:Content>
