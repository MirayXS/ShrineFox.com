<%@ Page Title="404 Not Found" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="ShrineFoxCom.NotFound" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="navipath">
        <a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> 
        <i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %>
    </div>

    <h1><%: Page.Title %></h1>
    </div>
    <div class="notice red">
        <p>
            Sorry, the content you were looking for has been moved or no longer exists.
            <br>Click "<b>Explore</b>" to view the sidebar. Maybe you'll find what you need.
        </p>
    </div>
</asp:Content>
