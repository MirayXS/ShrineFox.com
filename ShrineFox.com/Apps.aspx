<%@ Page Title="Apps" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Apps.aspx.cs" Inherits="ShrineFoxcom.Apps" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<asp:PlaceHolder ID="Sidebar" runat="server"></asp:PlaceHolder>
<a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> <i class="fa fa-angle-right" aria-hidden="true"></i> <a href="https://shrinefox.com/browse"><%: Page.Title %></a> <asp:PlaceHolder ID="Navigation" runat="server"></asp:PlaceHolder>
<h1><%: Page.Title %></h1>
Here you can find various web utilities I've developed to make modding a little more accessible.
<br><br><hr>
<h2><a href="https://shrinefox.com/apps/patchcreator">RPCS3 Patch Creator</a></h2>
Creates a custom <kbd>patch.yml</kbd> file you can use with RPCS3's Patch Manager, containing only the patches you want. 
You can also choose the old format, which works with <kbd>EBOOT.bin</kbd> patchers such as heepatch and RPCS3PatchEboot in case you want to use patches on your Custom Firmware PS3 console.
<br><br><hr>
<h2><a href="https://shrinefox.com/apps/updatecreator">PS4 Patch Creator</a></h2>
Generate a patched <kbd>EBOOT.bin</kbd> to use for modded P5R updates. Includes your desired set of patches.
<br><br><hr>
<h2><a href="https://shrinefox.com/apps/textsearch">Text Search</a></h2>
Discover which file contains a given line of dialog in P3/P4/P5. Also gives you tips on how to edit the file.
<br><br><hr>
<h2><a href="https://shrinefox.com/apps/files">Files</a></h2>
Browse pre-dumped files that can be useful for your mods. These are <b>not</b> full game dumps.
<br><br><hr>
</asp:Content>
