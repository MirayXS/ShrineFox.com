<%@ Page Title="PS4 Update Creator" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateCreator.aspx.cs" Inherits="ShrineFox.com.UpdateCreator" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <asp:PlaceHolder ID="Sidebar" runat="server"></asp:PlaceHolder>
        <b><a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true""></i> ShrineFox.com</a> <i class="fa fa-angle-right" aria-hidden="true"></i> Apps <i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %></b>
        <h1><%: Page.Title %></h1>
        <p>
            Generate a PS4 Game Update with your choice of patches.
            <br>Only works with games installed via FPKG! <a href="https://shrinefox.com/guides/2020/09/30/modding-persona-5-royal-jp-on-ps4-fw-6-72/">Read more</a>
            <br>Powered by <a href="https://github.com/zarroboogs/ppp">Lipsum's ps4 persona patches (ppp)</a>.
        </p>
        <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel">
            <ProgressTemplate>
                <div class="modal active modal-sm">
                    <span class="modal-overlay"></span>
                    <div class="loading loading-lg"></div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
            <div>
                <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <!--Game & Region Select-->
                        <ul class="tab tab-block">
                            <li class="tab-item active" id="p5rtab" runat="server"><asp:LinkButton id="p5r" runat="server" OnClick="GameTab_Click" OnClientClick="ShowProgress();">P5R</asp:LinkButton></li>
                            <li class="tab-item" id="p3dtab" runat="server"><asp:LinkButton id="p3d" runat="server" OnClick="GameTab_Click" OnClientClick="ShowProgress();" class="badge" data-badge="new">P3D</asp:LinkButton></li>
                            <li class="tab-item" id="p4dtab" runat="server"><asp:LinkButton id="p4d" runat="server" OnClick="GameTab_Click" OnClientClick="ShowProgress();" class="badge" data-badge="new">P4D</asp:LinkButton></li>
                            <li class="tab-item" id="p5dtab" runat="server"><asp:LinkButton id="p5d" runat="server" OnClick="GameTab_Click" OnClientClick="ShowProgress();" class="badge" data-badge="new">P5D</asp:LinkButton></li>
                            <li class="tab-item tab-action">
                                <div class="input-group input-inline">
                                    <label class="form-radio">
                                        <asp:RadioButton GroupName="region" id="usa" Text="USA" runat="server" AutoPostBack="true" OnCheckedChanged="Radio_CheckedChanged" OnClientCheckedChanged="ShowProgress();" Checked="true"/>
                                    </label>
                                    <label class="form-radio">
                                        <asp:RadioButton GroupName="region" id="eur" Text="EUR" AutoPostBack="true" OnCheckedChanged="Radio_CheckedChanged" OnClientCheckedChanged="ShowProgress();" runat="server"/>
                                    </label>
                                </div>
                            </li>
                        </ul>
                        <!--Patch Select-->
                        <ul class="tab">
                            <li class="tab-item active" id="mod_support_tab" runat="server"><asp:LinkButton id="mod_support" runat="server" OnClick="PatchTab_Click" OnClientClick="ShowProgress();">Mod Support</asp:LinkButton></li>
                            <li class="tab-item" id="mod_support2_tab" runat="server"><asp:LinkButton id="mod_support2" runat="server" OnClick="PatchTab_Click" OnClientClick="ShowProgress();">Mod Support (Alt)</asp:LinkButton></li>
                            <li class="tab-item" id="_0505_tab" runat="server"><asp:LinkButton id="_0505" runat="server" OnClick="PatchTab_Click" OnClientClick="ShowProgress();">5.05 Backport</asp:LinkButton></li>
                            <li class="tab-item" id="intro_skip_tab" runat="server"><asp:LinkButton id="intro_skip" runat="server" OnClick="PatchTab_Click" OnClientClick="ShowProgress();">Intro Skip</asp:LinkButton></li>
                            <li class="tab-item" id="all_dlc_tab" runat="server"><asp:LinkButton id="all_dlc" runat="server" OnClick="PatchTab_Click" OnClientClick="ShowProgress();">Content Enabler</asp:LinkButton></li>
                            <li class="tab-item" id="dlc_msg_tab" runat="server"><asp:LinkButton id="dlc_msg" runat="server" OnClick="PatchTab_Click" OnClientClick="ShowProgress();">DLC Msg Skip</asp:LinkButton></li>
                            <li class="tab-item" id="no_trp_tab" runat="server"><asp:LinkButton id="no_trp" runat="server" OnClick="PatchTab_Click" OnClientClick="ShowProgress();">Disable Trophies</asp:LinkButton></li>
                            <li class="tab-item" id="square_tab" runat="server"><asp:LinkButton id="square" runat="server" OnClick="PatchTab_Click" OnClientClick="ShowProgress();">Square Menu</asp:LinkButton></li>
                            <li class="tab-item" id="p5_save_tab" runat="server"><asp:LinkButton id="p5_save" runat="server" OnClick="PatchTab_Click" OnClientClick="ShowProgress();">Save Bonus</asp:LinkButton></li>
                            <li class="tab-item" id="env_tab" runat="server"><asp:LinkButton id="env" runat="server" OnClick="PatchTab_Click" OnClientClick="ShowProgress();">ENV Test</asp:LinkButton></li>
                            <li class="tab-item" id="zzz_tab" runat="server"><asp:LinkButton id="zzz" runat="server" OnClick="PatchTab_Click" OnClientClick="ShowProgress();">Misc Tests</asp:LinkButton></li>
                            <li class="tab-item d-none" id="overlay_tab" runat="server"><asp:LinkButton id="overlay" runat="server" OnClick="PatchTab_Click" OnClientClick="ShowProgress();">Disable Overlay</asp:LinkButton></li>
                        </ul>
                        <!--Selected Patch Info & Toggle-->
                        <div class="card">
                            <div class="card-image"><img class="img-responsive" id="image" runat="server" src="https://66.media.tumblr.com/c3f99e21c7edb1df53e7f2fa02117621/tumblr_inline_pl680q6yWy1rp7sxh_500.gif"></div>
                            <div class="card-header">
                                <div class="card-title h5" id="patch_name" runat="server">Mod Support</div>
                                <div class="card-subtitle text-gray" id="description" runat="server">mod.cpk file replacement via PKG</div>
                                <div class="card-body" id="description_long" runat="server">
                                    Loads modded files from a <kbd>mod.cpk</kbd> file in the PKG's <code>USRDIR</code> directory.
                                    <br>Only useful if you're downloading the patched eboot.bin and creating the PKG yourself.
                                </div>
                            </div>
                            <div class="card-footer" style="font-size:16pt;">
                                <label class="form-checkbox float-right">
                                    <asp:CheckBox OnCheckedChanged="Patch_CheckedChanged" OnClientCheckedChanged="ShowProgress();" id="enable" runat="server" AutoPostBack="true" Text="Enable This Patch"/>
                                </label>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="p5r" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="p3d" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="p4d" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="p5d" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="usa" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="eur" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="mod_support" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="mod_support2" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="_0505" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="intro_skip" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="all_dlc" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="dlc_msg" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="no_trp" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="square" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="p5_save" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="env" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="zzz" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="overlay" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="enable" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <!--Download Button-->
                <div class="card">
                    <div class="card-header">
                        <div class="card-title h5" id="titleID" runat="server">Patches Applied to <b>CUSA17416</b> (Persona 5 Royal, USA)</div>
                        <div class="card-subtitle text-gray" id="appliedPatches" runat="server">Mod Support (Alt), 5.05 Backport, DLC Msg Skip, Square Menu</div>
                        <div class="card-body" id="dlPath" runat="server">
                            p5r/CUSA17416/mod_support/_0505/intro_skip/dlc_msg/square/eboot.bin
                        </div>
                    </div>
                    <div class="card-footer">
                        <label class="form-checkbox float-right">
                            <div class="dropdown dropdown-right"><a class="btn btn-primary dropdown-toggle" tabindex="0">Download As... <i class="icon icon-caret"></i></a>
                                <ul class="menu text-left">
                                    <li class="menu-item"><asp:LinkButton id="pkg" runat="server" OnClick="PKG_Click">PKG</asp:LinkButton></li>
                                    <li class="menu-item"><asp:LinkButton id="eboot" runat="server" OnClick="EBOOT_Click">eboot.bin</asp:LinkButton></li>
                                </ul>
                            </div>
                        </label>
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
