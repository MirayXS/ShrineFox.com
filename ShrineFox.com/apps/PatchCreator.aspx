<%@ Page Title="Patch Creator" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PatchCreator.aspx.cs" Inherits="ShrineFoxCom.PatchCreator" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="Sidebar" runat="server"></asp:PlaceHolder>
    <b><a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true""></i> ShrineFox.com</a> <i class="fa fa-angle-right" aria-hidden="true"></i> Apps <i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %></b>
    <h1><%: Page.Title %></h1>
    <!-- Old/New Format Notice -->
    <div class="notices yellow">
        <p>
            Generate a <b>patch.yml</b> to use for modding Persona 5 on RPCS3.
            <br><a href="https://shrinefox.com/guides/2019/04/19/persona-5-rpcs3-modding-guide-1-downloads-and-setup/">Read more about Modding P5</a>.
            <br>
            <br><b>New Format</b>: Works with RPCS3's new Patch Manager. Place downloaded <kbd>patch.yml</kbd> in your <code>RPCS3/Patches</code> folder and go to <code>Manage > Game Patches</code>.
            <br><b>Old Format</b>: Can use to patch the EBOOT to run on custom firmware. <a href="https://shrinefox.com/guides/2019/06/12/persona-5-ps3-eboot-patching/">Read more here</a>.
        </p>
    </div>
    <!-- Old/New Format Options -->
    <div class="">
        <div class="columns">
            <div class="column col-3">
                <b>Format:</b>
            </div>
            <div class="column col-3">
                <asp:RadioButton id="radioNew" Text="New Format" Checked="True" GroupName="RadioGroup1" runat="server" />
            </div>
            <div class="column col-3">
                <asp:RadioButton id="radioOld" Text="Old Format" GroupName="RadioGroup1" runat="server"/>
            </div>
        </div>
    </div>
    <!--PPU Hash Notice-->
    <div class="notices blue">
        <p>
            You can find your PPU hash by running the game and then opening <kbd>RPCS3.log</kbd> with a text editor.
            <br>Use <kbd>CTRL+F</kbd> to search for <code>PPU executable hash</code>, and replace the string below with yours.
            <br><br><b>Patches won't work if you don't provide the correct hash</b>! The default one provided is for the digital EUR copy.
        </p>
    </div>
    <!--PPU Hash-->
    <div class="">
        <div class="columns">
            <div class="column col-2">
                <b>PPU Hash:</b>
            </div>
            <div class="column col-7">
                <asp:TextBox ID="txtBox_PPU" class="form-input" runat="server" Text="PPU-b8c34f774adb367761706a7f685d4f8d9d355426"></asp:TextBox>
            </div>
        </div>
    </div>
    <!--Select All/None Buttons-->
    <br>
    <div class="">
        <div class="columns">
            <div class="column col-5">
                <asp:Button class="btn btn-primary" ID="btnSelect" runat="server" Text="Select All" OnClick="Select_Click" />
            </div>
            <div class="column col-4">
                <asp:Button class="btn btn-primary" ID="btnDeselect" runat="server" Text="Deselect All" OnClick="Deselect_Click" />
            </div>
        </div>
    </div>
    <br>
    <!--Options-->
    <asp:PlaceHolder ID="placeHolder" runat="server"></asp:PlaceHolder>
    <!--Download Button-->
    <br>
    <asp:Button ID="btnDownload" class="btn btn-primary" runat="server" Text="Download patch.yml" OnClick="Download_Click" /> 
    <!--Warnings-->
    <div class="notices red">
        <p>
            For <b>p5_EX</b> and <b>p5_mod_SPRX</b>, <a href="https://gamebanana.com/wips/57221">read setup instructions here</a>.
            <br>The following are <b>incompatible with p5_EX</b> and will be removed automatically:
            <br><i>p5_ModSupport, p5_FileAccessLog, p5_FixScriptPrintingFunctions, p5_CommunityPatches, p5_BgmRandom</i>
            <br><br>Don't worry, p5_EX includes most of this functionality, but better.
        </p>
    </div>
    <br>
    <div class="notices red">
        <p>
            For <b>p5_4k</b>, use with <a href="https://amicitia.github.io/post/p5-4k-upscale">this mod</a>. You can add <a href="https://shrinefox.com/forum/viewtopic.php?f=15&t=527">this mod</a> above it for 4K Royal bustups.
            <br>For <b>p5_communityPatches</b>, install <a href="https://gamebanana.com/gamefiles/13624">this mod</a> with the lowest priority to prevent softlocks.
        </p>
    </div>
</asp:Content>
