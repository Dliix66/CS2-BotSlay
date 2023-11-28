# CS2-BotSlay
CSSharp plugin to slay all bots when the last player dies

- Requires CSSharp and metamod installed on your server
- Download the latest release from the [releases page](https://github.com/Dliix66/CS2-BotSlay/releases)
- Extract the zip file to your `csgo` folder
- Execute `css_plugins list` on your server to check the install
- You might have to `css_plugins load BotSlay` on the first install (or restart the server)

# Known issue:
- CSSharp has an issue causing some event not to be triggered until a hot-reload has been done
  > Run `css_plugins reload BotSlay` to fix the issue
