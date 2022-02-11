<%@ Page Title="PNACH Creator" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="PNACHCreator.aspx.cs" Inherits="ShrineFoxCom.PNACHCreator" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="navipath">
		<a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true"></i> ShrineFox.com</a> 
		<i class="fa fa-angle-right" aria-hidden="true"></i> <a href="https://shrinefox.com/WebApps">Apps</a> 
        <i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %>
	</div>
	<h1><%: Page.Title %></h1>
    Generate a <b>.pnach</b> to use for modding and applying cheats to PS2 games. Just put it in your PCSX2 pnach folder and enable cheats.
    <br><div class="notice red">This section is still under construction, so I apologize if it isn't currently working properly.
        <br>Please check back another time!</div>
    <br>
    <asp:PlaceHolder ID="lastUpdated" runat="server"></asp:PlaceHolder>
    <br>
    <br>
    <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel">
        <ProgressTemplate>
            <div class="modal active modal-sm">
                <span class="modal-overlay"></span>
                <div class="loading loading-lg"></div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <!--CRC Entry-->
    <div class="card">
        <div class="card-header">
            <div class="card-title h5">1. Select Game & Enter CRC</div>
            <div class="card-subtitle text-muted">Provide the correct CRC or this won't work!</div>
        </div>
        <div class="card-footer">
            <div class="columns">
                <div class="column col-5 text-center">
                    <asp:DropDownList id="gameList" class="form-select" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="SelectGame_Changed"/>
                    <br>
                    <asp:TextBox ID="txtBox_CRC" class="form-input" runat="server" Text="94A82AAA"></asp:TextBox>
                </div>
            </div>
            <br><br>Find your CRC by running the game in PCSX2 and checking <a href="https://i.imgur.com/5b2yURr.png">the title of the Console window</a>.
        </div>
    </div>
    <!--Patch Selection-->
    <div class="card">
        <div class="card-header">
            <div class="card-title h5">2. Select & Toggle Patches to Include</div>
            <div class="card-subtitle text-muted">Choose a patch to toggle or learn more about.</div>
        </div>
        <div class="card-footer">
            <div class="columns">
                <div class="column col-7">
                    <asp:DropDownList id="patchList" class="form-select" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="SelectPatch_Changed"/>
                </div>
            </div>
            <div class="columns">
                <div class="column col-5">
                    <asp:LinkButton ID="btnEnableAll" runat="server" Text="Include All" OnClick="EnableAll_Click"/>
                </div>
                <div class="column col-5">
                    <asp:LinkButton ID="btnDisableAll" runat="server" Text="Remove All" OnClick="DisableAll_Click"/>
                </div>
            </div>
        </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <!--Compatibility Notice-->
                <asp:PlaceHolder ID="NoticePlaceHolder2" runat="server"/>
                <div class="card">
                    <!--Selected Patch Info-->
                    <div class="card-header">
                        <div class="card-title h5" id="patchTitle" runat="server"></div>
                        <div class="card-subtitle text-muted" id="patchInfo" runat="server"></div>
                        <div class="card-body" id="patchNotes" runat="server"></div>
                    </div>
                    <div class="card-footer" style="font-size:16pt;">
                        <label class="float-right">
                            <asp:LinkButton id="enable" enabled="false" runat="server" OnClick="Enable_Click" OnClientCheckedChanged="ShowProgress();"><i class="fas fa-check-square"></i> Include This Patch</asp:LinkButton>
                        </label>
                    </div>
                </div>
                <!--Download Info-->
                <div class="card">
                    <div class="card-header">
                        <div class="card-title h5">3. Confirm & Download</div>
                        <div class="card-subtitle text-muted" id="appliedPatches" runat="server"></div>
                        <div class="card-body">
                            <!--Notice-->
                            <br><asp:PlaceHolder ID="NoticePlaceHolder" runat="server"/>
                            <br>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gameList" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="patchList" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnEnableAll" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnDisableAll" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="enable" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <!--Download Button-->
    <div class="card">
        <div class="card-footer">
            <asp:LinkButton class="btn btn-primary float-right" runat="server" OnClick="Download_Click">
                <i class="fa fa-download"></i> Download .pnach
            </asp:LinkButton>
        </div>
    </div>

    <script type="text/javascript">
    function ShowProgress()
    {
        document.getElementById('<% Response.Write(UpdateProgress1.ClientID); %>').style.display = "inline";
    }
    </script>
</asp:Content>
