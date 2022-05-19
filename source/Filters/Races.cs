using System.Linq;
using MoreFilters.Patch;
using RimWorld;
using Verse;

namespace MoreFilters.Filters
{
  internal static class Races
  {
    private static ThingCategoryDef _meatFarmCategoryDef;
    private static ThingCategoryDef _meatPetCategoryDef;
    private static ThingCategoryDef _meatFighterCategoryDef;
    private static ThingCategoryDef _meatGameCategoryDef;

    private static ThingCategoryDef _corpseFarmCategoryDef;
    private static ThingCategoryDef _corpsePetCategoryDef;
    private static ThingCategoryDef _corpseFighterCategoryDef;
    private static ThingCategoryDef _corpseGameCategoryDef;

    private static bool _useMeatFarm = true;
    private static bool _useMeatPet = true;
    private static bool _useMeatFighter = true;
    private static bool _useMeatGame = true;

    private static bool _useCorpseFarm = true;
    private static bool _useCorpsePet = true;
    private static bool _useCorpseFighter = true;
    private static bool _useCorpseGame = true;

    public static void Apply()
    {
      _meatFarmCategoryDef = ThingCategoryDef.Named("FilterMeatFarm");
      _meatPetCategoryDef = ThingCategoryDef.Named("FilterMeatPet");
      _meatFighterCategoryDef = ThingCategoryDef.Named("FilterMeatFighter");
      _meatGameCategoryDef = ThingCategoryDef.Named("FilterMeatGame");

      _corpseFarmCategoryDef = ThingCategoryDef.Named("FilterCorpseFarm");
      _corpsePetCategoryDef = ThingCategoryDef.Named("FilterCorpsePet");
      _corpseFighterCategoryDef = ThingCategoryDef.Named("FilterCorpseFighter");
      _corpseGameCategoryDef = ThingCategoryDef.Named("FilterCorpseGame");

      if (_useMeatFarm) { Core.AllFilters.Add(_meatFarmCategoryDef); }
      if (_useMeatPet) { Core.AllFilters.Add(_meatPetCategoryDef); }
      if (_useMeatFighter) { Core.AllFilters.Add(_meatFighterCategoryDef); }
      if (_useMeatGame) { Core.AllFilters.Add(_meatGameCategoryDef); }

      if (_useCorpseFarm) { Core.AllFilters.Add(_corpseFarmCategoryDef); }
      if (_useCorpsePet) { Core.AllFilters.Add(_corpsePetCategoryDef); }
      if (_useCorpseFighter) { Core.AllFilters.Add(_corpseFighterCategoryDef); }
      if (_useCorpseGame) { Core.AllFilters.Add(_corpseGameCategoryDef); }
    }

    public static void Parse()
    {
      foreach (var def in DefDatabase<ThingDef>.AllDefs.Where(def => def.race?.meatDef != null && def.race.meatDef.IsIngestible && def.tradeTags != null))
      {
        if (def.tradeTags.Contains("AnimalFighter"))
        {
          if (_useMeatFighter) { Core.AddCategory(def.race.meatDef, _meatFighterCategoryDef); }
          if (_useCorpseFighter) { Core.AddCategory(def.race.corpseDef, _corpseFighterCategoryDef); }
        }
        else if (def.race.petness > 0.5)
        {
          if (_useMeatPet) { Core.AddCategory(def.race.meatDef, _meatPetCategoryDef); }
          if (_useCorpsePet) { Core.AddCategory(def.race.corpseDef, _corpsePetCategoryDef); }
        }
        else if (def.tradeTags.Contains("AnimalFarm"))
        {
          if (_useMeatFarm) { Core.AddCategory(def.race.meatDef, _meatFarmCategoryDef); }
          if (_useCorpseFarm) { Core.AddCategory(def.race.corpseDef, _corpseFarmCategoryDef); }
        }
        else if (def.race.petness > 0)
        {
          if (_useMeatPet) { Core.AddCategory(def.race.meatDef, _meatPetCategoryDef); }
          if (_useCorpsePet) { Core.AddCategory(def.race.corpseDef, _corpsePetCategoryDef); }
        }
        else if (def.tradeTags.Contains("AnimalCommon") || def.tradeTags.Contains("AnimalUncommon") || def.tradeTags.Contains("AnimalExotic"))
        {
          if (_useMeatGame) { Core.AddCategory(def.race.meatDef, _meatGameCategoryDef); }
          if (_useCorpseGame) { Core.AddCategory(def.race.corpseDef, _corpseGameCategoryDef); }
        }
      }
    }

    public static void DrawSettings(Listing_Standard l)
    {
      l.Label(ThingCategoryDefOf.MeatRaw.LabelCap.Bold());
      l.CheckboxLabeled(_meatFarmCategoryDef.LabelCap.Indent(), ref _useMeatFarm);
      l.CheckboxLabeled(_meatPetCategoryDef.LabelCap.Indent(), ref _useMeatPet);
      l.CheckboxLabeled(_meatFighterCategoryDef.LabelCap.Indent(), ref _useMeatFighter);
      l.CheckboxLabeled(_meatGameCategoryDef.LabelCap.Indent(), ref _useMeatGame);
      l.GapLine();

      l.Label(ThingCategoryDefOf.CorpsesAnimal.LabelCap.Bold());
      l.CheckboxLabeled(_corpseFarmCategoryDef.LabelCap.Indent(), ref _useCorpseFarm);
      l.CheckboxLabeled(_corpsePetCategoryDef.LabelCap.Indent(), ref _useCorpsePet);
      l.CheckboxLabeled(_corpseFighterCategoryDef.LabelCap.Indent(), ref _useCorpseFighter);
      l.CheckboxLabeled(_corpseGameCategoryDef.LabelCap.Indent(), ref _useCorpseGame);
      l.GapLine();
    }

    public static void ExposeData()
    {
      Scribe_Values.Look(ref _useMeatFarm, nameof(_useMeatFarm).TrimName(), true);
      Scribe_Values.Look(ref _useMeatPet, nameof(_useMeatPet).TrimName(), true);
      Scribe_Values.Look(ref _useMeatFighter, nameof(_useMeatFighter).TrimName(), true);
      Scribe_Values.Look(ref _useMeatGame, nameof(_useMeatGame).TrimName(), true);

      Scribe_Values.Look(ref _useCorpseFarm, nameof(_useCorpseFarm).TrimName(), true);
      Scribe_Values.Look(ref _useCorpsePet, nameof(_useCorpsePet).TrimName(), true);
      Scribe_Values.Look(ref _useCorpseFighter, nameof(_useCorpseFighter).TrimName(), true);
      Scribe_Values.Look(ref _useCorpseGame, nameof(_useCorpseGame).TrimName(), true);
    }
  }
}
