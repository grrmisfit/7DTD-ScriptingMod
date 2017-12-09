﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using ScriptingMod.Extensions;
using ScriptingMod.ScriptEngines;
using ScriptingMod.Tools;

namespace ScriptingMod
{

    /// <summary>
    /// Main API mod interface for 7DTD. All public objects deriving from ModApiAbstract are automatically loaded.
    /// All overridden methods are called withing a try-catch already; no need for adding top-level try-catch ourselves.
    /// </summary>
    [UsedImplicitly]
    public class Api : ModApiAbstract
    {
        public Api()
        {
            Log.Debug("Api constructor called.");
#if DEBUG
            Log.Warning("ScriptingMod is compiled in DEBUG mode! This can hurt server performance significantly and will fill the logfile fast with useless information.");
#endif
            Log.Out("Initializing phase 1/3 ...");
            NonPublic.Init();
            PersistentData.Load();
            PatchTools.ApplyPatches();
            LitJsonTypeBindings.Register();
        }

        /// <summary>
        /// Called during game start on every mod, before the World is ready (GameManager.Instance.World == null)
        /// </summary>
        public override void GameAwake()
        {
            Log.Debug("Api.GameAwake called.");
            Log.Out("Initializing phase 2/3 ...");
            CommandTools.InitScripts();
            CommandTools.InitScriptsMonitoring();

            CommandTools.InvokeScriptEvents(ScriptEvent.gameAwake, () => new ScriptEventArgs());
        }

        /// <summary>
        /// Called when the game started and all objects are ready (e.g. GameManager.Instance.World)
        /// </summary>
        public override void GameStartDone()
        {
            Log.Debug("Api.GameStartDone called.");
            Log.Out("Initializing phase 3/3 ...");
            EacTools.Init();
            CommandTools.InitEvents();
            RepairEngine.InitAuto();
            TelemetryTools.Init();
            Log.Out($"Done initializing {Constants.ModName}.");

            CommandTools.InvokeScriptEvents(ScriptEvent.gameStartDone, () => new ScriptEventArgs());
        }

        /// <summary>
        /// Called on every game tick. Doing ANYTHING here has a big performance impact!
        /// </summary>
        public override void GameUpdate()
        {
            //Log.Debug("Api.GameUpdate called.");
        }

        /// <summary>
        /// Called when the game is in the process of shutting down, after connections were closed and resources were unloaded.
        /// </summary>
        public override void GameShutdown()
        {
            Log.Debug("Api.GameShutdown called.");
            TelemetryTools.Shutdown();
            CommandTools.InvokeScriptEvents(ScriptEvent.gameShutdown, () => new ScriptEventArgs());
        }

        /// <summary>
        /// Called when a player has connected but before he was authenticated with Steam. Do not trust the clientInfo data!
        /// </summary>
        /// <param name="clientInfo"></param>
        /// <param name="compatibilityVersion"></param>
        public override void PlayerLogin(ClientInfo clientInfo, string compatibilityVersion)
        {
            Log.Debug("Api.PlayerLogin called.");
            CommandTools.InvokeScriptEvents(ScriptEvent.playerLogin, () => new PlayerLoginEventArgs()
            {
                clientInfo           = clientInfo,
                compatibilityVersion = compatibilityVersion,
            });
        }

        /// <summary>
        /// Called when the player was authenticated and its entity was created ("entityLoaded" event) but before the player
        /// becomes visible and the loading screen disappears. During spawning all required chunks and entites are loaded.
        /// </summary>
        /// <param name="clientInfo"></param>
        /// <param name="chunkViewDim"></param>
        /// <param name="playerProfile"></param>
        public override void PlayerSpawning(ClientInfo clientInfo, int chunkViewDim, PlayerProfile playerProfile)
        {
            Log.Debug("Api.PlayerSpawning called.");
            CommandTools.InvokeScriptEvents(ScriptEvent.playerSpawning, () => new PlayerSpawningEventArgs()
            {
                clientInfo    = clientInfo,
                playerProfile = playerProfile,
            });
        }

