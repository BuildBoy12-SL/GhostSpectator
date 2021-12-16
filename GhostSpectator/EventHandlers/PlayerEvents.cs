// -----------------------------------------------------------------------
// <copyright file="PlayerEvents.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.API.Features;

namespace GhostSpectator.EventHandlers
{
    using System.Linq;
    using Exiled.Events.EventArgs;
    using GhostSpectator.API;
    using UnityEngine;
    using PlayerHandlers = Exiled.Events.Handlers.Player;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers.Player"/>.
    /// </summary>
    public class PlayerEvents
    {
        private readonly Plugin plugin;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerEvents"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin"/> class.</param>
        public PlayerEvents(Plugin plugin) => this.plugin = plugin;

        /// <summary>
        /// Subscribes to all required events.
        /// </summary>
        public void Subscribe()
        {
            PlayerHandlers.ActivatingGenerator += OnActivatingGenerator;
            PlayerHandlers.ActivatingWarheadPanel += OnActivatingWarheadPanel;
            PlayerHandlers.ActivatingWorkstation += OnActivatingWorkstation;
            PlayerHandlers.ChangingRole += OnChangingRole;
            PlayerHandlers.ClosingGenerator += OnClosingGenerator;
            PlayerHandlers.DroppingItem += OnDroppingItem;
            PlayerHandlers.Dying += OnDying;
            PlayerHandlers.EnteringFemurBreaker += OnEnteringFemurBreaker;
            PlayerHandlers.Escaping += OnEscaping;
            PlayerHandlers.Handcuffing += OnHandcuffing;
            PlayerHandlers.Hurting += OnHurting;
            PlayerHandlers.InteractingDoor += OnInteractingDoor;
            PlayerHandlers.InteractingElevator += OnInteractingElevator;
            PlayerHandlers.InteractingLocker += OnInteractingLocker;
            PlayerHandlers.IntercomSpeaking += OnIntercomSpeaking;
            PlayerHandlers.OpeningGenerator += OnOpeningGenerator;
            PlayerHandlers.PickingUpAmmo += OnPickingUpAmmo;
            PlayerHandlers.PickingUpArmor += OnPickingUpArmor;
            PlayerHandlers.PickingUpItem += OnPickingUpItem;
            PlayerHandlers.ReceivingEffect += OnReceivingEffect;
            PlayerHandlers.RemovingHandcuffs += OnRemovingHandcuffs;
            PlayerHandlers.Shooting += OnShooting;
            PlayerHandlers.SpawningRagdoll += OnSpawningRagdoll;
            PlayerHandlers.StoppingGenerator += OnStoppingGenerator;
            PlayerHandlers.ThrowingItem += OnThrowingItem;
            PlayerHandlers.TriggeringTesla += OnTriggeringTesla;
            PlayerHandlers.UsingItem += OnUsingItem;
        }

        /// <summary>
        /// Unsubscribes from all required events.
        /// </summary>
        public void Unsubscribe()
        {
            PlayerHandlers.ActivatingGenerator -= OnActivatingGenerator;
            PlayerHandlers.ActivatingWarheadPanel -= OnActivatingWarheadPanel;
            PlayerHandlers.ChangingRole -= OnChangingRole;
            PlayerHandlers.ActivatingWorkstation -= OnActivatingWorkstation;
            PlayerHandlers.ClosingGenerator -= OnClosingGenerator;
            PlayerHandlers.DroppingItem -= OnDroppingItem;
            PlayerHandlers.Dying -= OnDying;
            PlayerHandlers.EnteringFemurBreaker -= OnEnteringFemurBreaker;
            PlayerHandlers.Escaping -= OnEscaping;
            PlayerHandlers.Handcuffing -= OnHandcuffing;
            PlayerHandlers.Hurting -= OnHurting;
            PlayerHandlers.InteractingDoor -= OnInteractingDoor;
            PlayerHandlers.InteractingElevator -= OnInteractingElevator;
            PlayerHandlers.InteractingLocker -= OnInteractingLocker;
            PlayerHandlers.IntercomSpeaking -= OnIntercomSpeaking;
            PlayerHandlers.OpeningGenerator -= OnOpeningGenerator;
            PlayerHandlers.PickingUpAmmo -= OnPickingUpAmmo;
            PlayerHandlers.PickingUpArmor -= OnPickingUpArmor;
            PlayerHandlers.PickingUpItem -= OnPickingUpItem;
            PlayerHandlers.ReceivingEffect -= OnReceivingEffect;
            PlayerHandlers.RemovingHandcuffs -= OnRemovingHandcuffs;
            PlayerHandlers.Shooting -= OnShooting;
            PlayerHandlers.SpawningRagdoll -= OnSpawningRagdoll;
            PlayerHandlers.StoppingGenerator -= OnStoppingGenerator;
            PlayerHandlers.ThrowingItem -= OnThrowingItem;
            PlayerHandlers.TriggeringTesla -= OnTriggeringTesla;
            PlayerHandlers.UsingItem -= OnUsingItem;
        }

