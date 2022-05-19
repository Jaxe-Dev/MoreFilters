using Verse;

namespace MoreFilters.Patch
{
  internal static class Extensions
  {
    public static string Italic(this string self) => "<i>" + self + "</i>";
    public static string Bold(this string self) => "<b>" + self + "</b>";
    public static string Bold(this TaggedString self) => self.ToString().Bold();
    public static string Indent(this TaggedString self) => "  " + self;
    public static string TrimName(this string self) => self.TrimStart('_').CapitalizeFirst();
  }
}
