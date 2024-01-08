using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LC_EZAdmin
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        internal static HashSet<ulong> BannedIds;
        internal const string BannedListFilename = "banned_ids.txt";

        private void Awake()
        {
            Log = base.Logger;
            // Plugin startup logic
            Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            // read ids from banned file
            Log.LogInfo($"Reading {BannedListFilename}");
            string[] stringIds = System.IO.File.ReadAllLines(BannedListFilename);
            List<ulong> ids = new List<ulong>();
            foreach (string idStr in stringIds) {
                ulong id;
                if (UInt64.TryParse(idStr.Trim(), out id))
                {
                    ids.Add(id);
                }
                else
                {
                    Log.LogWarning($"Id {idStr} is not a valid integer. Omitting from ban list.");
                }
            }
            BannedIds = new HashSet<ulong>(ids);
            Log.LogInfo($"Loaded in {ids.Count()} players into ban list");

            var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll(typeof(Patches));
        }
    }
    internal class Patches
    {
        [HarmonyPatch(typeof(StartOfRound), "KickPlayer")]
        [HarmonyPostfix]
        public static void Postfix(StartOfRound __instance)
        {
            Plugin.BannedIds.UnionWith(__instance.KickedClientIds);
            Plugin.Log.LogInfo($"Dumping kicked players to {Plugin.BannedListFilename} ...");

            System.IO.File.WriteAllLines(Plugin.BannedListFilename, Plugin.BannedIds.Select(id => id.ToString()));
            Plugin.Log.LogInfo("Dump complete!");
        }

        [HarmonyPatch(typeof(StartOfRound), "Awake")]
        [HarmonyPrefix]
        public static void ServerStart(StartOfRound __instance)
        {
            Plugin.Log.LogInfo($"Automatically adding {Plugin.BannedIds.Count()} players to kick list");
            __instance.KickedClientIds.AddRange(Plugin.BannedIds);
        }
    }
}
