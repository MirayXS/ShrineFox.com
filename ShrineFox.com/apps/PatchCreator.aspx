<%@ Page Title="Patch Creator" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PatchCreator.aspx.cs" Inherits="ShrineFoxCom.PatchCreator" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="Sidebar" runat="server"></asp:PlaceHolder>
    <b><a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true""></i> ShrineFox.com</a> <i class="fa fa-angle-right" aria-hidden="true"></i> Apps <i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %></b>
    <h1><%: Page.Title %></h1>
    Generate a <b>patch.yml</b> to use for modding Persona 5 (PS3). <a href="https://shrinefox.com/guides/2019/04/19/persona-5-rpcs3-modding-guide-1-downloads-and-setup/">Read more</a>
    <br>Automatically removes conflicting and unwanted patches so you only download what you need.
    <br>
    <br>
    <asp:PlaceHolder ID="lastUpdated" runat="server"></asp:PlaceHolder>
    <br><br>
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
            <div class="card-title h5">PPU Hash</div>
            <div class="card-subtitle text-gray">Provide the correct hash or this won't work!</div>
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
    <div>
        <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <!--Notice-->
                <asp:PlaceHolder ID="NoticePlaceHolder2" runat="server"/>
                <!--Patch Selection & Details-->
                <div class="card">
                    <div class="card-header">
                        <div class="card-title h5" id="patchTitle" runat="server">Select A Patch</div>
                        <div class="card-subtitle text-gray" id="patchInfo" runat="server">Learn about a patch's functionality & toggle it</div>
                        <div class="card-body" id="patchNotes" runat="server"></div>
                    </div>
                    <div class="card-footer" style="font-size:16pt;">
                        <div class="columns">
                            <div class="column col-6">
                                <asp:DropDownList id="patchList" class="form-select" AutoPostBack="True" OnSelectedIndexChanged="Selection_Changed" runat="server"/>
                            </div>
                            <div class="column col-6">
                                <asp:LinkButton id="enable" enabled="false" runat="server" OnClick="Enable_Click" OnClientCheckedChanged="ShowProgress();"><i class="fas fa-check-square"></i> Enable This Patch</asp:LinkButton>
                            </div>
                            <br>
                            <br>
                            <div class="column col-2">
                                <asp:Button class="btn btn-primary" ID="btnEnableAll" runat="server" Text="Enable All" OnClick="EnableAll_Click" />
                            </div>
                            <div class="column col-2">
                                <asp:Button class="btn btn-primary" ID="btnDisableAll" runat="server" Text="Disable All" OnClick="DisableAll_Click" />
                            </div>
                        </div>
                        <label class="form-checkbox float-right">
                            
                        </label>
                    </div>
                </div>
                <!--Download Button-->
                <div class="card">
                    <div class="card-header">
                        <div class="card-title h5">Downloads</div>
                        <div class="card-subtitle text-gray" id="appliedPatches" runat="server"></div>
                        <div class="card-body">
                            <b>New Format</b>: Works with RPCS3's new Patch Manager. Place downloaded <kbd>patch.yml</kbd> in your <code>RPCS3/Patches</code> folder and go to <code>Manage > Game Patches</code>.
                            <br><br><b>Old Format</b>: Can use to patch <kbd>eboot.bin</kbd> to use patches on PS3 with custom firmware. <a href="https://shrinefox.com/guides/2019/06/12/persona-5-ps3-eboot-patching/">Read more</a>
                            <!--Notice-->
                            <br><asp:PlaceHolder ID="NoticePlaceHolder" runat="server"/>
                            <br>
                        </div>
                    </div>
                    <div class="card-footer">
                        <div class="dropdown dropdown-right float-right" runat="server" id="downloadBtn"><a class="btn btn-primary dropdown-toggle" tabindex="0">Download patch.yml <i class="icon icon-caret"></i></a>
                            <ul class="menu text-left">
                                <li class="menu-item"><asp:LinkButton runat="server" id="newFormat" OnClick="Download_Click" OnClientCheckedChanged="ShowProgress();">New Format</asp:LinkButton></li>
                                <li class="menu-item"><asp:LinkButton runat="server" id="oldFormat" OnClick="Download_Click" OnClientCheckedChanged="ShowProgress();">Old Format</asp:LinkButton></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="patchList" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="enable" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="newFormat" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="oldFormat" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnEnableAll" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnDisableAll" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
    function ShowProgress()
    {
        document.getElementById('<% Response.Write(UpdateProgress1.ClientID); %>').style.display = "inline";
    }
    </script>
</asp:Content>
