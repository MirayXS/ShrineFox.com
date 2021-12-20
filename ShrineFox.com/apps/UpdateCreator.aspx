<%@ Page Title="PS4 Update Creator" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateCreator.aspx.cs" Inherits="ShrineFox.com.UpdateCreator" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <asp:PlaceHolder ID="Sidebar" runat="server"></asp:PlaceHolder>
        <b><a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true""></i> ShrineFox.com</a> <i class="fa fa-angle-right" aria-hidden="true"></i> Apps <i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %></b>
        <h1><%: Page.Title %></h1>
        <p>
            Generate a PS4 Game Update with your choice of patches. <a href="https://shrinefox.com/guides/2020/09/30/modding-persona-5-royal-jp-on-ps4-fw-6-72/">Read more</a>
            <br>Powered by <a href="https://github.com/zarroboogs/ppp">Lipsum's ps4 persona patches (ppp)</a>.
        </p>
        <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div>
                <asp:UpdatePanel ID="UpdatePanel_GameTabs" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <!--Game & Region Select-->
                        <ul class="tab tab-block">
                            <li class="tab-item" id="p5rtab" runat="server"><asp:LinkButton id="p5r" runat="server" OnClick="GameTab_Click">P5R</asp:LinkButton></li>
                            <li class="tab-item" id="p3dtab" runat="server"><asp:LinkButton id="p3d" runat="server" OnClick="GameTab_Click" class="badge" data-badge="new">P3D</asp:LinkButton></li>
                            <li class="tab-item" id="p4dtab" runat="server"><asp:LinkButton id="p4d" runat="server" OnClick="GameTab_Click" class="badge" data-badge="new">P4D</asp:LinkButton></li>
                            <li class="tab-item" id="p5dtab" runat="server"><asp:LinkButton id="p5d" runat="server" OnClick="GameTab_Click" class="badge" data-badge="new">P5D</asp:LinkButton></li>
                            <li class="tab-item tab-action">
                                <div class="input-group input-inline">
                                    <label class="form-radio">
                                        <input type="radio" name="region" id="usa" runat="server"><i class="form-icon"></i> USA
                                    </label>
                                    <label class="form-radio">
                                        <input type="radio" name="region" id="eur" runat="server"><i class="form-icon"></i> EUR
                                    </label>
                                </div>
                            </li>
                        </ul>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="p5r" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="p3d" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="p4d" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="p5d" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel_PatchTabs" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <!--Patch Select-->
                        <ul class="tab">
                            <li class="tab-item" id="mod_support_tab" runat="server"><a id="mod_support" runat="server">Mod Support</a></li>
                            <li class="tab-item" id="mod_support2_tab" runat="server"><a id="mod_support2" runat="server">Mod Support (Alt)</a></li>
                            <li class="tab-item" id="_0505_tab" runat="server"><a id="_0505" runat="server">5.05 Backport</a></li>
                            <li class="tab-item" id="intro_skip_tab" runat="server"><a id="intro_skip" runat="server">Intro Skip</a></li>
                            <li class="tab-item" id="all_dlc_tab" runat="server"><a id="all_dlc" runat="server">Content Enabler</a></li>
                            <li class="tab-item" id="dlc_msg_tab" runat="server"><a id="dlc_msg" runat="server" class="badge" data-badge="new">DLC Msg Skip</a></li>
                            <li class="tab-item" id="no_trp_tab" runat="server"><a id="no_trp" runat="server">Disable Trophies</a></li>
                            <li class="tab-item" id="square_tab" runat="server"><a id="square" runat="server">Square Menu</a></li>
                            <li class="tab-item" id="p5_save_tab" runat="server"><a id="p5_save" runat="server">Save Bonus</a></li>
                            <li class="tab-item" id="env_tab" runat="server"><a id="env" runat="server">ENV Test</a></li>
                            <li class="tab-item" id="zzz_tab" runat="server"><a id="zzz" runat="server">Misc Tests</a></li>
                        </ul>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel_Description" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <!--Selected Patch Info & Toggle-->
                        <div class="card">
                            <div class="card-image"><img class="img-responsive" id="image" runat="server" src="https://66.media.tumblr.com/c3f99e21c7edb1df53e7f2fa02117621/tumblr_inline_pl680q6yWy1rp7sxh_500.gif"></div>
                            <div class="card-header">
                                <div class="card-title h5" id="patch_name" runat="server">Mod Support</div>
                                <div class="card-subtitle text-gray" id="description" runat="server">mod.cpk file replacement</div>
                                <div class="card-body" id="description_long" runat="server">Loads modded from a mod.cpk file placed in /data/p5r.</div>
                            </div>
                            <div class="card-footer">
                                <label class="form-checkbox float-right">
                                    <input type="checkbox" name="enable" id="enable" runat="server"><i class="form-icon"></i> Enable
                                </label>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
</asp:Content>
