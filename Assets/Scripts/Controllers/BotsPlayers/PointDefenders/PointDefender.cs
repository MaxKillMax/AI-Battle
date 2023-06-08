using System;
using System.Collections.Generic;
using System.Linq;
using AIBattle.LiveObjects;
using AIBattle.LiveObjects.LiveComponents.Aggressions;
using AIBattle.LiveObjects.LiveComponents.Attacks;
using AIBattle.LiveObjects.LiveComponents.HealDamageImproves;
using AIBattle.LiveObjects.LiveComponents.Healths;
using AIBattle.LiveObjects.LiveComponents.Movements;
using UnityEngine;

namespace AIBattle.Controllers.BotsPlayers.PointDefenders
{
    /// <summary>
    /// Bots that protect a certain point
    /// </summary>
    public class PointDefender : BotsPlayer
    {
        private const float NORMALIZED_DANGEROUS_HEALTH_PERCENT = 0.5f;

        private Vector3[] _botPatrolPoints;

        private int _currentPatrolIndex = -1;

        private Vector3 CurrentPatrolPoint
        {
            get
            {
                _currentPatrolIndex++;

                if (_currentPatrolIndex >= _botPatrolPoints.Length)
                    _currentPatrolIndex = 0;

                return _botPatrolPoints[_currentPatrolIndex];
            }
        }

        public PointDefender(PointDefenderParameters parameters) : base(parameters.Id, parameters.ActionDelay, parameters.LiveObjects)
        {
            _botPatrolPoints = parameters.BotPatrolPoints;
        }

        public override Action GetBestActionOf(LiveObject liveObject)
        {
            GetBestActionOf(liveObject, out Action action);
            return action;
        }

        private int GetBestActionOf(LiveObject liveObject, out Action action)
        {
            if (liveObject.TryGetLiveComponent(out HealDamageImprove improve) && improve.Time <= 0)
            {
                if (liveObject.TryGetLiveComponent(out Health health) && health.Amount <= health.MaxAmount * NORMALIZED_DANGEROUS_HEALTH_PERCENT)
                {
                    action = () => improve.TryHeal();
                    return 500;
                }
                else
                {
                    action = () => improve.TryDamage();
                    return 450;
                }
            }

            if (liveObject.TryGetLiveComponent(out Aggression aggression)
                && aggression.HasEnemies
                && liveObject.TryGetLiveComponent(out Attack attack)
                && attack.Target.LiveObject == null
                && liveObject.TryGetLiveComponent(out Movement movement)
                && movement.DestinationTransform != aggression.LiveObjects[0].transform)
            {
                action = () => movement.MoveToTransform(aggression.LiveObjects[0].transform);
                return 400;
            }

            if (liveObject.TryGetLiveComponent(out Movement patrolMovement) && patrolMovement.State == MovementState.Ended)
            {
                action = () => patrolMovement.MoveToPoint(CurrentPatrolPoint);
                return 300;
            }

            action = null;
            return 0;
        }

        public override List<LiveObject> SortByPriority(IEnumerable<LiveObject> liveObjects) => liveObjects.OrderByDescending((l) => GetBestActionOf(l, out _)).ToList();
    }
}
