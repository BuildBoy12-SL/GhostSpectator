// -----------------------------------------------------------------------
// <copyright file="Scp914Events.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator.EventHandlers
{
    using Exiled.Events.EventArgs;
    using Scp914Handlers = Exiled.Events.Handlers.Scp914;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers.Scp914"/>.
    /// </summary>
    public class Scp914Events
    {
        private readonly Plugin plugin;

        /// <summary>
        /// Initializes a new instance of the <see cref="Scp914Events"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin"/> class.</param>
        public Scp914Events(Plugin plugin) => this.plugin = plugin;

        /// <summary>
        /// Subscribes to all required events.
        /// </summary>
        public void Subscribe()
        {
            Scp914Handlers.Activating += OnActivating;
            Scp914Handlers.ChangingKnobSetting += OnChangingKnobSetting;
            Scp914Handlers.UpgradingPlayer += OnUpgradingPlayer;
            Scp914Handlers.UpgradingInventoryItem += OnUpgradingInventoryItem;
        }

        /// <summary>
        /// Unsubscribes from all required events.
        /// </summary>
        public void Unsubscribe()
        {
            Scp914Handlers.Activating -= OnActivating;
            Scp914Handlers.ChangingKnobSetting -= OnChangingKnobSetting;
            Scp914Handlers.UpgradingPlayer -= OnUpgradingPlayer;
            Scp914Handlers.UpgradingInventoryItem -= OnUpgradingInventoryItem;
        }

        private void OnActivating(ActivatingEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnChangingKnobSetting(ChangingKnobSettingEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnUpgradingPlayer(UpgradingPlayerEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnUpgradingInventoryItem(UpgradingInventoryItemEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }
    }
}