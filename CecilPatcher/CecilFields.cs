using System;
namespace CecilX
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class CecilField : Attribute
    {
        public bool isStatic;
        public Type targetType;

        public CecilField(Type targetType, bool isStatic)
        {
            this.targetType = targetType;
            this.isStatic = isStatic;
        }
    }
}
