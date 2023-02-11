<%@ Page Title="Text Search" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TextSearch.aspx.cs" Inherits="ShrineFoxCom.TextSearch" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="navipath">
		<a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> 
		<i class="fa fa-angle-right" aria-hidden="true"></i> <a href="https://shrinefox.com/WebApps">Apps</a> 
        <i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %>
	</div>
	<h1><%: Page.Title %></h1>
    <div class="container">
        <div class="notice red">
            <b>Notice:</b> Due to performance concerns, this feature of the site has been discontinued.
            <br>It may return if I find a suitable workaround in the future, so stay tuned. I apologize for the inconvenience.
            <br>In the meantime, you can download and search dialog in <a href="https://drive.google.com/file/d/113DuAlmIqb8AU4xBYNuU5FDxPP3mVX67/view?usp=share_link">these .txt files</a>.
        </div>
    </div>
</asp:Content>
