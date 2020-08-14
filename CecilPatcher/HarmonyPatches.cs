
using System;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace CecilX
{
    [HarmonyPatch(typeof(UIRoot_Entry), nameof(UIRoot_Entry.Init))]
    public static class H_Apply
    {
        public static void Postfix()
        {
            if (!Finder.ERROR) CecilPatcher.ApplyAll();

            if (!Finder.READY || Finder.ERROR)
            {
                Dialog_MessageBox window = new Dialog_MessageBox(
                    Finder.ERROR ? "An Error during patching forced mod loading to stop!" :
                    "A patch has been apply. Please restart your game now. (If you don't restart mods won't work correctly)", "Quit & Apply Patches", delegate
                 {
                     Application.Quit();
                 }, "Ignore".Translate());

                Find.WindowStack.Add(window);
            }
#if DEBUG
            Log.Message("Cecil: Harmony Ok");
#endif
        }
    }
}
