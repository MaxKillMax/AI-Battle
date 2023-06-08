using AIBattle.LiveObjects.LiveComponents.Attacks;

namespace AIBattle.LiveObjects.Listeners
{
    public class AttackListener : LiveComponentListener<Attack>
    {
        protected override void SetListenState(bool state, LiveObject liveObject, Attack component)
        {
            component.OnAttackStarted.SetListenState(state, () => PrepareForBattle(liveObject, component.Target.LiveObject));
        }

        private void PrepareForBattle(LiveObject liveObject, LiveObject target)
        {
            liveObject.Agent.enabled = false;
            liveObject.transform.LookAt(target.transform);
            liveObject.Agent.enabled = true;
        }
    }
}
