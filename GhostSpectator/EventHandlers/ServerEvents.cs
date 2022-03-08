// -----------------------------------------------------------------------
// <copyright file="ServerEvents.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator.EventHandlers
{
    using GhostSpectator.API;
    using MEC;
    using ServerHandlers = Exiled.Events.Handlers.Server;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers.Server"/>.
    /// </summary>
    public class ServerEvents
    {
        /// <summary>
        /// Subscribes to all required events.
        /// </summary>
        public void Subscribe()
        {
            ServerHandlers.WaitingForPlayers += OnWaitingForPlayers;
        }

        /// <summary>
        /// Unsubscribes from all required events.
        /// </summary>
        public void Unsubscribe()
        {
            ServerHandlers.WaitingForPlayers -= OnWaitingForPlayers;
        }

        private void OnWaitingForPlayers()
        {
            Timing.CallDelayed(5f, DoorCache.Recache);
        }
    }
}