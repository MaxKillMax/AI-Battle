using System;
using System.Collections.Generic;
using AIBattle.LiveObjects;
using AIBattle.LiveObjects.LiveComponents.Targets;
using AIBattle.UnityAlternatives;
using UnityEngine.Assertions;

namespace AIBattle.Controllers.BotsPlayers
{
    /// <summary>
    /// Implementing artificial intelligence control
    /// </summary>
    public abstract class BotsPlayer : Controller
    {
        private readonly float _actionDelay;
        private float _delay;

        public BotsPlayer(TargetId id, float actionDelay, List<LiveObject> liveObjects) : base(id, liveObjects)
        {
            Assert.IsTrue(actionDelay > 0);

            _actionDelay = actionDelay;
            _delay = actionDelay;

            Input.OnUpdate += Update;
        }

        private void Update()
        {
            _delay -= Time.Delta;

            if (_delay >= 0)
                return;

            _delay = _actionDelay;
            Act();
        }

        private void Act()
        {
            if (LiveObjects.Count == 0)
                return;

            LiveObjects = SortByPriority(LiveObjects);

            Action action = GetBestActionOf(LiveObjects[0]);
            action?.Invoke();
        }

        public abstract List<LiveObject> SortByPriority(IEnumerable<LiveObject> liveObjects);

        public abstract Action GetBestActionOf(LiveObject liveObject);
    }
}
