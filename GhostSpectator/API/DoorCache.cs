﻿// -----------------------------------------------------------------------
// <copyright file="DoorCache.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator.API
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Exiled.API.Enums;
    using Exiled.API.Features;

    /// <summary>
    /// Manages a <see cref="List{T}"/> of <see cref="Door"/>s to remove those in LCZ post-decontamination.
    /// </summary>
    public static class DoorCache
    {
        private static readonly List<Door> DoorsValue = new List<Door>();

        /// <summary>
        /// Gets all valid doors to teleport to.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}"/> of <see cref="Door"/> which are considered to be valid teleport targets.</returns>
        public static ReadOnlyCollection<Door> Doors => DoorsValue.AsReadOnly();

        /// <summary>
        /// Clears and resets <see cref="DoorsValue"/> to hold every door in <see cref="Door.List"/>.
        /// </summary>
        internal static void Recache()
        {
            DoorsValue.Clear();
            DoorsValue.AddRange(Door.List);
        }

        /// <summary>
        /// Removes all doors which are in <see cref="ZoneType.LightContainment"/>.
        /// </summary>
        internal static void OnDecontaminating()
        {
            DoorsValue.RemoveAll(door =>
            {
                float y = door.Position.y;
                return y < 500 && y > -500;
            });
        }
    }
}