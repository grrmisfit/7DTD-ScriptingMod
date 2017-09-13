﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using ScriptingMod.Extensions;
using ScriptingMod.Tools;

namespace ScriptingMod
{
    /*
     * TODO [P3]: Implement dj-reload-scripts to reload all command scripts
     */

    [UsedImplicitly]
    public class Api : ModApiAbstract
    {
        public Api()
        {
            Log.Debug("Api constructor called.");
        }

        public override void GameAwake()
        {
            //Log.Debug("Api.GameAwake called.");
            try
            {
                Log.Out($"Initializing {Constants.ModNameFull} ...");
                NonPublic.Init();
                PersistentData.Load();
                CommandTools.LoadScripts();
                CommandTools.InitScriptsMonitoring();
                RepairEngine.InitAuto();
                Log.Out($"Done initializing {Constants.ModNameFull}.");
            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        public override void GameStartDone()
        {
            //Log.Debug("Api.GameStartDone called.");
        }

        public override void GameUpdate()
        {
            // Logging makes too much noise!
            //Log.Debug("Api.GameUpdate called.");
        }

        public override void GameShutdown()
        {
            //Log.Debug("Api.GameShutdown called.");
        }

        public override void PlayerLogin(ClientInfo _cInfo, string _compatibilityVersion)
        {
            //Log.Debug("Api.PlayerLogin called.");
        }

        public override void PlayerSpawning(ClientInfo _cInfo, int _chunkViewDim, PlayerProfile _playerProfile)
        {
            //Log.Debug("Api.PlayerSpawning called.");
        }

        public event Action<ClientInfo, RespawnType, Vector3i> OnPlayerSpawnedInWorld;

        public override void PlayerSpawnedInWorld(ClientInfo cInfo, RespawnType respawnReason, Vector3i pos)
        {
            //Log.Debug("Api.PlayerSpawnedInWorld called.");
            OnPlayerSpawnedInWorld?.Invoke(cInfo, respawnReason, pos);
        }

        public override void PlayerDisconnected(ClientInfo _cInfo, bool _bShutdown)
        {
            //Log.Debug("Api.PlayerDisconnected called.");
        }

        public override void SavePlayerData(ClientInfo _cInfo, PlayerDataFile _playerDataFile)
        {
            //Log.Debug("Api.SavePlayerData called.");
        }

        public override bool ChatMessage(ClientInfo _cInfo, EnumGameMessages _type, string _msg, string _mainName, bool _localizeMain, string _secondaryName, bool _localizeSecondary)
        {
            //Log.Debug("Api.ChatMessage called.");
            return true;
        }

        public override void CalcChunkColorsDone(Chunk _chunk)
        {
            //Log.Debug("Api.CalcChunkColorsDone called.");
        }

    }
}
