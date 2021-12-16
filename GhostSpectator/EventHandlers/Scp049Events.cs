// -----------------------------------------------------------------------
// <copyright file="Scp049Events.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator.EventHandlers
{
    using Exiled.Events.EventArgs;
    using Scp049Handlers = Exiled.Events.Handlers.Scp049;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers.Scp049"/>.
    /// </summary>
    public class Scp049Events
    {
        private readonly Plugin plugin;

        /// <summary>
        /// Initializes a new instance of the <see cref="Scp049Events"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin"/> class.</param>
        public Scp049Events(Plugin plugin) => this.plugin = plugin;

        /// <summary>
        /// Subscribes to all required events.
        /// </summary>
        public void Subscribe()
        {
            Scp049Handlers.FinishingRecall += OnFinishingRecall;
        }

        /// <summary>
        /// Unsubscribes from all required events.
        /// </summary>
        public void Unsubscribe()
        {
            Scp049Handlers.FinishingRecall -= OnFinishingRecall;
        }

        private void OnFinishingRecall(FinishingRecallEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Target))
                ev.IsAllowed = true;
        }
    }
}