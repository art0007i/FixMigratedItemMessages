using HarmonyLib;
using ResoniteModLoader;
using FrooxEngine;
using SkyFrost.Base;

namespace FixMigratedItemMessages;

public class FixMigratedItemMessages : ResoniteMod
{
    public override string Name => "FixMigratedItemMessages";
    public override string Author => "art0007i";
    public override string Version => "1.0.0";
    public override string Link => "https://github.com/art0007i/FixMigratedItemMessages/";
    public override void OnEngineInit()
    {
        Harmony harmony = new Harmony("me.art0007i.FixMigratedItemMessages");
        harmony.PatchAll();

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
