// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public class Config : IConfig
    {
        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a <see cref="List{T}"/> of <see cref="RoleType"/>s which ghosts cannot teleport to.
        /// </summary>
        [Description("A list of roletypes which ghosts cannot teleport to.")]
        public List<RoleType> TeleportBlacklist { get; set; } = new List<RoleType>
        {
            RoleType.Tutorial,
        };
    }
}