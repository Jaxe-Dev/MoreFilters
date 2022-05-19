using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace MoreFilters.Filters
{
  internal static class Core
  {
    public static readonly List<ThingCategoryDef> AllFilters = new List<ThingCategoryDef>();

    public static void ApplyPrefix()
    {
      Basic.Apply();

      Apparel.Apply();
      Weapons.Apply();

      Food.Apply();
      Races.Apply();

      var list = Traverse.Create(typeof(DefDatabase<ThingCategoryDef>)).Field("defsList").GetValue<List<ThingCategoryDef>>();

      foreach (var def in AllFilters)
      {
        def.resourceReadoutRoot = true;
        list.Remove(def);
      }
      list.InsertRange(1, AllFilters);

      Races.Parse();

      foreach (var def in DefDatabase<ThingDef>.AllDefs.Where(def => def.IsWithinCategory(ThingCategoryDefOf.Root)))
      {
        Basic.Parse(def);

        Apparel.Parse(def);
        Weapons.Parse(def);

        Food.Parse(def);
      }
    }

    public static void ApplyPostfix() => ThingCategoryNodeDatabase.RootNode.catDef.childCategories[0].treeNode.SetOpen(-1, false);

    public static void AddCategory(ThingDef def, ThingCategoryDef category)
    {
      if (!def.thingCategories.Contains(category)) { def.thingCategories.Add(category); }
    }
  }
}
