using MoreFilters.Patch;
using RimWorld;
using Verse;

namespace MoreFilters.Filters
{
  internal static class Weapons
  {
    private static ThingCategoryDef _weaponNeolithicCategoryDef;
    private static ThingCategoryDef _weaponMedievalCategoryDef;
    private static ThingCategoryDef _weaponIndustrialCategoryDef;
    private static ThingCategoryDef _weaponSpacerCategoryDef;
    private static ThingCategoryDef _weaponUltraCategoryDef;
    private static ThingCategoryDef _weaponArcotechCategoryDef;

    private static bool _useWeaponNeolithic = true;
    private static bool _useWeaponMedieval = true;
    private static bool _useWeaponIndustrial = true;
    private static bool _useWeaponSpacer = true;
    private static bool _useWeaponUltra = true;
    private static bool _useWeaponArcotech = true;

    public static void Apply()
    {
      _weaponNeolithicCategoryDef = ThingCategoryDef.Named("FilterWeaponsNeolithic");
      _weaponMedievalCategoryDef = ThingCategoryDef.Named("FilterWeaponsMedieval");
      _weaponIndustrialCategoryDef = ThingCategoryDef.Named("FilterWeaponsIndustrial");
      _weaponSpacerCategoryDef = ThingCategoryDef.Named("FilterWeaponsSpacer");
      _weaponUltraCategoryDef = ThingCategoryDef.Named("FilterWeaponsUltra");
      _weaponArcotechCategoryDef = ThingCategoryDef.Named("FilterWeaponsArcotech");

      if (_useWeaponNeolithic) { Core.AllFilters.Add(_weaponNeolithicCategoryDef); }
      if (_useWeaponMedieval) { Core.AllFilters.Add(_weaponMedievalCategoryDef); }
      if (_useWeaponIndustrial) { Core.AllFilters.Add(_weaponIndustrialCategoryDef); }
      if (_useWeaponSpacer) { Core.AllFilters.Add(_weaponSpacerCategoryDef); }
      if (_useWeaponUltra) { Core.AllFilters.Add(_weaponUltraCategoryDef); }
      if (_useWeaponArcotech) { Core.AllFilters.Add(_weaponArcotechCategoryDef); }
    }

    public static void Parse(ThingDef def)
    {
      if (!def.IsWeapon || (def.FirstThingCategory != ThingCategoryDefOf.Weapons && def.FirstThingCategory.parent != ThingCategoryDefOf.Weapons)) { return; }

      if (_useWeaponNeolithic && def.techLevel == TechLevel.Neolithic) { Core.AddCategory(def, _weaponNeolithicCategoryDef); }
      if (_useWeaponMedieval && def.techLevel == TechLevel.Medieval) { Core.AddCategory(def, _weaponMedievalCategoryDef); }
      if (_useWeaponIndustrial && def.techLevel == TechLevel.Industrial) { Core.AddCategory(def, _weaponIndustrialCategoryDef); }
      if (_useWeaponSpacer && def.techLevel == TechLevel.Spacer) { Core.AddCategory(def, _weaponSpacerCategoryDef); }
      if (_useWeaponUltra && def.techLevel == TechLevel.Ultra) { Core.AddCategory(def, _weaponUltraCategoryDef); }
      if (_useWeaponArcotech && def.techLevel == TechLevel.Archotech) { Core.AddCategory(def, _weaponArcotechCategoryDef); }
    }

    public static void DrawSettings(Listing_Standard l)
    {
      l.Label(ThingCategoryDefOf.Weapons.LabelCap.Bold());
      l.CheckboxLabeled(_weaponNeolithicCategoryDef.LabelCap.Indent(), ref _useWeaponNeolithic);
      l.CheckboxLabeled(_weaponMedievalCategoryDef.LabelCap.Indent(), ref _useWeaponMedieval);
      l.CheckboxLabeled(_weaponIndustrialCategoryDef.LabelCap.Indent(), ref _useWeaponIndustrial);
      l.CheckboxLabeled(_weaponSpacerCategoryDef.LabelCap.Indent(), ref _useWeaponSpacer);
      l.CheckboxLabeled(_weaponUltraCategoryDef.LabelCap.Indent(), ref _useWeaponUltra);
      l.CheckboxLabeled(_weaponArcotechCategoryDef.LabelCap.Indent(), ref _useWeaponArcotech);
      l.GapLine();
    }

    public static void ExposeData()
    {
      Scribe_Values.Look(ref _useWeaponNeolithic, nameof(_useWeaponNeolithic).TrimName(), true);
      Scribe_Values.Look(ref _useWeaponMedieval, nameof(_useWeaponMedieval).TrimName(), true);
      Scribe_Values.Look(ref _useWeaponIndustrial, nameof(_useWeaponIndustrial).TrimName(), true);
      Scribe_Values.Look(ref _useWeaponSpacer, nameof(_useWeaponSpacer).TrimName(), true);
      Scribe_Values.Look(ref _useWeaponUltra, nameof(_useWeaponUltra).TrimName(), true);
      Scribe_Values.Look(ref _useWeaponArcotech, nameof(_useWeaponArcotech).TrimName(), true);
    }
  }
}
