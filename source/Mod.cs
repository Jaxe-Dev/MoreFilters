using HarmonyLib;
using UnityEngine;
using Verse;

namespace MoreFilters
{
  [StaticConstructorOnStartup]
  internal class Mod : Verse.Mod
  {
    public const string Id = "MoreFilters";
    public const string Name = "More Filters";
    public const string Version = "1.7";

    public Mod(ModContentPack content) : base(content)
    {
      new Harmony(Id).PatchAll();
      Log("Initialized");

      GetSettings<ModSettings>();
    }

    public override string SettingsCategory() => Name;

    public override void DoSettingsWindowContents(Rect rect)
    {
      ModSettings.Draw(rect);

      base.DoSettingsWindowContents(rect);
    }

    public static void Log(string message) => Verse.Log.Message(PrefixMessage(message));
    public static void Warning(string message) => Verse.Log.Warning(PrefixMessage(message));
    private static string PrefixMessage(string message) => $"[{Name} v{Version}] {message}";
  }
}
