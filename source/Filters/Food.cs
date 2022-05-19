using MoreFilters.Patch;
using RimWorld;
using Verse;

namespace MoreFilters.Filters
{
  internal static class Food
  {
    private static ThingCategoryDef _foodConventionalCategoryDef;

    private static bool _useFoodConventional = true;

    public static void Apply()
    {
      _foodConventionalCategoryDef = ThingCategoryDef.Named("FilterFoodConventional");

      if (_useFoodConventional) { Core.AllFilters.Add(_foodConventionalCategoryDef); }
    }

    public static void Parse(ThingDef def)
    {
      if (_useFoodConventional && def.ingestible != null && (def.ingestible.preferability == FoodPreferability.RawTasty || def.ingestible.preferability > FoodPreferability.MealAwful || (def.thingCategories?.Contains(ThingCategoryDefOf.EggsUnfertilized) ?? false) || (def.ingestible.joy > 0 && def.ingestible.drugCategory == DrugCategory.None))) { Core.AddCategory(def, _foodConventionalCategoryDef); }
    }

    public static void DrawSettings(Listing_Standard l)
    {
      l.Label(ThingCategoryDefOf.Foods.LabelCap.Bold());
      l.CheckboxLabeled(_foodConventionalCategoryDef.LabelCap.Indent(), ref _useFoodConventional);
      l.GapLine();
    }

    public static void ExposeData()
    {
      Scribe_Values.Look(ref _useFoodConventional, nameof(_useFoodConventional).TrimName(), true);
    }
  }
}
