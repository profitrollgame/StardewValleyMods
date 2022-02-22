﻿using StardewValley;
using StardewValley.Tools;
using System;
using Object = StardewValley.Object;

namespace StardewRPG
{
    public partial class ModEntry
    {
        private static void Object_performObjectDropInAction_Prefix(Object __instance, ref int __state)
        {
            if (!Config.EnableMod || !__instance.bigCraftable.Value)
                return;
            __state = __instance.MinutesUntilReady;
        }
        private static void Object_performObjectDropInAction_Postfix(Object __instance, int __state)
        {
            if (!Config.EnableMod || !__instance.bigCraftable.Value || __state >= __instance.MinutesUntilReady)
                return;
            var sub = GetStatMod(GetStatValue(Game1.player, "wis", Config.BaseStatValue)) * Config.WisCraftTimeBonus;
            SMonitor.Log($"Modifying craft time {__instance.MinutesUntilReady } - {sub}");
            __instance.MinutesUntilReady = (int)Math.Round(__instance.MinutesUntilReady * (1 - sub));
        }
        private static void Object_salePrice_Postfix(Object __instance, ref int __result)
        {
            if (!Config.EnableMod)
                return;
            var mult = GetStatMod(GetStatValue(Game1.player, "cha", Config.BaseStatValue)) * Config.ChaPriceBonus;
            SMonitor.Log($"Modifying sell price of {__result} by {mult}x");
            __result = (int)Math.Round(__result * (1 - mult));
        }
        private static void Object_sellToStorePrice_Postfix(ref int __result)
        {
            if (!Config.EnableMod)
                return;
            var mult = GetStatMod(GetStatValue(Game1.player, "cha", Config.BaseStatValue)) * Config.ChaPriceBonus;
            SMonitor.Log($"Modifying sell price of {__result} by {mult}x");
            __result = (int)Math.Round(__result * (1 + mult));
        }
    }
}