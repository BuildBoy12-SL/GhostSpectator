// -----------------------------------------------------------------------
// <copyright file="FallDamagePatch.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator.Patches
{
#pragma warning disable SA1118
    using System.Collections.Generic;
    using System.Reflection.Emit;
    using Exiled.API.Features;
    using HarmonyLib;
    using NorthwoodLib.Pools;
    using static HarmonyLib.AccessTools;

    /// <summary>
    /// Patches <see cref="FallDamage.OnTouchdown"/> to disable fall damage computing for ghosts.
    /// </summary>
    [HarmonyPatch(typeof(FallDamage), nameof(FallDamage.OnTouchdown))]
    internal static class FallDamagePatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
            int index = newInstructions.FindIndex(x => x.opcode == OpCodes.Ldarg_0);
            Label runDamageLabel = newInstructions[index].labels[0];
            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(Plugin), nameof(Plugin.Instance))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Plugin), nameof(Plugin.Ghost))),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, Field(typeof(FallDamage), nameof(FallDamage._hub))),
                new CodeInstruction(OpCodes.Call, Method(typeof(Player), nameof(Player.Get), new[] { typeof(ReferenceHub) })),
                new CodeInstruction(OpCodes.Call, Method(typeof(Ghost), nameof(Ghost.Check), new[] { typeof(Player) })),
                new CodeInstruction(OpCodes.Brfalse_S, runDamageLabel),
                new CodeInstruction(OpCodes.Ret),
            });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}