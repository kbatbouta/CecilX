using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Verse;

namespace CecilX
{
    public class CecilFieldDef
    {
        public string name;

        public CecilField field;
        public Type fieldType;

        [Obsolete]
        public void Apply(ModuleDefinition module)
        {

            try
            {
#if DEBUG
                if (module == null)
                {
                    Log.Warning("Tried to recover from a null module");

                    module = ModuleDefinition.ReadModule(Finder.targetAssemblyPath);
                }
#endif

                TypeDefinition targetType = module.GetType(field.targetType.FullName);

                if (targetType == null) Log.Error("Target type not found! <" + field.targetType.FullName + ">");

                if (targetType.Fields.Any(f => f.Name == name))
                {
#if DEBUG
                    Console.WriteLine("Didn't readd field:\t" + name + "\t<" + fieldType + ">");
#endif
                    return;
                }
                Finder.READY = false;
                var reference = module.Import(fieldType);

                FieldDefinition fField = new FieldDefinition(name,
                    Mono.Cecil.FieldAttributes.Public |
                    FieldAttributes.HasFieldRVA,
                    reference);

                targetType.Fields.Add(fField);

#if DEBUG
                Console.WriteLine("Added field:\t" + name + "\t<" + fieldType + ">");
#endif
            }
            catch (Exception er)
            {

            }
            finally
            {

            }
        }
    }
}
