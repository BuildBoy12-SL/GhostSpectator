// -----------------------------------------------------------------------
// <copyright file="DrawRandomTeamPatch.cs" company="Build">
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
    using Respawning;
    using UnityEngine;
    using static HarmonyLib.AccessTools;

    /// <summary>
    /// Patches <see cref="RespawnTickets.DrawRandomTeam"/> to allow ghosts to be considered as spectators.
    /// </summary>
    [HarmonyPatch(typeof(RespawnTickets), nameof(RespawnTickets.DrawRandomTeam))]
    internal static class DrawRandomTeamPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            int index = newInstructions.FindIndex(instruction => instruction.opcode == OpCodes.Ldc_I4_1);
            Label flagTrueLabel = generator.DefineLabel();
            newInstructions[index].labels.Add(flagTrueLabel);

            index = newInstructions.FindIndex(instruction => instruction.opcode == OpCodes.Stloc_3) + 1;

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(Plugin), nameof(Plugin.Instance))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Plugin), nameof(Plugin.Ghost))),
                new CodeInstruction(OpCodes.Ldloca_S, 3),
                new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(KeyValuePair<GameObject, ReferenceHub>), nameof(KeyValuePair<GameObject, ReferenceHub>.Key))),
                new CodeInstruction(OpCodes.Call, Method(typeof(Player), nameof(Player.Get), new[] { typeof(GameObject) })),
                new CodeInstruction(OpCodes.Call, Method(typeof(Ghost), nameof(Ghost.Check), new[] { typeof(Player) })),
                new CodeInstruction(OpCodes.Brtrue_S, flagTrueLabel),
            });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}