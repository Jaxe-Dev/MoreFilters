using HarmonyLib;
using MoreFilters.Filters;
using Verse;

namespace MoreFilters.Patch
{
  [HarmonyPatch(typeof(ThingCategoryNodeDatabase), "FinalizeInit")]
  internal static class Verse_ThingCategoryNodeDatabase_FinalizeInit
  {
    private static void Prefix() => Core.ApplyPrefix();

    private static void Postfix() => Core.ApplyPostfix();
  }
}
