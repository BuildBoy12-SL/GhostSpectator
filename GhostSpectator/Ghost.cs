// -----------------------------------------------------------------------
// <copyright file="Ghost.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Exiled.API.Features;
    using GhostSpectator.API;
    using MEC;

    /// <summary>
    /// Represents a ghost role in-game.
    /// </summary>
    public class Ghost
    {
        private readonly List<Player> trackedPlayers = new List<Player>();

        /// <summary>
        /// Gets a collection of players considered to be ghosts.
        /// </summary>
        public ReadOnlyCollection<Player> TrackedPlayers => trackedPlayers.AsReadOnly();

        /// <summary>
        /// Sets a player to be a ghost.
        /// </summary>
        /// <param name="player">The player to make a ghost.</param>
        public void AddRole(Player player)
        {
            if (Check(player))
                return;

            player.Role.Type = RoleType.Tutorial;

            player.IsGodModeEnabled = true;
            player.IsInvisible = true;
            player.NoClipEnabled = true;

            Scp096.TurnedPlayers.Add(player);
            Scp173.TurnedPlayers.Add(player);

            trackedPlayers.Add(player);

            Timing.CallDelayed(0.5f, () =>
            {
                if (!Check(player))
                    return;

                player.Position = SpawnPosition.Get(player);
                player.AddItem(ItemType.Coin);
                player.AddItem(ItemType.KeycardO5);
            });
        }

        /// <summary>
        /// Removes the ghost role from the player.
        /// </summary>
        /// <param name="player">The player to remove the role from.</param>
        public void RemoveRole(Player player)
        {
            if (!Check(player))
                return;

            player.IsGodModeEnabled = false;
            player.IsInvisible = false;
            player.NoClipEnabled = false;

            Scp096.TurnedPlayers.Remove(player);
            Scp173.TurnedPlayers.Remove(player);

            trackedPlayers.Remove(player);

            if (player.IsAlive)
                player.Role.Type = RoleType.Spectator;
        }

        /// <summary>
        /// Checks if a player is a ghost.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <returns>A value indicating whether the checked player is a ghost.</returns>
        public bool Check(Player player) => TrackedPlayers.Contains(player);
    }
}