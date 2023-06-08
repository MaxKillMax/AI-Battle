using System;
using System.Collections.Generic;

namespace AIBattle.LiveObjects.LiveComponents.Animators
{
    /// <summary>
    /// Component interact with UnityEngine Animator
    /// </summary>
    public class Animator : LiveComponent
    {
        private const string ANIMATION_END_POSTFIX = "_End";

        private readonly Dictionary<string, Action> _exitAnimationActions = new();

        private UnityEngine.Animator _animator;

        public Animator(AnimatorParameters parameters)
        {
            _animator = parameters.Animator;

            parameters.EventTracker.OnMessageReceived.AddListener(CheckEventMessage);
        }

        public void Animate(Animation animation)
        {
            switch (animation.Type)
            {
                case ParameterType.Trigger:
                    _animator.SetTrigger(animation.Name);
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (animation.OnExited != null)
                _exitAnimationActions.Add(animation.Name + ANIMATION_END_POSTFIX, animation.OnExited);
        }

        private void CheckEventMessage(string message)
        {
            if (!_exitAnimationActions.TryGetValue(message, out Action action))
                return;

            _exitAnimationActions.Remove(message);
            action?.Invoke();
        }
    }
}
