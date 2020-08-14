using System;
using System.IO;
using System.Runtime.InteropServices;
using HarmonyLib;
using Mono.Cecil;
using UnityEngine;
using Verse;

namespace CecilX
{
    public static class Initializer
    {
        public static void RestoreAndDisable()
        {

        }

        public static void CheckForBackups()
        {
            if (File.Exists(Finder.outputAssemblyPath))
                Finder.assembliesFound = true;

            if (File.Exists(Finder.targetAssemblyPath))
                Finder.backupExists = true;
        }

        public static void Initialize()
        {
            Finder.Harmony = new Harmony("krkr.cecilx");
            Finder.Harmony.PatchAll();

            GetOSSpesifics();
            CheckForBackups();

            Log.Message("target:" + Finder.targetAssemblyPath);
            Log.Message("output:" + Finder.outputAssemblyPath);

            if (!Finder.backupExists)
            {
                CreateBackup();
            }

            try
            {
                Finder.Module = ModuleDefinition.ReadModule(Finder.targetAssemblyPath);
            }
            catch (Exception er)
            {
                RestoreAndDisable();

                Log.Error(er.Message);
                Finder.ERROR = true;
                Finder.READY = false;
            }
        }

        static void CreateBackup()
        {
            if (!File.Exists(Finder.targetAssemblyPath))

                File.Copy(Finder.outputAssemblyPath, Finder.targetAssemblyPath, overwrite: true);
            else

                Log.Message("Assembly-CSharp backup found!!");
        }

        static void GetOSSpesifics()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Finder.targetAssemblyPath = Application.dataPath + "/Resources/Data/Managed/Assembly-CSharp.old.dll";
                Finder.outputAssemblyPath = Application.dataPath + "/Resources/Data/Managed/Assembly-CSharp.dll";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Finder.targetAssemblyPath = Application.dataPath + "/Managed/Assembly-CSharp.old.dll";
                Finder.outputAssemblyPath = Application.dataPath + "/Managed/Assembly-CSharp.dll";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Finder.targetAssemblyPath = Application.dataPath + "/Managed/Assembly-CSharp.old.dll";
                Finder.outputAssemblyPath = Application.dataPath + "/Managed/Assembly-CSharp.dll";
            }
        }
    }
}
