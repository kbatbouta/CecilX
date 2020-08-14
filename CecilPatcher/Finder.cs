using System;
using HarmonyLib;
using Mono.Cecil;

namespace CecilX
{
    public static class Finder
    {
        public static string targetAssemblyPath;
        public static string outputAssemblyPath;

        public static bool backupExists = false;
        public static bool assembliesFound = false;

        public static ModuleDefinition Module;
        public static Harmony Harmony;
        public static bool READY = true;
        public static bool ERROR = false;
    }
}
