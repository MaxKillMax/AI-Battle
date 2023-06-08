using AIBattle.Events;
using UnityEngine;
using UnityEngine.AI;

namespace AIBattle.LiveObjects.LiveComponents.Movements
{
    /// <summary>
    /// Component allow liveObject to move in unity world
    /// </summary>
    public class Movement : LiveComponent
    {
        private readonly NavMeshAgent _agent;

        public MovementState State { get; private set; } = MovementState.Ended;
        public bool IsStopped { get => _agent.isStopped; set => _agent.isStopped = value; }
        public Transform DestinationTransform { get; private set; }

        public Event<Vector3> OnMoveStarted { get; private set; } = new();
        public Events.Event OnMoveContinued { get; private set; } = new();
        public Events.Event OnMoveStopped { get; private set; } = new();
        public Events.Event OnMoveEnded { get; private set; } = new();

        public Movement(MovementParameters parameters)
        {
            _agent = parameters.Agent;
            _agent.isStopped = true;

            UnityAlternatives.Input.OnUpdate += CheckAgent;
        }

        public override void OnDestroy()
        {
            UnityAlternatives.Input.OnUpdate -= CheckAgent;
        }

        public void MoveToPoint(Vector3 point)
        {
            DestinationTransform = null;

            _agent.SetDestination(point);
            _agent.isStopped = false;

            UpdateState(MovementState.Moving, new(() => OnMoveStarted?.Invoke(point)));
        }

        public void MoveToTransform(Transform transform)
        {
            MoveToPoint(transform.position);
            DestinationTransform = transform;
        }

        public void End()
        {
            DestinationTransform = null;
            UpdateState(MovementState.Ended, OnMoveEnded);
            IsStopped = true;
        }

        private void CheckAgent()
        {
            if (!_agent.isOnNavMesh)
                return;

            switch (State)
            {
                case MovementState.Moving:
                    if (_agent.remainingDistance <= _agent.stoppingDistance)
                        TryEnd();
                    else if (IsStopped)
                        UpdateState(MovementState.Stopped, OnMoveStopped);
                    break;

                case MovementState.Stopped:
                    if (!IsStopped)
                        UpdateState(MovementState.Moving, OnMoveContinued);
                    break;
            }
        }

        private void TryEnd()
        {
            if (DestinationTransform != null && DistanceToDestinationTransform() > _agent.stoppingDistance)
                _agent.destination = DestinationTransform.position;
            else
                End();
        }

        private void UpdateState(MovementState state, Events.Event @event)
        {
            State = state;
            @event?.Invoke();
        }

        private float DistanceToDestinationTransform() => Vector3.Distance(_agent.pathEndPosition, DestinationTransform.position);
    }
}
