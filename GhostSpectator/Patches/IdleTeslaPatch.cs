// -----------------------------------------------------------------------
// <copyright file="IdleTeslaPatch.cs" company="Build">
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
    /// Patches <see cref="TeslaGate.PlayerInIdleRange"/> to prevent ghosts from triggering the idle tesla gate animation.
    /// </summary>
    [HarmonyPatch(typeof(TeslaGate), nameof(TeslaGate.PlayerInIdleRange))]
    internal static class IdleTeslaPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
            Label checkSpectatorLabel = generator.DefineLabel();
            newInstructions[0].WithLabels(checkSpectatorLabel);
            newInstructions.InsertRange(0, new[]
            {
                new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(Plugin), nameof(Plugin.Instance))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Plugin), nameof(Plugin.Ghost))),
                new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Call, Method(typeof(Player), nameof(Player.Get), new[] { typeof(ReferenceHub) })),
                new CodeInstruction(OpCodes.Call, Method(typeof(Ghost), nameof(Ghost.Check), new[] { typeof(Player) })),
                new CodeInstruction(OpCodes.Brfalse_S, checkSpectatorLabel),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Ret),
            });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}