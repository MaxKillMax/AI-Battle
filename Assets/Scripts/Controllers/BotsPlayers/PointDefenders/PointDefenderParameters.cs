using System;
using System.Collections.Generic;
using AIBattle.LiveObjects;
using AIBattle.LiveObjects.LiveComponents.Targets;
using UnityEngine;

namespace AIBattle.Controllers.BotsPlayers.PointDefenders
{
    [Serializable]
    public struct PointDefenderParameters
    {
        public TargetId Id;
        public float ActionDelay;

        public List<LiveObject> LiveObjects;
        public Vector3[] BotPatrolPoints;
    }
}
