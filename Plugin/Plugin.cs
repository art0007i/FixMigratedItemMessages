using BepInEx;
using BepInEx.Logging;
using BepInEx.NET.Common;
using BepInExResoniteShim;
using FrooxEngine;
using HarmonyLib;
using SkyFrost.Base;

namespace FixMigratedItemMessages;

[ResonitePlugin(PluginMetadata.GUID, PluginMetadata.NAME, PluginMetadata.VERSION, PluginMetadata.AUTHORS, PluginMetadata.REPOSITORY_URL)]
[BepInDependency(BepInExResoniteShim.PluginMetadata.GUID, BepInDependency.DependencyFlags.HardDependency)]
public class Plugin : BasePlugin
{
#nullable disable
    internal static new ManualLogSource Log;
#nullable enable

    public override void Load()
    {
        Log = base.Log;
        HarmonyInstance.PatchAll();
    }
    
    [HarmonyPatch(typeof(ContactsDialog), "SpawnMessageItem")]
    class FixMigratedItemMessagesPatch
    {
        const string OLD_PREFIX = "neosdb";
        public static void Prefix(ref Record record)
        {
            if(record.AssetURI.StartsWith(OLD_PREFIX))
            {
                record.AssetURI = Engine.Current.PlatformProfile.DBScheme + record.AssetURI.Substring(OLD_PREFIX.Length);
            }
        }
    }
}
