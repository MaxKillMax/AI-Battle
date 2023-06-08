using System.Collections.Generic;
using AIBattle.LiveObjects.LiveComponents.Healths;
using AIBattle.LiveObjects.LiveComponents.Targets;

namespace AIBattle.LiveObjects.LiveComponents.Attacks
{
    /// <summary>
    /// Can attack other liveObject
    /// </summary>
    public partial class Attack : LiveComponent
    {
        private ComponentAction<Health> _processTarget;
        private bool _inProcess;

        public HealthAction Action { get; private set; }

        private readonly List<ComponentAction<Health>> _targets = new(1);
        public ComponentAction<Health> Target => _targets.Count > 0 ? _targets[0] : default;

        public Events.Event OnAttackStarted { get; private set; } = new();
        public Events.Event OnAttackEnded { get; private set; } = new();

        public Attack(AttackParameters parameters)
        {
            Action = parameters.Action;

            OnAttackStarted = parameters.OnAttackStarted;
            parameters.OnAttackCompleted.AddListener(EndProcess);
            parameters.OnEnemyAppeared.AddListener(AddTarget);
            parameters.OnEnemyDisappered.AddListener(RemoveTarget);
        }

        public override void OnDestroy()
        {
            if (_targets.Count > 0)
                UnityAlternatives.Input.OnUpdate -= TryAttack;
        }

        private void EndProcess()
        {
            if (_targets.Contains(_processTarget))
                _processTarget.Component.Act(Action);

            _inProcess = false;
            OnAttackEnded?.Invoke();
        }

        private void AddTarget(LiveObject liveObject)
        {
            _targets.Add(new()
            {
                LiveObject = liveObject,
                Target = liveObject.GetLiveComponent<Target>(),
                Component = liveObject.GetLiveComponent<Health>()
            });

            if (_targets.Count == 1)
                UnityAlternatives.Input.OnUpdate += TryAttack;
        }

        private void RemoveTarget(LiveObject liveObject)
        {
            _targets.RemoveAll((a) => a.LiveObject == liveObject);

            if (_targets.Count == 0)
                UnityAlternatives.Input.OnUpdate -= TryAttack;
        }

        private void TryAttack()
        {
            if (_targets.Count == 0 || _inProcess)
                return;

            if (Target.LiveObject == null)
            {
                RemoveTarget(null);
                return;
            }

            _processTarget = Target;
            _inProcess = true;

            OnAttackStarted.Invoke();
        }
    }

    public partial class Attack
    {
        public static void ImproveDamage(Attack attack, HealthAction addValue) => attack.Action = new(attack.Action.Value + addValue.Value);
    }
}