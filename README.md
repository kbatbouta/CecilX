# CecilX (RimWorld)

A Cecil (Mono) patcher for RimWorld.

## usage

1. Add desired fields following this format:
```cs
public class CecilFieldsDefOf
{
    [CecilField(typeof(Pawn), isStatic: false)]
    public static bool testBool;

    [CecilField(typeof(Pawn), isStatic: false)]
    public static Pawn testPawn;
}
 ```
2. Patch (similar to LibHarmony)
```cs
public static CecilPatcher patcher = new CecilPatcher();
public static Harmony harmony = new Harmony("krkr.roofs");

static Initialization()
{
    harmony.PatchAll();
    patcher.CreateAll();
}
```
