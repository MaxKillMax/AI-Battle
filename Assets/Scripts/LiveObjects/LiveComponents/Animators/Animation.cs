using System;

namespace AIBattle.LiveObjects.LiveComponents.Animators
{
    /// <summary>
    /// Used by Animator LiveComponent
    /// </summary>
    [Serializable]
    public struct Animation
    {
        public string Name;
        public ParameterType Type;
        public Action OnExited;
    }
}
