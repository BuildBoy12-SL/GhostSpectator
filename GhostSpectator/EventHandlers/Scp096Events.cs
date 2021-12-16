// -----------------------------------------------------------------------
// <copyright file="Scp096Events.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator.EventHandlers
{
    using Exiled.Events.EventArgs;
    using Scp096Handlers = Exiled.Events.Handlers.Scp096;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers.Scp096"/>.
    /// </summary>
    public class Scp096Events
    {
        private readonly Plugin plugin;

        /// <summary>
        /// Initializes a new instance of the <see cref="Scp096Events"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin"/> class.</param>
        public Scp096Events(Plugin plugin) => this.plugin = plugin;

        /// <summary>
        /// Subscribes to all required events.
        /// </summary>
        public void Subscribe()
        {
            Scp096Handlers.AddingTarget += OnAddingTarget;
        }

        /// <summary>
        /// Unsubscribes from all required events.
        /// </summary>
        public void Unsubscribe()
        {
            Scp096Handlers.AddingTarget -= OnAddingTarget;
        }

        private void OnAddingTarget(AddingTargetEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Target))
                ev.IsAllowed = false;
        }
    }
}