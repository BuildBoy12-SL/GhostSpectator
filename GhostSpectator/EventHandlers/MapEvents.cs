// -----------------------------------------------------------------------
// <copyright file="MapEvents.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator.EventHandlers
{
    using Exiled.Events.EventArgs;
    using GhostSpectator.API;
    using MapHandlers = Exiled.Events.Handlers.Map;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers.Map"/>.
    /// </summary>
    public class MapEvents
    {
        private readonly Plugin plugin;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapEvents"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin"/> class.</param>
        public MapEvents(Plugin plugin) => this.plugin = plugin;

        /// <summary>
        /// Subscribes to all required events.
        /// </summary>
        public void Subscribe()
        {
            MapHandlers.Decontaminating += OnDecontaminating;
            MapHandlers.PlacingBlood += OnPlacingBlood;
        }

        /// <summary>
        /// Unsubscribes from all required events.
        /// </summary>
        public void Unsubscribe()
        {
            MapHandlers.Decontaminating -= OnDecontaminating;
            MapHandlers.PlacingBlood -= OnPlacingBlood;
        }

        private void OnDecontaminating(DecontaminatingEventArgs ev)
        {
            DoorCache.OnDecontaminating();
        }

        private void OnPlacingBlood(PlacingBloodEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }
    }
}