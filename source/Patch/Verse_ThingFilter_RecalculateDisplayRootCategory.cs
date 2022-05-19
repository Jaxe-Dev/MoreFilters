using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using MoreFilters.Filters;
using Verse;

namespace MoreFilters.Patch
{
  [HarmonyPatch(typeof(ThingFilter), "RecalculateDisplayRootCategory")]
  internal static class Verse_ThingFilter_RecalculateDisplayRootCategory
  {
    private static readonly FieldInfo AllowDefsField = AccessTools.Field(typeof(ThingFilter), "allowedDefs");

    private static bool Prefix(ThingFilter __instance)
    {
      if (!(AllowDefsField.GetValue(__instance) is HashSet<ThingDef> allowedDefsField))
      {
        Mod.Warning("Cannot prefix RecalculateDisplayRootCategory");
        return true;
      }

      __instance.DisplayRootCategory = ThingCategoryNodeDatabase.RootNode;

      foreach (var node in ThingCategoryNodeDatabase.allThingCategoryNodes.Where(node => !Core.AllFilters.Contains(node.catDef)))
      {
        var flag = false;
        var flag2 = false;

        foreach (var thingDef in allowedDefsField)
        {
          if (node.catDef.ContainedInThisOrDescendant(thingDef)) { flag2 = true; }
          else { flag = true; }
        }
        if (!flag && flag2) { __instance.DisplayRootCategory = node; }
      }

      return false;
    }
  }
}
