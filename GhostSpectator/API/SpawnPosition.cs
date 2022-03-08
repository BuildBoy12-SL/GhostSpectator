// -----------------------------------------------------------------------
// <copyright file="SpawnPosition.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator.API
{
    using System.Collections.Generic;
    using Exiled.API.Features;
    using UnityEngine;

    /// <summary>
    /// Handles available positions to spawn ghosts.
    /// </summary>
    public static class SpawnPosition
    {
        private static readonly Dictionary<int, Vector3> SpawnPositions = new Dictionary<int, Vector3>();
        private static readonly Vector3 PocketDimensionPosition = Vector3.down * 1996f;
        private static readonly Vector3 TutorialTowerPosition = new Vector3(55f, 1021f, -45f);

        /// <summary>
        /// Gets a valid spawnpoint for a user.
        /// </summary>
        /// <param name="player">The player to get the spawn position of.</param>
        /// <returns>The valid spawnpoint of the player.</returns>
        public static Vector3 Get(Player player)
        {
            return SpawnPositions.TryGetValue(player.Id, out Vector3 position) ? position : TutorialTowerPosition;
        }

        /// <summary>
        /// Updates the spawn position of a player.
        /// </summary>
        /// <param name="player">The player to update.</param>
        public static void Update(Player player)
        {
            SpawnPositions[player.Id] = player.IsInPocketDimension ? PocketDimensionPosition : player.Position + (Vector3.up * 1.5f);
        }
    }
}