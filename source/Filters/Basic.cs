using MoreFilters.Patch;
using RimWorld;
using Verse;

namespace MoreFilters.Filters
{
  internal static class Basic
  {
    private static ThingCategoryDef _degradableCategoryDef;
    private static ThingCategoryDef _rottableCategoryDef;

    private static bool _useDegradable = true;
    private static bool _useRottable = true;

    public static void Apply()
    {
      _degradableCategoryDef = ThingCategoryDef.Named("FilterDegradable");
      _rottableCategoryDef = ThingCategoryDef.Named("FilterRottable");

      if (_useDegradable) { Core.AllFilters.Add(_degradableCategoryDef); }
      if (_useRottable) { Core.AllFilters.Add(_rottableCategoryDef); }
    }

    public static void Parse(ThingDef def)
    {
      if (_useDegradable && def.CanEverDeteriorate && def.GetStatValueAbstract(StatDefOf.DeteriorationRate) > 0) { Core.AddCategory(def, _degradableCategoryDef); }
      if (_useRottable && def.HasComp(typeof(CompRottable))) { Core.AddCategory(def, _rottableCategoryDef); }
    }

    public static void DrawSettings(Listing_Standard l)
    {
      l.Label(ThingCategoryDefOf.Root.LabelCap.Bold());
      l.CheckboxLabeled(_degradableCategoryDef.LabelCap.Indent(), ref _useDegradable);
      l.CheckboxLabeled(_rottableCategoryDef.LabelCap.Indent(), ref _useRottable);
      l.GapLine();
    }

    public static void ExposeData()
    {
      Scribe_Values.Look(ref _useDegradable, nameof(_useDegradable).TrimName(), true);
      Scribe_Values.Look(ref _useRottable, nameof(_useRottable).TrimName(), true);
    }
  }
}
