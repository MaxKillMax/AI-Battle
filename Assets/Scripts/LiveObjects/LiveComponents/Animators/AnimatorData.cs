using AIBattle.Animations;
using Unity.VisualScripting;
using UnityEngine;

namespace AIBattle.LiveObjects.LiveComponents.Animators
{
    [CreateAssetMenu(fileName = nameof(Animator), menuName = PathStart + nameof(Animator), order = Order)]
    public class AnimatorData : LiveComponentData
    {
        public override LiveComponent Create(LiveObject liveObject) => new Animator(new()
        {
            Animator = liveObject.Animator,
            EventTracker = liveObject.AddComponent<AnimationEventTracker>()
        });
    }
}
