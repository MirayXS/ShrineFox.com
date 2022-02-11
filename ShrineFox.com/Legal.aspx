<%@ Page Title="Legal Disclaimer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Legal.aspx.cs" Inherits="ShrineFoxCom.Legal" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="navipath">
		<a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> 
		<i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %>
	</div>
	<h1><%: Page.Title %></h1>
<asp:PlaceHolder ID="lastUpdated" runat="server"></asp:PlaceHolder>
<br>
<br>As a fansite, ShrineFox.com is NOT affiliated, associated, authorized, endorsed by, or in any way officially connected with 
	<a href="http://www.atlus.co.jp/">Atlus Co., Ltd</a>, <a href="http://atlus.com/">Atlus U.S.A., Inc.</a> or 
	<a href="http://sega.jp/">Sega Games Co., Ltd</a>, or any of its subsidiaries or its affiliates. 
	<br><br>The official ATLUS website can be found at https://atlus.com. ATLUS and SEGA are registered in the 
	U.S. Patent and Trademark Office. "ATLUS", "SHIN MEGAMI TENSEI", "CATHERINE" and "PERSONA" are either registered trademarks 
	or trademarks of ATLUS Co., Ltd. or its affiliates.
	<br><br>ShrineFox.com is a noncommercial fansite which does not sell goods or services in relation to 
	Atlus Co., Ltd, Atlus U.S.A., Inc. or Sega Games Co., Ltd. 
	<br><br>All media content such as screenshots, trailers, music, artwork and footage is the property of Atlus Co., Ltd., SEGA Games Co., Ltd or their 
	affiliated companies.
	<br>
	<br>ShrineFox.com, its users, and its administrators are not responsible for any loss or damage to your personal data, software or devices. 
	You are responsible for any undesired results incurred by using instructional material or software found on this website or from 
	another website linked on ShrineFox.com.
	<br>
</asp:Content>
