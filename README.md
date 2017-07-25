# djkrose's Scripting Mod
Adds scripting support and other useful functionality to 7 Days To Die dedicated server.

<!-- [Download](#) | [Documentation](#) | [FAQ](#) |  [Discord Channel](https://discord.gg/y26jNDz) -->

## Scripting Examples

<table>
<tr></tr>

<tr>
<th>Lua</th>
<th>JavaScript</th>
</tr><tr><!-- start with gray backgrund -->

<td>
1. Create file <i>example.lua</i>

```lua
-- @commands    mylua
-- @description My first Lua command
-- @help        This example shows how easy it is
--              to add a new command in Lua script
print "Hello Lua World!"
```

2\. Drop file into *\\Mods\\ScriptingMod\\scripts* folder<br>
3\. Execute commands: `help mylua` and `mylua`<br>
![Example mylua](/Documentation/img/example-mylua.png?raw=true)<br>
4\. Profit!
</td>

<td>
1. Create file <i>example.js</i>

```javascript
// @commands    myjavascript myjs
// @description My first JavaScript command
// @help        This example shows how easy it is
//              to add a new command in JavaScript
console.log("Hello JavaScript World!");
```

2\. Drop file into *\\Mods\\ScriptingMod\\scripts* folder<br>
3\. Execute commands: `help myjs`and `myjs`<br>
![Example mylua](/Documentation/img/example-myjs.png?raw=true)<br>
4\. Profit!
</td>

</tr>
</table>

#### Why Scripting?

* Quickly and easily modify commands to your own preference; changes are live instantly without server restart
* Pick and choose commands from various online sources; write and share single command snippets with others
* Implement new commands from simple to complex individual features; no need for third-party bots
* Full OS access, full file access, full network access: use web requests (REST/JSON), any database, send emails, ...
* Two of the most flexible and easy to learn  programming languages with lots of resources online
* Full access to .Net Framework, all Unity objects, and all 7DTD  game data
* Full support of 7DTD command permission system: assign access levels to your commands just like any other

## Built-in Commands

* Import/export prefabs <b>with container content, ownership, lock status, sign texts</b>, and all other metadata.<br>
  &ndash;&nbsp; Fully restore griefed bases with one command!<br>
  &ndash;&nbsp; Copy buildings from server to server or between server versions after world reset.

## Download
[coming in a few days]

## Compatibility

 * **Dedicated Server** of [7 Days to Die](http://store.steampowered.com/app/251570/7_Days_to_Die/). The mod is not meant to be used with the desktop client.
 * Current version is compatible with **Alpha 16.1 (b1)** only.
 * Developed and tested on **Windows**. Linux may or may not work; reports are welcome!
 * **No dependencies**: No other mods or other software is required.
 * Successfully tested together with these great mods:
   * [Alloc's server fixes](https://7dtd.illy.bz/wiki/Server%20fixes)
   * [Coppis Additions](https://7daystodie.com/forums/showthread.php?44835-Coppi-MOD-New-features)
   * [StompiNZ's Bad Company Manager](https://7daystodie.com/forums/showthread.php?57569)

## Documentation
[coming in a few days]

## Source Code
During this early phase of development the source code is not yet publicly available. The GitHub repository is just used for issue tracking and documentation. When the mod has reached stable status (version 1.0) the source code will be published and others are invited to contribute to it or use parts in their own mods.

## Support
Nice people like me might be able to help here:
 - [djkrose's Discord: #scriptingmod](https://discord.gg/y26jNDz)
 - [Official 7DTD Discord](https://discord.gg/taYNEUS)

Just remember: This software is provided free of charge, as is, without any guarantee or support. You are not *entitled* to support, nor is anyone liable for data loss. If it doesn't work for you, that is just bad luck. If data is lost, you should've made a backup.

The ScriptingMod is not affiliated with the official [7DTD game](http://store.steampowered.com/app/251570/7_Days_to_Die/) or with [The Fun Pimps](http://thefunpimps.com/) in any way.

## License

[![Creative Commons License](https://i.creativecommons.org/l/by-nc/4.0/88x31.png)](http://creativecommons.org/licenses/by-nc/4.0/) djkrose's Scripting Mod is licensed under a [CC BY-NC 4.0](http://creativecommons.org/licenses/by-nc/4.0/). That means, you are free to use the mod and the source code (once published) for non-commercial purposes; just leave the credit notes intact.

Of course, *The Fun Pimps* themselves are free to take over any part of the code into their awesome game!