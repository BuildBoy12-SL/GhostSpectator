// -----------------------------------------------------------------------
// <copyright file="SpawnPatch.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator.Patches
{
#pragma warning disable SA1118
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Emit;
    using Exiled.API.Features;
    using HarmonyLib;
    using NorthwoodLib.Pools;
    using Respawning;
    using static HarmonyLib.AccessTools;

    /// <summary>
    /// Patches <see cref="RespawnManager.Spawn"/> to allow ghosts to be considered as spectators.
    /// </summary>
    [HarmonyPatch(typeof(RespawnManager), nameof(RespawnManager.Spawn))]
    internal static class SpawnPatch
    {
        private static List<ReferenceHub> SpawnablePlayers => Player.List.Where(p =>
        {
            Ghost ghost = Plugin.Instance.Ghost;
            return (p.IsDead && !p.IsOverwatchEnabled) || ghost.Check(p);
        }).Select(x => x.ReferenceHub).ToList();

        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
            int index = newInstructions.FindIndex(x => x.opcode == OpCodes.Stloc_1) - 13;
            Label getPlayersLabel = newInstructions[index].labels[0];
            newInstructions.RemoveRange(index, 14);
            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(SpawnPatch), nameof(SpawnablePlayers))).WithLabels(getPlayersLabel),
                new CodeInstruction(OpCodes.Stloc_1),
            });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}