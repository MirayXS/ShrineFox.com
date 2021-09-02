<%@ Page Title="Text Search" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TextSearch.aspx.cs" Inherits="ShrineFoxcom.TextSearch" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="Sidebar" runat="server"></asp:PlaceHolder>
    <b><a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true""></i> ShrineFox.com</a> <i class="fa fa-angle-right" aria-hidden="true"></i> Apps <i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %></b>
    <h1><%: Page.Title %></h1>
    <div id="tip" runat="server"></div>
    <div class="container">
        <div class="columns">
            <div class="column col-4">
                <asp:dropdownlist class="form-select" runat="server" id="gameList">
                    <asp:listitem text="Persona 5 (PS3)" value="P5"></asp:listitem>
                    <asp:listitem text="Persona 4" value="P4"></asp:listitem>
                    <asp:listitem text="Persona 3 FES" value="P3FES"></asp:listitem>
                    <asp:listitem text="Persona 3 Portable" value="P3P"></asp:listitem>
                    <asp:listitem text="Persona 4 Golden" value="P4G"></asp:listitem>
                </asp:dropdownlist>
            </div>
            <div class="column col-6">
                <asp:TextBox ID="TextBox1" runat="server" class="form-input" placeholder="Search..."></asp:TextBox>
            </div>
            <div class="column col-2">
                <asp:Button ID="Button1" runat="server" Text="Search" class="btn btn-primary" style="width: 100%;" OnClick="Button1_Click" />
            </div>
        </div>
        <span class="float-right">
            <asp:CheckBox ID="CaseSensitive" runat="server" Text="Case Sensitive" Enabled="true" Checked="false" />
        </span>
    </div>
    <br>
    <br>
    <br>
    <div id="Div1" runat="server">
        <div id="results" runat="server">
            <div class="card">
                <center>
                    <p>Looking for a file containing certain dialog? Look no further!
                    <br>Just select a game and enter the text you're searching with.</p>
                </center>
            </div>
        </div>
        <br /><br />
        <input id="Hidden1" type="hidden"  runat="server"  value=""/>
        <asp:Panel ID="HiddenPostsPanel" runat="server">
            <asp:LinkButton ID="HiddenPostsLink" runat="server" OnClick="HiddenPostsLink_Click"><div class="unhide"><i class="fas fa-ellipsis-h"></i> Find Next Occurence </div></asp:LinkButton>
        </asp:Panel>
    </div>
</asp:Content>
