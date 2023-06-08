using System;

namespace AIBattle.LiveObjects.LiveComponents
{
    /// <summary>
    /// Represents the specific logic of a LiveObject
    /// </summary>
    [Serializable]
    public abstract class LiveComponent
    {
        /// <summary>
        /// When LiveObject is destroyed, he call this event
        /// </summary>
        public virtual void OnDestroy() { }
    }
}
