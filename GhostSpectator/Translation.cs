// -----------------------------------------------------------------------
// <copyright file="Translation.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator
{
    using System.ComponentModel;
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public class Translation : ITranslation
    {
        /// <summary>
        /// Gets or sets the message to show when a ghost teleports to a player.
        /// </summary>
        [Description("The message to show when a ghost teleports to a player.")]
        public string TeleportPlayer { get; set; } = "You teleported to <b>{name}</b> who is a <b>{class}</b>.";

        /// <summary>
        /// Gets or sets the message to be displayed to a ghost when there are no valid teleport targets.
        /// </summary>
        [Description("The message to be displayed to a ghost when there are no valid teleport targets.")]
        public string FailTeleport { get; set; } = "Could not find a valid player to teleport to.";

        /// <summary>
        /// Gets or sets the message to show when a player teleports to a door.
        /// </summary>
        [Description("The message to show when a player teleports to a door.")]
        public string TeleportDoor { get; set; } = "You teleported to a door.";

        /// <summary>
        /// Gets or sets the message to show when a player teleports to a door.
        /// </summary>
        [Description("The message to show when a player teleports to a door.")]
        public string TeleportNamedDoor { get; set; } = "You teleported to a door named {name}.";
    }
}