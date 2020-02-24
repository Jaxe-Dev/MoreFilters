using HarmonyLib;
using Verse;

namespace MoreFilters.Patch
{
    [HarmonyPatch(typeof(ThingCategoryNodeDatabase), "FinalizeInit")]
    internal static class Verse_ThingCategoryNodeDatabase_FinalizeInit
    {
        private static void Prefix() => Mod.ApplyPrefix();
        private static void Postfix() => Mod.ApplyPostfix();
    }
}
