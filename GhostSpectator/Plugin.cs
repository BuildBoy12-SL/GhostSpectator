// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator
{
    using System;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using GhostSpectator.EventHandlers;
    using HarmonyLib;

    /// <summary>
    /// The main plugin class.
    /// </summary>
    public class Plugin : Plugin<Config, Translation>
    {
        private Harmony harmony;

        private MapEvents mapEvents;
        private PlayerEvents playerEvents;
        private Scp049Events scp049Events;
        private Scp096Events scp096Events;
        private Scp106Events scp106Events;
        private Scp914Events scp914Events;
        private ServerEvents serverEvents;
        private WarheadEvents warheadEvents;

        /// <summary>
        /// Gets the only existing instance of the <see cref="Plugin"/> class.
        /// </summary>
        public static Plugin Instance { get; private set; }

        /// <summary>
        /// Gets an instance of the <see cref="GhostSpectator"/> class.
        /// </summary>
        public Ghost Ghost { get; private set; }

        /// <inheritdoc />
        public override string Author => "Build";

        /// <inheritdoc />
        public override string Name => "GhostSpectator";

        /// <inheritdoc />
        public override string Prefix => "GhostSpectator";

        /// <inheritdoc />
        public override PluginPriority Priority => PluginPriority.Last;

        /// <inheritdoc />
        public override Version RequiredExiledVersion { get; } = new Version(4, 1, 5);

        /// <inheritdoc />
        public override Version Version { get; } = new Version(1, 0, 0);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            Ghost = new Ghost();

            Instance = this;

            harmony = new Harmony($"build.ghostSpectator.{DateTime.UtcNow.Ticks}");
            harmony.PatchAll();

            mapEvents = new MapEvents(this);
            mapEvents.Subscribe();
            playerEvents = new PlayerEvents(this);
            playerEvents.Subscribe();
            scp049Events = new Scp049Events(this);
            scp049Events.Subscribe();
            scp096Events = new Scp096Events(this);
            scp096Events.Subscribe();
            scp106Events = new Scp106Events(this);
            scp106Events.Subscribe();
            scp914Events = new Scp914Events(this);
            scp914Events.Subscribe();
            serverEvents = new ServerEvents(this);
            serverEvents.Subscribe();
            warheadEvents = new WarheadEvents(this);
            warheadEvents.Subscribe();

            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            mapEvents.Unsubscribe();
            mapEvents = null;
            playerEvents.Unsubscribe();
            playerEvents = null;
            scp049Events.Unsubscribe();
            scp049Events = null;
            scp096Events.Unsubscribe();
            scp096Events = null;
            scp106Events.Unsubscribe();
            scp106Events = null;
            scp914Events.Unsubscribe();
            scp914Events = null;
            serverEvents.Unsubscribe();
            serverEvents = null;
            warheadEvents.Unsubscribe();
            warheadEvents = null;

            harmony.UnpatchAll();
            harmony = null;
            Ghost = null;

            Instance = null;
            base.OnDisabled();
        }
    }
}