        /// <summary>
        /// Called when the player was made visible and the loading screen disappeared.
        /// </summary>
        /// <param name="clientInfo"></param>
        /// <param name="respawnReason"></param>
        /// <param name="pos"></param>
        public override void PlayerSpawnedInWorld(ClientInfo clientInfo, RespawnType respawnReason, Vector3i pos)
        {
            Log.Debug("Api.PlayerSpawnedInWorld called.");
            CommandTools.InvokeScriptEvents(ScriptEvent.playerSpawnedInWorld, () => new PlayerSpawnedInWorldEventArgs()
            {
                reason     = respawnReason.ToString(),
                position   = pos,
                clientInfo = clientInfo,
            });
        }

        /// <summary>
        /// Called when a player has disconnected from the game and all associated game data is about to be unloaded.
        /// A chat message has not yet been distributed and "steamPlayerDisconnected" was not yet invoked.
        /// </summary>
        /// <param name="clientInfo"></param>
        /// <param name="shutdown"></param>
        public override void PlayerDisconnected(ClientInfo clientInfo, bool shutdown)
        {
            Log.Debug("Api.PlayerDisconnected called.");
            CommandTools.InvokeScriptEvents(ScriptEvent.playerDisconnected, () => new PlayerDisconnectedEventArgs()
            {
                clientInfo = clientInfo,
            });
        }

        /// <summary>
        /// Called in regular intervalls for players to save their player file to disk
        /// </summary>
        /// <param name="clientInfo"></param>
        /// <param name="playerDataFile"></param>
        public override void SavePlayerData(ClientInfo clientInfo, PlayerDataFile playerDataFile)
        {
            Log.Debug("Api.SavePlayerData called.");
            CommandTools.InvokeScriptEvents(ScriptEvent.playerSaveData, () =>
            {
                var playerDataDir = GameUtils.GetPlayerDataDir().Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
                return new PlayerSaveDataEventArgs()
                {
                    playerDataFile = playerDataDir + clientInfo.playerId + "." + PlayerDataFile.EXT,
                    clientInfo     = clientInfo,
                };
            });
        }

        /// <summary>
        /// Called for every chat message, including messages about Joins, Leaves, Died, Killed, etc.
        /// </summary>
        /// <param name="clientInfo"></param>
        /// <param name="messageType"></param>
        /// <param name="message"></param>
        /// <param name="mainName"></param>
        /// <param name="localizeMain"></param>
        /// <param name="secondaryName"></param>
        /// <param name="localizeSecondary"></param>
        /// <returns></returns>
        public override bool ChatMessage(ClientInfo clientInfo, EnumGameMessages messageType, string message, string mainName, bool localizeMain, string secondaryName, bool localizeSecondary)
        {
            Log.Debug("Api.ChatMessage called.");
            var args = new ChatMessageEventArgs()
            {
                messageType = messageType.ToString(),
                from        = mainName,
                message     = message,
                clientInfo  = clientInfo,
            };

            CommandTools.InvokeScriptEvents(ScriptEvent.chatMessage, () => args);

            return !args.isPropagationStopped;
        }

        /// <summary>
        /// Called when the map for the chunk was calculated by generating a pixel color for each of the 16x16 blocks.
        /// The map can be retrieved with chunk.GetMapColors().
        /// </summary>
        /// <param name="chunk"></param>
        public override void CalcChunkColorsDone(Chunk chunk)
        {
            // No logging to avoid spam
            // Log.Debug("Api.CalcChunkColorsDone called.");
            CommandTools.InvokeScriptEvents(ScriptEvent.chunkMapCalculated, () => new ChunkMapCalculatedEventArgs()
            {
                chunkKey  = chunk.Key,
                chunkPos  = ChunkTools.ChunkKeyToChunkXZ(chunk.Key),
            });
        }

        /// <summary>
        /// Returns the Mod instance that is associated with the currently executing assembly,
        /// or null if it is not (yet) fully loaded in the list of mods.
        /// This approach is better than just using GetMod(modName) because the user could've changed the modname in ModInfo.xml.
        /// </summary>
        internal static Mod GetExecutingMod()
        {
            return ModManager.GetLoadedMods()?.Where(m => m.Assembly.Equals(typeof(Api).Assembly)).SingleOrDefault();
        }

    }
}