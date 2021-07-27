# More Filters
![](https://img.shields.io/badge/Mod_Version-1.5-blue.svg)
![](https://img.shields.io/badge/Built_for_RimWorld-1.3-blue.svg)
![](https://img.shields.io/badge/Powered_by_Harmony-2.x-blue.svg)
![Steam Subscribers](https://img.shields.io/badge/dynamic/xml.svg?label=Steam+Subscribers&query=//table/tr[2]/td[1]&colorB=blue&url=https://steamcommunity.com/sharedfiles/filedetails/%3Fid=1508850027&suffix=+total)
![GitHub Downloads](https://img.shields.io/github/downloads/Jaxe-Dev/MoreFilters/total.svg?colorB=blue&label=GitHub+Downloads)

[Link to Steam Workshop page](https://steamcommunity.com/sharedfiles/filedetails/?id=1911734422)

---

This mod adds several new filters to item lists to make it easier to customize your stockpiles and bills.

Currently, the following filters can be selected/deselected:

*Rottable (all items that can rot unless frozen)
*Degradable (all items that will deteriorate unless under shelter)

Also for Weapons and Apparel, filters are added for each tech level so for example you can quickly make a Smelt Weapon bill that only affects medieval weapons.

This mod can be added or removed from a savegame at any time without issue.

---

##### INSTALLATION
- **[Go to the Steam Workshop page](https://steamcommunity.com/sharedfiles/filedetails/?id=1911734422) and subscribe to the mod.**

---

The following base methods are patched with Harmony:
```C#
Prefix  : Verse.ThingCategoryNodeDatabase.FinalizeInit
Postfix : Verse.ThingCategoryNodeDatabase.FinalizeInit
Prefix* : Verse_ThingFilter_RecalculateDisplayRootCategory
```
*A prefix marked by a \* denotes that in some circumstances the original method will be bypassed*