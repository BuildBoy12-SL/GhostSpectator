// -----------------------------------------------------------------------
// <copyright file="Ghost.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator.Commands
{
    using System;
    using CommandSystem;
    using Exiled.API.Features;

    /// <inheritdoc />
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Ghost : ICommand
    {
        /// <inheritdoc />
        public string Command { get; } = "tutorial";

        /// <inheritdoc />
        public string[] Aliases { get; } = { "ghost", "spec" };

        /// <inheritdoc />
        public string Description { get; } = "Sets a user to a tutorial that cannot interact with objects.";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player == null)
            {
                response = "This command must be executed in-game.";
                return false;
            }

            if (Plugin.Instance.Ghost.Check(player))
            {
                player.Role = RoleType.Spectator;
                response = "Set you back to spectator.";
                return true;
            }

            if (player.IsDead)
            {
                Plugin.Instance.Ghost.AddRole(player);
                response = "Set you to a tutorial.";
                return true;
            }

            response = "This command may only be used by spectators.";
            return false;
        }
    }
}