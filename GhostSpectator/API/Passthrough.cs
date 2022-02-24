// -----------------------------------------------------------------------
// <copyright file="Passthrough.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace GhostSpectator.API
{
    using System.Collections.Generic;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using Exiled.API.Structs;
    using MEC;
    using UnityEngine;

    /// <summary>
    /// Passes players through objects seamlessly.
    /// </summary>
    public static class Passthrough
    {
        private static readonly List<int> ElevatorUsers = new List<int>();

        /// <summary>
        /// Moves a player to the other side of a door.
        /// </summary>
        /// <param name="door">The door to go through.</param>
        /// <param name="player">The player to move.</param>
        public static void Door(Door door, Player player)
        {
            float y;
            if (IsCheckpoint(door))
            {
                y = player.Position.y;
                Vector3 position = player.Position + (player.CameraTransform.forward * (5f + Vector3.Distance(player.Position, door.Position)));
                position.y = y;
                player.Position = position;
                return;
            }

            Vector3 doorForward = door.Base.transform.forward;
            Vector3 doorForwardByFive = doorForward * 5f;
            Vector3 doorPosition = CalculateDoorPosition(door, player);
            y = player.Position.y;
            Vector3 pos = doorPosition + (Vector3.Distance(player.Position, doorPosition + doorForwardByFive) > Vector3.Distance(player.Position, doorPosition - doorForwardByFive)
                ? doorForward
                : -doorForward);

            pos.y = y;
            player.Position = pos;
        }

        public static void Elevator(Player player, Lift lift)
        {
            if (ElevatorUsers.Contains(player.Id))
                return;

            float furthestLiftDistance = -1f;
            Transform furthestLift = null;

            foreach (Elevator elevator in lift.Elevators)
            {
                float objectDistance = Vector3.Distance(player.Position, elevator.Target.position);
                if (objectDistance > furthestLiftDistance)
                {
                    furthestLift = elevator.Target;
                    furthestLiftDistance = objectDistance;
                }
            }

            if (furthestLift != null)
            {
                player.Position = furthestLift.position + (furthestLift.right * 5);
            }

            ElevatorUsers.Add(player.Id);
            Timing.CallDelayed(0.1f, () => ElevatorUsers.Remove(player.Id));
        }

        private static Vector3 CalculateDoorPosition(Door door, Player player)
        {
            Vector3 doorPosition = door.Position;
            Vector3 doorForwardByFive = door.Base.transform.forward * 5f;
            if (!string.IsNullOrEmpty(door.Nametag) && door.Nametag.Contains("Airlocks"))
            {
                var forward = door.Base.transform.forward;
                doorPosition += (Vector3.Distance(player.Position, doorPosition + doorForwardByFive) < Vector3.Distance(player.Position, doorPosition - doorForwardByFive))
                    ? forward
                    : -forward * 4f;
            }

            return doorPosition;
        }

        private static bool IsCheckpoint(Door door)
        {
            switch (door.Type)
            {
                case DoorType.CheckpointEntrance:
                case DoorType.CheckpointLczA:
                case DoorType.CheckpointLczB:
                    return true;
                default:
                    return false;
            }
        }
    }
}