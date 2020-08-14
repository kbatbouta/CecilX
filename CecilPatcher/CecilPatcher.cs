using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace CecilX
{
    public class CecilPatcher
    {
        private Assembly assembly;

        private static List<CecilFieldDef> fieldsDefOf = new List<CecilFieldDef>();

        public List<CecilFieldDef> Fields => fieldsDefOf;

        public CecilPatcher()
        {
            assembly = Assembly.GetCallingAssembly();

            Log.Message(assembly.FullName);
        }

        public void CreateAll()
        {
            assembly = Assembly.GetCallingAssembly();

            fieldsDefOf.AddRange(AccessTools.GetTypesFromAssembly(assembly).Where<Type>(
                t => !t.IsInterface).SelectMany(
                t => t.GetFields()).Where<FieldInfo>(field =>
           {
               Log.Message("Concidering Field:\t" + field.Name);
               if (!field.HasAttribute<CecilField>() || field.IsPrivate) return false;

               Log.Message("Field Found: \t<" + field.FieldType + ">\t" + field.Name);
               return true;
           }
           ).Select(t =>
           {
               var tmp = new CecilFieldDef();
               tmp.name = t.Name;
               tmp.fieldType = t.FieldType;
               tmp.field = t.TryGetAttribute<CecilField>();
               return tmp;
           }));
        }

        [Obsolete]
        public static void ApplyAll()
        {
            foreach (var f in fieldsDefOf) f.Apply(Finder.Module);
            Finder.Module.Write(Finder.outputAssemblyPath);
        }
    }
}
