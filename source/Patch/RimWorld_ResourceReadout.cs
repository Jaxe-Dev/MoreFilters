using System.Collections.Generic;
using HarmonyLib;
using MoreFilters.Filters;
using RimWorld;
using Verse;

namespace MoreFilters.Patch
{
  [HarmonyPatch(typeof(ResourceReadout), MethodType.Constructor)]
  internal static class RimWorld_ResourceReadout_ctor
  {
    private static void Postfix(ResourceReadout __instance)
    {
      var rootThingCategories = Traverse.Create(__instance).Field<List<ThingCategoryDef>>("RootThingCategories");
      foreach (var def in rootThingCategories.Value.ToArray())
      {
        if (Core.AllFilters.Contains(def)) { rootThingCategories.Value.Remove(def); }
      }
    }
  }
}
