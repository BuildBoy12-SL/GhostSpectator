// -----------------------------------------------------------------------
// <copyright file="Ghost.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator
{
    using System.Collections.Generic;
    using Exiled.API.Features;
    using GhostSpectator.API;
    using MEC;

    public class Ghost
    {
        public HashSet<Player> TrackedPlayers { get; } = new HashSet<Player>();

        public void AddRole(Player player)
        {
            player.Role = RoleType.Tutorial;

            player.IsGodModeEnabled = true;
            player.IsInvisible = true;
            player.NoClipEnabled = true;

            Scp096.TurnedPlayers.Add(player);
            Scp173.TurnedPlayers.Add(player);

            TrackedPlayers.Add(player);

            Timing.CallDelayed(0.5f, () =>
            {
                if (!Check(player))
                    return;

                player.Position = SpawnPosition.Get(player);
                player.AddItem(ItemType.Coin);
                player.AddItem(ItemType.KeycardO5);
            });
        }

        public void RemoveRole(Player player)
        {
            if (!Check(player))
                return;

            player.IsGodModeEnabled = false;
            player.IsInvisible = false;
            player.NoClipEnabled = false;

            Scp096.TurnedPlayers.Remove(player);
            Scp173.TurnedPlayers.Remove(player);

            TrackedPlayers.Remove(player);

            if (player.IsAlive)
                player.Role = RoleType.Spectator;
        }

        public bool Check(Player player) => TrackedPlayers.Contains(player);
    }
}