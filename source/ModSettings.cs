using MoreFilters.Filters;
using MoreFilters.Patch;
using UnityEngine;
using Verse;

namespace MoreFilters
{
  public class ModSettings : Verse.ModSettings
  {
    private static Vector2 _settingsScrollPosition;
    private static Rect _settingsViewRect;

    public static void Draw(Rect rect)
    {
      var l = new Listing_Standard();
      var mainRect = new Rect(rect.x, rect.y, rect.width, rect.height - 70f);
      var bottomRect = new Rect(rect.x, rect.yMax - 50f, rect.width, 50f);

      if (_settingsViewRect == default) { _settingsViewRect = new Rect(mainRect.x, mainRect.y, mainRect.width - 20f, 99999f); }

      Widgets.BeginScrollView(mainRect, ref _settingsScrollPosition, _settingsViewRect);

      l.Begin(_settingsViewRect);

      Basic.DrawSettings(l);

      Apparel.DrawSettings(l);
      Weapons.DrawSettings(l);

      Food.DrawSettings(l);
      Races.DrawSettings(l);

      l.End();

      Widgets.EndScrollView();
      _settingsViewRect.height = l.CurHeight;

      l.Begin(bottomRect);
      l.Label("Please RESTART RimWorld for any changes to take effect.".Bold().Italic().Colorize(Color.yellow));
      l.End();
    }

    public override void ExposeData()
    {
      Basic.ExposeData();

      Apparel.ExposeData();
      Weapons.ExposeData();

      Food.ExposeData();
      Races.ExposeData();

      base.ExposeData();
    }
  }
}
