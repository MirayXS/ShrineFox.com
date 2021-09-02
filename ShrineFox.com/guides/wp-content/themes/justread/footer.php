                        </section>
                    </section>
                </section>
            </td>
        </tr>
    </table>
    <!--Footer-->
    <footer>
        <table>
            <tbody>
                <tr>
                    <td id="social_links">
                        <a href="https://twitter.com/AmicitiaTeam"><i class="fab fa-twitter"></i> AmicitiaTeam</a>
                        <br><a href="https://reddit.com/r/Amicitia"><i class="fab fa-reddit"></i> r/Amicitia</a>
                        <br><a href="https://discord.gg/9USHGmB"><i class="fab fa-discord"></i> Discord</a>
                    </td>
                    <td id="site_disclaimer">
                        We are NOT affiliated, associated, authorized, endorsed by, or in any way
                        officially connected with ATLUS, SEGA, or any of its subsidiaries or its affiliates.
                        The official ATLUS website can be found at <a href="https://atlus.com/">https://atlus.com</a>. ATLUS and SEGA are
                        registered in the U.S. Patent and Trademark Office. ATLUS, SHIN MEGAMI TENSEI, and PERSONA are
                        either registered trademarks or trademarks of ATLUS Co., Ltd. or its affiliates.
                        <!--Theme Select-->
                        <br><br>Theme: 
                            <select id="theme" name="theme" class="form-select" onchange="ThemeToggle()">
                                <option value="" selected>Default</option>
                                <option value="Blue">Blue</option>
                                <option value="Blue Dark">Blue Dark</option>
                                <option value="Red">Red</option>
                                <option value="Red Dark">Red Dark</option>
                                <option value="Green">Green</option>
                                <option value="Green Dark">Green Dark</option>
                                <option value="Yellow">Yellow</option>
                                <option value="Berry">Berry</option>
                                <option value="Custom">Custom</option>
                            </select>
                        <!--Color Picker-->
                        <script>
                            jscolor.presets.default = {
                                format: 'rgb', previewSize: 20, paletteCols: 1,
                                backgroundColor: 'rgb(var(--post))', borderColor: 'rgb(var(--post))',
                                padding: 5, width: 100, height: 100, mode: 'HVS',
                                controlBorderColor: 'rgb(var(--post))', sliderSize: 8, shadow: false
                            };
                        </script>
                        <div id="colorpicker" style="display:none;">
                            <br><button id="customtext" data-jscolor="{onChange: 'updateText(this)',value:'rgb(var(--text))', alpha:1}">Text</button>
                            <br><button id="customlink" data-jscolor="{onChange: 'updateLink(this)',value:'rgb(var(--link))', alpha:1}">Link</button>
                            <br><button id="customwaves" data-jscolor="{onChange: 'updateWaves(this)',value:'rgb(var(--waves))', alpha:1}">Waves</button>
                            <br><button id="customwaves2" data-jscolor="{onChange: 'updateWaves2(this)',value:'rgb(var(--waves2))', alpha:1}">Waves 2</button>
                            <br><button id="customsearchtext" data-jscolor="{onChange: 'updateSearchText(this)',value:'rgb(var(--searchtext))', alpha:1}">Search Text</button>
                            <br><button id="custombg" data-jscolor="{onChange: 'updateBg(this)',value:'rgb(var(--bg))', alpha:1}">Post BG</button>
                        </div>
                    </td>
            </tbody>
        </table>
        <br>
        <br>
        <center>
            <div class="ad">
                <!--AdSense-->
                <script async="" src="https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
                <ins class="adsbygoogle" style="display:block" data-ad-format="fluid" data-ad-layout-key="-hx+4+s-4m+70" data-ad-client="ca-pub-9519592525056753" data-ad-slot="8263745709"></ins>
                <script>(adsbygoogle = window.adsbygoogle || []).push({});</script>
            </div>
            <!--Credits-->
            <br>ShrineFox 2020 - 2021. <a href="https://ko-fi.com/shrinefox"><i class="fa fa-coffee"></i> Support</a> | <a href="https://trello.com/b/mXTXRmpR/shrinefox-to-do">Track Progress</a>
            <br>
        </center>
        <!--End Footer-->
    </footer>
</div>