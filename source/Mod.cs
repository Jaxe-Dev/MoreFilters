using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace MoreFilters
{
  [StaticConstructorOnStartup]
  internal class Mod : Verse.Mod
  {
    public const string Id = "MoreFilters";
    public const string Name = "More Filters";
    public const string Version = "1.6";

    public static readonly List<ThingCategoryDef> AllFilters = new List<ThingCategoryDef>();

    public Mod(ModContentPack content) : base(content)
    {
      new Harmony(Id).PatchAll();
      Log("Initialized");
    }

    public static void Log(string message) => Verse.Log.Message(PrefixMessage(message));
    public static void Warning(string message) => Verse.Log.Warning(PrefixMessage(message));
    private static string PrefixMessage(string message) => $"[{Name} v{Version}] {message}";

    public static void ApplyPrefix()
    {
      var rottableCategoryDef = ThingCategoryDef.Named("FilterRottable");
      var degradableCategoryDef = ThingCategoryDef.Named("FilterDegradable");

      var conventionalCategoryDef = ThingCategoryDef.Named("FilterFoodConventional");

      var meatFarmCategoryDef = ThingCategoryDef.Named("FilterMeatFarm");
      var meatPetCategoryDef = ThingCategoryDef.Named("FilterMeatPet");
      var meatFighterCategoryDef = ThingCategoryDef.Named("FilterMeatFighter");
      var meatGameCategoryDef = ThingCategoryDef.Named("FilterMeatGame");

      var corpseFarmCategoryDef = ThingCategoryDef.Named("FilterCorpseFarm");
      var corpsePetCategoryDef = ThingCategoryDef.Named("FilterCorpsePet");
      var corpseFighterCategoryDef = ThingCategoryDef.Named("FilterCorpseFighter");
      var corpseGameCategoryDef = ThingCategoryDef.Named("FilterCorpseGame");

      var apparelNeolithicCategoryDef = ThingCategoryDef.Named("FilterApparelNeolithic");
      var apparelMedievalCategoryDef = ThingCategoryDef.Named("FilterApparelMedieval");
      var apparelIndustrialCategoryDef = ThingCategoryDef.Named("FilterApparelIndustrial");
      var apparelSpacerCategoryDef = ThingCategoryDef.Named("FilterApparelSpacer");
      var apparelUltraCategoryDef = ThingCategoryDef.Named("FilterApparelUltra");
      var apparelArcotechCategoryDef = ThingCategoryDef.Named("FilterApparelArcotech");

      var weaponsNeolithicCategoryDef = ThingCategoryDef.Named("FilterWeaponsNeolithic");
      var weaponsMedievalCategoryDef = ThingCategoryDef.Named("FilterWeaponsMedieval");
      var weaponsIndustrialCategoryDef = ThingCategoryDef.Named("FilterWeaponsIndustrial");
      var weaponsSpacerCategoryDef = ThingCategoryDef.Named("FilterWeaponsSpacer");
      var weaponsUltraCategoryDef = ThingCategoryDef.Named("FilterWeaponsUltra");
      var weaponsArcotechCategoryDef = ThingCategoryDef.Named("FilterWeaponsArcotech");

      AllFilters.Add(rottableCategoryDef);
      AllFilters.Add(degradableCategoryDef);

      AllFilters.Add(conventionalCategoryDef);

      AllFilters.Add(apparelNeolithicCategoryDef);
      AllFilters.Add(apparelMedievalCategoryDef);
      AllFilters.Add(apparelIndustrialCategoryDef);
      AllFilters.Add(apparelSpacerCategoryDef);
      AllFilters.Add(apparelUltraCategoryDef);
      AllFilters.Add(apparelArcotechCategoryDef);

      AllFilters.Add(weaponsNeolithicCategoryDef);
      AllFilters.Add(weaponsMedievalCategoryDef);
      AllFilters.Add(weaponsIndustrialCategoryDef);
      AllFilters.Add(weaponsSpacerCategoryDef);
      AllFilters.Add(weaponsUltraCategoryDef);
      AllFilters.Add(weaponsArcotechCategoryDef);

      var list = Traverse.Create(typeof(DefDatabase<ThingCategoryDef>)).Field("defsList").GetValue<List<ThingCategoryDef>>();

      foreach (var def in AllFilters) { list.Remove(def); }

      list.InsertRange(1, AllFilters);

      foreach (var def in DefDatabase<ThingDef>.AllDefs.Where(def => def.race?.meatDef != null && def.race.meatDef.IsIngestible && def.tradeTags != null))
      {
        if (def.tradeTags.Contains("AnimalFighter"))
        {
          AddCategory(def.race.meatDef, meatFighterCategoryDef);
          AddCategory(def.race.corpseDef, corpseFighterCategoryDef);
        }
        else if (def.race.petness > 0.5)
        {
          AddCategory(def.race.meatDef, meatPetCategoryDef);
          AddCategory(def.race.corpseDef, corpsePetCategoryDef);
        }
        else if (def.tradeTags.Contains("AnimalFarm"))
        {
          AddCategory(def.race.meatDef, meatFarmCategoryDef);
          AddCategory(def.race.corpseDef, corpseFarmCategoryDef);
        }
        else if (def.race.petness > 0)
        {
          AddCategory(def.race.meatDef, meatPetCategoryDef);
          AddCategory(def.race.corpseDef, corpsePetCategoryDef);
        }
        else if (def.tradeTags.Contains("AnimalCommon") || def.tradeTags.Contains("AnimalUncommon") || def.tradeTags.Contains("AnimalExotic"))
        {
          AddCategory(def.race.meatDef, meatGameCategoryDef);
          AddCategory(def.race.corpseDef, corpseGameCategoryDef);
        }
      }

      foreach (var def in DefDatabase<ThingDef>.AllDefs.Where(def => def.IsWithinCategory(ThingCategoryDefOf.Root)))
      {
        if (def.HasComp(typeof(CompRottable))) { AddCategory(def, rottableCategoryDef); }
        if (CanDeteriorate(def)) { AddCategory(def, degradableCategoryDef); }

        if (IsFoodConventional(def)) { AddCategory(def, conventionalCategoryDef); }

        if (def.IsApparel)
        {
          if (def.techLevel == TechLevel.Neolithic) { AddCategory(def, apparelNeolithicCategoryDef); }
          if (def.techLevel == TechLevel.Medieval) { AddCategory(def, apparelMedievalCategoryDef); }
          if (def.techLevel == TechLevel.Industrial) { AddCategory(def, apparelIndustrialCategoryDef); }
          if (def.techLevel == TechLevel.Spacer) { AddCategory(def, apparelSpacerCategoryDef); }
          if (def.techLevel == TechLevel.Ultra) { AddCategory(def, apparelUltraCategoryDef); }
          if (def.techLevel == TechLevel.Archotech) { AddCategory(def, apparelArcotechCategoryDef); }
        }

        if (def.IsWeapon)
        {
          if (def.techLevel == TechLevel.Neolithic) { AddCategory(def, weaponsNeolithicCategoryDef); }
          if (def.techLevel == TechLevel.Medieval) { AddCategory(def, weaponsMedievalCategoryDef); }
          if (def.techLevel == TechLevel.Industrial) { AddCategory(def, weaponsIndustrialCategoryDef); }
          if (def.techLevel == TechLevel.Spacer) { AddCategory(def, weaponsSpacerCategoryDef); }
          if (def.techLevel == TechLevel.Ultra) { AddCategory(def, weaponsUltraCategoryDef); }
          if (def.techLevel == TechLevel.Archotech) { AddCategory(def, weaponsArcotechCategoryDef); }
        }
      }
    }

    public static void ApplyPostfix() => ThingCategoryNodeDatabase.RootNode.catDef.childCategories[0].treeNode.SetOpen(-1, false);

    private static bool CanDeteriorate(ThingDef def) => def.CanEverDeteriorate && def.GetStatValueAbstract(StatDefOf.DeteriorationRate) > 0;
    private static bool IsFoodConventional(ThingDef def) => def.ingestible != null && (def.ingestible.preferability == FoodPreferability.RawTasty || def.ingestible.preferability > FoodPreferability.MealAwful || (def.thingCategories?.Contains(ThingCategoryDefOf.EggsUnfertilized) ?? false) || (def.ingestible.joy > 0 && def.ingestible.drugCategory == DrugCategory.None));

    private static void AddCategory(ThingDef def, ThingCategoryDef category)
    {
      if (!def.thingCategories.Contains(category)) { def.thingCategories.Add(category); }
    }
  }
}
