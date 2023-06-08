using UnityEngine;

namespace AIBattle.LiveObjects.LiveComponents
{
    public abstract class LiveComponentData : ScriptableObject
    {
        protected const int Order = 52;
        protected const string PathStart = "LiveComponents/";

        public int InitOrder = 0;

        public abstract LiveComponent Create(LiveObject liveObject);
    }
}
