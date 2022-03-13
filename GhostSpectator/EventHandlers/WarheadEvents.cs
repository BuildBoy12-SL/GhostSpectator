// -----------------------------------------------------------------------
// <copyright file="WarheadEvents.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator.EventHandlers
{
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using UnityEngine;
    using WarheadHandlers = Exiled.Events.Handlers.Warhead;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers.Warhead"/>.
    /// </summary>
    public class WarheadEvents
    {
        private static readonly Vector3 SurfacePosition = new Vector3(0, 1003, 7);
        private readonly Plugin plugin;

        /// <summary>
        /// Initializes a new instance of the <see cref="WarheadEvents"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin"/> class.</param>
        public WarheadEvents(Plugin plugin) => this.plugin = plugin;

        /// <summary>
        /// Subscribes to all required events.
        /// </summary>
        public void Subscribe()
        {
            WarheadHandlers.ChangingLeverStatus += OnChangingLeverStatus;
            WarheadHandlers.Detonated += OnDetonated;
            WarheadHandlers.Starting += OnStarting;
            WarheadHandlers.Stopping += OnStopping;
        }

        /// <summary>
        /// Unsubscribes from all required events.
        /// </summary>
        public void Unsubscribe()
        {
            WarheadHandlers.ChangingLeverStatus -= OnChangingLeverStatus;
            WarheadHandlers.Detonated -= OnDetonated;
            WarheadHandlers.Starting -= OnStarting;
            WarheadHandlers.Stopping -= OnStopping;
        }

        private void OnChangingLeverStatus(ChangingLeverStatusEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnDetonated()
        {
            foreach (Player player in plugin.Ghost.TrackedPlayers)
            {
                if (player.Position.y < 800f)
                    player.Position = SurfacePosition;
            }
        }

        private void OnStarting(StartingEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnStopping(StoppingEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }
    }
}