using AIBattle.LiveObjects.LiveComponents.Aggressions;
using AIBattle.LiveObjects.LiveComponents.Movements;

namespace AIBattle.LiveObjects.Listeners
{
    public class AggressionListener : LiveComponentListener<Aggression>
    {
        protected override void SetListenState(bool state, LiveObject liveObject, Aggression component)
        {
            component.OnEnemyDisappered.SetListenState(state, (l) => TryStop(liveObject, component, l));
        }

        private void TryStop(LiveObject liveObject, Aggression aggression, LiveObject target)
        {
            if (aggression.HasEnemies)
                return;

            if (!liveObject.TryGetLiveComponent(out Movement movement))
                return;

            if (movement.DestinationTransform == target.transform)
                movement.End();
        }
    }
}
