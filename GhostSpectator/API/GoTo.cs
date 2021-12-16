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

    public static class GoTo
    {
        public static void Door(Player player)
        {
            ReadOnlyCollection<Door> doors = DoorCache.Doors;

            Door chosen = null;

            Vector3 safePos = Vector3.zero;
            while (safePos == Vector3.zero)
            {
                chosen = doors[Random.Range(0, doors.Count)];
                if (PlayerMovementSync.FindSafePosition(chosen.Position + chosen.Base.transform.forward, out Vector3 pos))
                {
                    safePos = pos;
                }
            }

            string nameTag = chosen.Nametag;
            player.ClearBroadcasts();
            player.Broadcast(3, !string.IsNullOrEmpty(nameTag) ? Plugin.Instance.Translation.TeleportNamedDoor.Replace("{name}", nameTag) : Plugin.Instance.Translation.TeleportDoor);
            player.Position = safePos;
        }

        public static void Player(Player player)
        {
            Plugin plugin = Plugin.Instance;

            List<Player> validPlayers = Exiled.API.Features.Player.List.Where(p =>
                p.IsAlive && !plugin.Ghost.Check(p) && !plugin.Config.TeleportBlacklist.Contains(p.Role) &&
                !p.SessionVariables.ContainsKey("IsNPC")).ToList();

            if (validPlayers.IsEmpty())
            {
                player.ClearBroadcasts();
                player.Broadcast(3, plugin.Translation.FailTeleport);
                return;
            }

            Player chosen = validPlayers[Random.Range(0, validPlayers.Count)];

            player.ClearBroadcasts();
            player.Broadcast(3, plugin.Translation.TeleportPlayer
                .Replace("{name}", chosen.Nickname)
                .Replace("{class}", $"<color={chosen.RoleColor.ToHex()}>{chosen.ReferenceHub.characterClassManager.CurRole.fullName}</color>"));

            player.Position = chosen.Position + (Vector3.up * 2);
        }
    }
}