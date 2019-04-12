using System;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using UnityEngine;

namespace R2API
{
    public static class PlayerValuesAPI
    {
        public static bool GodMode { get; set; } = false;
        public static void InitHooks()
        {
            IL.RoR2.HealthComponent.TakeDamage += (il) =>
            {
                ILCursor cursor = new ILCursor(il).Goto(0);
                cursor.Emit(OpCodes.Ldarg_0);
                cursor.EmitDelegate<Action<HealthComponent>>(instance =>
                {
                    if (!GodMode || !instance.CompareTag("Player"))
                    {
                        return;
                    }
                    instance.godMode = GodMode;
                });
            };
        }
    }
}