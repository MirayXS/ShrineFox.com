<%@ Page Title="Patch Creator (Test)" EnableViewState="true" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="ShrineFoxCom.Test" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="navipath">
		<a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> 
		<i class="fa fa-angle-right" aria-hidden="true"></i> <a href="https://shrinefox.com/WebApps">Apps</a> 
        <i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %>
	</div>
	<h1><%: Page.Title %></h1>
    Generate a <b>patch.yml</b> to use for modding Persona 5 (PS3). <a href="https://shrinefox.com/guides/2019/04/19/persona-5-rpcs3-modding-guide-1-downloads-and-setup/">Read more</a>
    <br>Automatically removes conflicting and unwanted patches so you only download what you need.
    <br>
    <br>
    <div class="notice yellow">
        <p>
            This page exists as a testing area for deploying upcoming versions of web applications.
            <br>Nothing seen here is final and might not work as expected.
        </p>
    </div>
    <asp:PlaceHolder ID="lastUpdated" runat="server"></asp:PlaceHolder>
    <br>
    <br>
    <asp:HiddenField ID="HiddenField" runat="server" value="" ClientIDMode="Static" />
    <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel">
        <ProgressTemplate>
            <div class="modal active modal-sm">
                <span class="modal-overlay"></span>
                <div class="loading loading-lg"></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <!--PPU Entry-->
    <div class="card">
        <div class="card-header">
            <div class="card-title h5">1. PPU Hash</div>
            <div class="card-subtitle text-muted">Provide the correct hash or this won't work!</div>
        </div>
        <div class="card-footer">
            <div class="columns">
                <div class="column col-8 text-center">
                    <asp:TextBox ID="txtBox_PPU" class="form-input" runat="server" Text="PPU-b8c34f774adb367761706a7f685d4f8d9d355426"></asp:TextBox>
                </div>
            </div>
            <br><br>Find your PPU hash by running the game and then opening <kbd>RPCS3.log</kbd> with a text editor.
            <br>Search for <code>PPU executable hash</code> with <kbd>CTRL+F</kbd>.
        </div>
    </div>
    <!--Compatibility Notice-->
    <asp:PlaceHolder ID="NoticePlaceHolder2" runat="server"/>
    <!--Patch Selection-->
    <div class="card">
        <div class="card-header">
            <div class="card-title h5">2. Select & Toggle Patches</div>
            <div class="card-subtitle text-muted">Choose a patch to toggle. Hover over one to learn more about it.</div>
        </div>
        <div class="card-footer">
            <div class="columns">
                <div class="column col-5">
                    <asp:LinkButton ID="btnEnableAll" runat="server" Text="Enable All" OnClick="EnableAll_Click" AutoPostBack="true"/>
                </div>
                <div class="column col-5">
                    <asp:LinkButton ID="btnDisableAll" runat="server" Text="Disable All" OnClick="DisableAll_Click" AutoPostBack="true"/>
                </div>
            </div>
            <br>
            <!-- Checkboxes for Patches -->
            <asp:CheckBoxList ID="chkBoxList_Patches" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="CheckBox_SelectedIndexChanged" AutoPostBack="true"/>
        </div>
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <!--Download Info-->
                <div class="card">
                    <div class="card-header">
                        <div class="card-title h5">3. Choose Download Format</div>
                        <div class="card-subtitle text-muted" id="appliedPatches" runat="server"></div>
                        <div class="card-body">
                            <b>New Format</b>: Works with RPCS3's new Patch Manager. Place downloaded <kbd>patch.yml</kbd> in your <code>RPCS3/Patches</code> folder and go to <code>Manage > Game Patches</code>.
                            <br>
                            <br><b>Old Format</b>: Can use to patch <kbd>eboot.bin</kbd> to use patches on PS3 with custom firmware. <a href="https://shrinefox.com/guides/2019/06/12/persona-5-ps3-eboot-patching/">Read more</a>
                            <!--Notice-->
                            <br><asp:PlaceHolder ID="NoticePlaceHolder" runat="server"/>
                            <br>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="chkBoxList_Patches" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnEnableAll" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnDisableAll" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <!--Download Button-->
    <div class="card">
        <div class="card-footer">
            <div class="dropdown dropdown-right float-right"><a class="btn btn-primary dropdown-toggle" tabindex="0">Download patch.yml <i class="icon icon-caret"></i></a>
                <ul class="menu text-left">
                    <li class="menu-item"><asp:LinkButton runat="server" id="newFormat" OnClick="Download_Click">New Format</asp:LinkButton></li>
                    <li class="menu-item"><asp:LinkButton runat="server" id="oldFormat" OnClick="Download_Click">Old Format</asp:LinkButton></li>
                </ul>
            </div>
        </div>
    </div>

    <script type="text/javascript">
    function ShowProgress()
    {
        document.getElementById('<% Response.Write(UpdateProgress1.ClientID); %>').style.display = "inline";
    }
    </script>
</asp:Content>