        private void OnActivatingGenerator(ActivatingGeneratorEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnActivatingWarheadPanel(ActivatingWarheadPanelEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnActivatingWorkstation(ActivatingWorkstationEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                plugin.Ghost.RemoveRole(ev.Player);
        }

        private void OnClosingGenerator(ClosingGeneratorEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnDroppingItem(DroppingItemEventArgs ev)
        {
            if (!plugin.Ghost.Check(ev.Player))
                return;

            ev.IsAllowed = false;
            switch (ev.Item.Type)
            {
                case ItemType.Coin:
                    GoTo.Player(ev.Player);
                    break;
                case ItemType.KeycardO5:
                    GoTo.Door(ev.Player);
                    break;
            }
        }

        private void OnDying(DyingEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Target))
            {
                ev.Target.ClearBroadcasts();
                return;
            }

            SpawnPosition.Update(ev.Target);
        }

        private void OnEnteringFemurBreaker(EnteringFemurBreakerEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnEscaping(EscapingEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnHandcuffing(HandcuffingEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Cuffer) || plugin.Ghost.Check(ev.Target))
                ev.IsAllowed = false;
        }

        private void OnHurting(HurtingEventArgs ev)
        {
            if ((ev.Attacker != null && plugin.Ghost.Check(ev.Attacker)) || plugin.Ghost.Check(ev.Target))
                ev.IsAllowed = false;
        }

        private void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (!plugin.Ghost.Check(ev.Player))
                return;

            ev.IsAllowed = false;
            Passthrough.Door(ev.Door, ev.Player);
        }

        private void OnInteractingElevator(InteractingElevatorEventArgs ev)
        {
            if (!plugin.Ghost.Check(ev.Player))
                return;

            ev.IsAllowed = false;
            Passthrough.Elevator(ev.Player, ev.Lift);
        }

        private void OnInteractingLocker(InteractingLockerEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnIntercomSpeaking(IntercomSpeakingEventArgs ev)
        {
            if (ev.Player != null && plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnOpeningGenerator(OpeningGeneratorEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnPickingUpAmmo(PickingUpAmmoEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnPickingUpArmor(PickingUpArmorEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnReceivingEffect(ReceivingEffectEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnRemovingHandcuffs(RemovingHandcuffsEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Cuffer))
                ev.IsAllowed = false;
        }

        private void OnShooting(ShootingEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Shooter))
                ev.IsAllowed = false;
        }

        private void OnSpawningRagdoll(SpawningRagdollEventArgs ev)
        {
            if (ev.Owner != null && plugin.Ghost.Check(ev.Owner))
                ev.IsAllowed = false;
        }

        private void OnStoppingGenerator(StoppingGeneratorEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnThrowingItem(ThrowingItemEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }

        private void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsTriggerable = false;
        }

        private void OnUsingItem(UsingItemEventArgs ev)
        {
            if (plugin.Ghost.Check(ev.Player))
                ev.IsAllowed = false;
        }
    }
}