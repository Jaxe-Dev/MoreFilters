using MoreFilters.Patch;
using RimWorld;
using Verse;

namespace MoreFilters.Filters
{
  internal static class Apparel
  {
    private static ThingCategoryDef _apparelNeolithicCategoryDef;
    private static ThingCategoryDef _apparelMedievalCategoryDef;
    private static ThingCategoryDef _apparelIndustrialCategoryDef;
    private static ThingCategoryDef _apparelSpacerCategoryDef;
    private static ThingCategoryDef _apparelUltraCategoryDef;
    private static ThingCategoryDef _apparelArcotechCategoryDef;

    private static bool _useApparelNeolithic = true;
    private static bool _useApparelMedieval = true;
    private static bool _useApparelIndustrial = true;
    private static bool _useApparelSpacer = true;
    private static bool _useApparelUltra = true;
    private static bool _useApparelArcotech = true;

    public static void Apply()
    {
      _apparelNeolithicCategoryDef = ThingCategoryDef.Named("FilterApparelNeolithic");
      _apparelMedievalCategoryDef = ThingCategoryDef.Named("FilterApparelMedieval");
      _apparelIndustrialCategoryDef = ThingCategoryDef.Named("FilterApparelIndustrial");
      _apparelSpacerCategoryDef = ThingCategoryDef.Named("FilterApparelSpacer");
      _apparelUltraCategoryDef = ThingCategoryDef.Named("FilterApparelUltra");
      _apparelArcotechCategoryDef = ThingCategoryDef.Named("FilterApparelArcotech");

      if (_useApparelNeolithic) { Core.AllFilters.Add(_apparelNeolithicCategoryDef); }
      if (_useApparelMedieval) { Core.AllFilters.Add(_apparelMedievalCategoryDef); }
      if (_useApparelIndustrial) { Core.AllFilters.Add(_apparelIndustrialCategoryDef); }
      if (_useApparelSpacer) { Core.AllFilters.Add(_apparelSpacerCategoryDef); }
      if (_useApparelUltra) { Core.AllFilters.Add(_apparelUltraCategoryDef); }
      if (_useApparelArcotech) { Core.AllFilters.Add(_apparelArcotechCategoryDef); }
    }

    public static void Parse(ThingDef def)
    {
      if (!def.IsApparel) { return; }

      if (_useApparelNeolithic && def.techLevel == TechLevel.Neolithic) { Core.AddCategory(def, _apparelNeolithicCategoryDef); }
      if (_useApparelMedieval && def.techLevel == TechLevel.Medieval) { Core.AddCategory(def, _apparelMedievalCategoryDef); }
      if (_useApparelIndustrial && def.techLevel == TechLevel.Industrial) { Core.AddCategory(def, _apparelIndustrialCategoryDef); }
      if (_useApparelSpacer && def.techLevel == TechLevel.Spacer) { Core.AddCategory(def, _apparelSpacerCategoryDef); }
      if (_useApparelUltra && def.techLevel == TechLevel.Ultra) { Core.AddCategory(def, _apparelUltraCategoryDef); }
      if (_useApparelArcotech && def.techLevel == TechLevel.Archotech) { Core.AddCategory(def, _apparelArcotechCategoryDef); }
    }

    public static void DrawSettings(Listing_Standard l)
    {
      l.Label(ThingCategoryDefOf.Apparel.LabelCap.Bold());
      l.CheckboxLabeled(_apparelNeolithicCategoryDef.LabelCap.Indent(), ref _useApparelNeolithic);
      l.CheckboxLabeled(_apparelMedievalCategoryDef.LabelCap.Indent(), ref _useApparelMedieval);
      l.CheckboxLabeled(_apparelIndustrialCategoryDef.LabelCap.Indent(), ref _useApparelIndustrial);
      l.CheckboxLabeled(_apparelSpacerCategoryDef.LabelCap.Indent(), ref _useApparelSpacer);
      l.CheckboxLabeled(_apparelUltraCategoryDef.LabelCap.Indent(), ref _useApparelUltra);
      l.CheckboxLabeled(_apparelArcotechCategoryDef.LabelCap.Indent(), ref _useApparelArcotech);
      l.GapLine();
    }

    public static void ExposeData()
    {
      Scribe_Values.Look(ref _useApparelNeolithic, nameof(_useApparelNeolithic).TrimName(), true);
      Scribe_Values.Look(ref _useApparelMedieval, nameof(_useApparelMedieval).TrimName(), true);
      Scribe_Values.Look(ref _useApparelIndustrial, nameof(_useApparelIndustrial).TrimName(), true);
      Scribe_Values.Look(ref _useApparelSpacer, nameof(_useApparelSpacer).TrimName(), true);
      Scribe_Values.Look(ref _useApparelUltra, nameof(_useApparelUltra).TrimName(), true);
      Scribe_Values.Look(ref _useApparelArcotech, nameof(_useApparelArcotech).TrimName(), true);
    }
  }
}
