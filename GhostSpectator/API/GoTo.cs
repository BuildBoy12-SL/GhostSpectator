// -----------------------------------------------------------------------
// <copyright file="GoTo.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator.API
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Exiled.API.Features;
    using UnityEngine;

    /// <summary>
    /// Handles teleporting of ghosts to preset options.
    /// </summary>
    public static class GoTo
    {
        /// <summary>
        /// Teleports a player to a door in <see cref="DoorCache.Doors"/>.
        /// </summary>
        /// <param name="player">The player to teleport.</param>
        public static void Door(Player player)
        {
            ReadOnlyCollection<Door> doors = DoorCache.Doors;

            Door chosen = null;

            int safetyIndex = 0;
            Vector3 safePos = Vector3.zero;
            while (safePos == Vector3.zero)
            {
                if (++safetyIndex >= 10)
                    break;

                chosen = doors[Random.Range(0, doors.Count)];
                if (PlayerMovementSync.FindSafePosition(chosen.Position + chosen.Base.transform.forward, out Vector3 pos))
                    safePos = pos;
            }

            if (chosen is null)
            {
                player.Broadcast(3, Plugin.Instance.Translation.FailTeleport, shouldClearPrevious: true);
                return;
            }

            string nameTag = chosen.Nametag;
            player.ClearBroadcasts();
            player.Broadcast(3, !string.IsNullOrEmpty(nameTag) ? Plugin.Instance.Translation.TeleportNamedDoor.Replace("{name}", nameTag) : Plugin.Instance.Translation.TeleportDoor);
            player.Position = safePos;
        }

        /// <summary>
        /// Teleports a player to a valid player in <see cref="Exiled.API.Features.Player.List"/>.
        /// </summary>
        /// <param name="player">The player to teleport.</param>
        public static void Player(Player player)
        {
            Plugin plugin = Plugin.Instance;

            List<Player> validPlayers = Exiled.API.Features.Player.List.Where(p =>
                p.IsAlive &&
                !plugin.Ghost.Check(p) &&
                !plugin.Config.TeleportBlacklist.Contains(p.Role) &&
                !p.SessionVariables.ContainsKey("IsNPC")).ToList();

            if (validPlayers.IsEmpty())
            {
                player.Broadcast(3, plugin.Translation.FailTeleport, shouldClearPrevious: true);
                return;
            }

            Player chosen = validPlayers[Random.Range(0, validPlayers.Count)];

            player.ClearBroadcasts();
            player.Broadcast(3, plugin.Translation.TeleportPlayer
                .Replace("{name}", chosen.Nickname)
                .Replace("{class}", $"<color={chosen.Role.Color.ToHex()}>{chosen.ReferenceHub.characterClassManager.CurRole.fullName}</color>"));

            player.Position = chosen.Position + (Vector3.up * 2);
        }
    }
}