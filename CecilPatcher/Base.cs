using System;
using UnityEngine;
using Verse;

namespace CecilX
{
    public class Base : Mod
    {
        public Base(ModContentPack content) : base(content)
        {
            Initializer.Initialize();

            Log.Message("Initialized");
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return base.SettingsCategory();
        }

        public override void WriteSettings()
        {
            base.WriteSettings();
        }
    }
}
