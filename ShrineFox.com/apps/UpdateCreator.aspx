<%@ Page Title="PS4 Update Creator" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateCreator.aspx.cs" Inherits="ShrineFox.com.UpdateCreator" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <asp:PlaceHolder ID="Sidebar" runat="server"></asp:PlaceHolder>
        <b><a href="https://shrinefox.com/"><i class="fa fa-home" aria-hidden="true""></i> ShrineFox.com</a> <i class="fa fa-angle-right" aria-hidden="true"></i> Apps <i class="fa fa-angle-right" aria-hidden="true"></i> <%: Page.Title %></b>
        <h1><%: Page.Title %></h1>
        <div class="notices blue">
            <p>
                Generate a <b>v1.02 EBOOT</b> with your choice of patches.
                <br>Can be used to create a customized PS4 update PKG. <a href="https://shrinefox.com/guides/2020/09/30/modding-persona-5-royal-jp-on-ps4-fw-6-72/">Read more</a>.
                <br>
                <br>Once installed, you can use FTP to install mods instead of building a new PKG every time.
                <br>Based on <a href="https://github.com/zarroboogs/ppp">Lipsum's ps4 persona patches</a>.
            </p>
        </div>
        <table>
            <tr>
                <td style="vertical-align: top;">
                    <h2>Default Patches</h2>
                    <b>Mod Support Alt (mod_support2)</b> by Lipsum
                    <br><span style="font-size: 15px;">
	                    File replacement via a mod.cpk file (placed in /data/p5r/).
                    </span>
                    <br><b>PS4 FW 5.05 Backport (0505)</b> by Lipsum
                    <br><span style="font-size: 15px;">
	                    Makes 5.05 the minimum system firmware required to run the game.
                    </span>
                    <br><b>P5 Save Bonus Enabler (P5_save)</b> by Lipsum
                    <br><span style="font-size: 15px;">
	                    Enables P5 save bonus even without P5 saves present on system.
                    </span>
                    <br><b>Global Square Menu (square)</b> by Lipsum
                    <br><span style="font-size: 15px;">
	                    Enables the square menu globally (e.g. in Velvet Room or during events or game sections which disable it).
	                    <br>This can be useful for the <a href="https://github.com/Amicitia/Persona-5-Royal-Mod-Menu/releases">P5R Mod Menu</a>.
                    </span>
                    <h2>Optional Patches</h2>
                    <b>Intro Skip (intro_skip)</b> by Lipsum<br><span style="font-size: 15px;">
	                    Skips boot logos and intro movie.
                    </span>
                    <br><b>Content Enabler (all_dlc)</b> by Lipsum<br><span style="font-size: 15px;">
	                    Enables on-disc content.
                    </span>
                    <br><b>Disable Trophies (no_trp)</b> by Lipsum<br><span style="font-size: 15px;">
	                    Prevents the game from unlocking trophies.
                    </span>
                    <br><b>ENV Tests (env)</b> by Lipsum<br><span style="font-size: 15px;">
	                    Maps all env/env*.ENV to env/env0000_000_000.ENV.
                    </span>
                </td>
                <td style="vertical-align: top;">
                    <h2>Game Version</h2>
                    <asp:Panel ID="radioButtonsContainer1" runat="server">
                        <asp:RadioButton id="radio_CUSA17416" Text="Persona 5 Royal (USA)" ToolTip="CUSA17416" Checked="True" GroupName="RadioGroup1" runat="server" />
                        <br><asp:RadioButton id="radio_CUSA17419" Text="Persona 5 Royal (EUR)" ToolTip="CUSA17419" GroupName="RadioGroup1" runat="server"/>
                        <br><asp:RadioButton id="radio_CUSA06638" Enabled="False" Text="Persona 5 PS4 (EUR) (Coming Soon)" ToolTip="CUSA06638" GroupName="RadioGroup1" runat="server"/>
                    </asp:Panel>
                    <h2>Patch Set</h2>
                    <div class="notices yellow">
                        <p>
                            I couldn't support all possible combinations due to the amoung of storage it'd take, 
                            so choose a preset combination below or <a href="https://github.com/zarroboogs/ppp#readme">
                                patch your EBOOT yourself</a>.
                        </p>
                    </div>
                    <asp:Panel ID="radioButtonsContainer2" runat="server">
                    <br><asp:RadioButton id="radio_basic" Text="<b>Basic</b><br>mod_support2, 0505, P5_save, square
                        <br><span style='font-size: 15px;'>Only stuff that benefits you without impacting regular gameplay, 
                            <br>affecting your save, or requiring any custom files. You may still not have Square Menu 
                                functionality until your cellphone is unlocked normally ingame.</span>" 
                                    ToolTip="Royal//mod_support2//0505//square//p5_save//" Checked="True" GroupName="RadioGroup2" runat="server" />
                    <br><asp:RadioButton id="radio_extra" Text="<b>Extra</b><br>mod_support2, intro_skip, 0505, P5_save, square
                        <br><span style='font-size: 15px;'>Same as above, but skips the opening logos/movie goes straight to title screen.
                            <br>Recommended for the fastest possible bootup.</span>" 
                                ToolTip="Royal//mod_support2//intro_skip//0505//square//p5_save//" Checked="False" GroupName="RadioGroup2" runat="server" />
                    <div class="notices red">
                        <p>
                            The following patch <b>crashes the game</b> if <kbd>env/env0000_000_000.ENV</kbd> is not present in <kbd>mod.cpk</kbd>.
                            <br>It's recommended to use with the Title Screen Skip mod for instant ENV testing.
                        </p>
                    </div>
                    <br><asp:RadioButton id="radio_extra_evt" Text="<b>Extra (+ ENV Tester)</b><br>mod_support2, intro_skip, 0505, P5_save, square, env
                        <br><span style='font-size: 15px;'>Same as above, but always loads custom file env/env0000_000_000.ENV.</span>" 
                                ToolTip="Royal//mod_support2//intro_skip//0505//square//p5_save//env//" Checked="False" GroupName="RadioGroup2" runat="server" />
                    <div class="notices red">
                        <p>
                            The following patches <b>will make your saves incompatible</b> should you stop using them in the future.
                        </p>
                    </div>
                    <asp:RadioButton id="radio_baller" Text="<b>Unlocker</b><br>mod_support2, intro_skip, 0505, square, all_dlc, p5_save
                        <br><span style='font-size: 15px;'>Same as <b>Extra</b>, but unlocks on-disc content.</span>" 
                                ToolTip="Royal//mod_support2//intro_skip//0505//square//all_dlc//p5_save//" Checked="False" GroupName="RadioGroup2" runat="server" />
                    <br><asp:RadioButton id="radio_epic" Text="<b>Undisturbed</b><br>mod_support2, intro_skip, 0505, square, all_dlc, no_trp, p5_save
                        <br><span style='font-size: 15px;'>Same as <b>Unlocker</b>, but disables trophies. If that's what you're into.</span>" 
                            ToolTip="Royal//mod_support2//intro_skip//0505//square//all_dlc//no_trp//p5_save//" Checked="False" GroupName="RadioGroup2" runat="server" />
                    </asp:Panel>
                    <br>
                    <br><asp:Button class="btn btn-primary" ID="btnDownload" runat="server" Text="Download Mod" OnClick="Download_Click" />
                    <br>
                    <br>Not seeing a combination you'd like to have premade? <a href="https://shrinefox.com/forum/posting.php?mode=post&f=3">Ask on the forum</a>!
                </td>
            </tr>
        </table>
        <br>
        <asp:PlaceHolder ID="placeHolder" runat="server"></asp:PlaceHolder>
</asp:Content>
