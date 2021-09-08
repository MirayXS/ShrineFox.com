<%@ Page Title="Text Search" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TextSearch.aspx.cs" Inherits="ShrineFoxcom.TextSearch" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="Sidebar" runat="server"></asp:PlaceHolder>
    <b><a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true""></i> ShrineFox.com</a> <i class="fa fa-angle-right" aria-hidden="true"></i> Apps <i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %></b>
    <h1><%: Page.Title %></h1>
    <div class="container">
        <div class="columns">
            <div class="column col-4">
                <asp:dropdownlist class="form-select" runat="server" id="gameList">
                    <asp:listitem text="Persona 5 (PS3)" value="P5"></asp:listitem>
                    <asp:listitem text="Persona 5 Royal" value="P5R"></asp:listitem>
                    <asp:listitem text="Persona 4" value="P4"></asp:listitem>
                    <asp:listitem text="Persona 4 Golden" value="P4G"></asp:listitem>
                    <asp:listitem text="Persona 3 FES" value="P3FES"></asp:listitem>
                    <asp:listitem text="Persona 3 Portable" value="P3P"></asp:listitem>
                </asp:dropdownlist>
            </div>
            <div class="column col-6">
                <asp:TextBox ID="SearchBox" runat="server" class="form-input" placeholder="Search..."></asp:TextBox>
            </div>
            <div class="column col-2">
                <asp:Button ID="SearchBtn" runat="server" Text="Search" class="btn btn-primary" style="width: 100%;" OnClick="Search_Click" />
            </div>
        </div>
        <!--Options -->
        <div class="">
            <div class="columns">
                <div class="column col-2">
                    <asp:RadioButton id="radioMsg" Text="MessageScript" Checked="True" GroupName="RadioGroup1" runat="server" />
                </div>
                <div class="column col-2">
                    <asp:RadioButton id="radioFlow" Text="FlowScript" GroupName="RadioGroup1" runat="server"/>
                </div>
                <div class="column col-2">
                    <asp:CheckBox ID="CaseSensitive" runat="server" Text="Case Sensitive" Enabled="true" Checked="false" />
                </div>
            </div>
        </div>
    </div>
    <br>
    <br>
    <br>
    <div id="SearchTip" runat="server"></div>
    <div id="Div1" runat="server">
        <div id="results" runat="server">
            <div class="card">
                <center>
                    <p>Looking for a file containing certain dialog? Look no further!
                    <br>Just select a game and enter the text you're searching with.</p>
                </center>
            </div>
        </div>
        <input id="Hidden1" type="hidden"  runat="server"  value=""/>
        <asp:Panel ID="HiddenPostsPanel" runat="server">
            <asp:LinkButton ID="Next" runat="server" OnClick="Next_Click"><div class="unhide"><i class="fas fa-ellipsis-h"></i> Show More Occurences </div></asp:LinkButton>
        </asp:Panel>
        <br /><br />
    </div>
</asp:Content>
