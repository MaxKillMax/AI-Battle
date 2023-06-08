using System;

namespace AIBattle.LiveObjects.LiveComponents.Healths
{
    /// <summary>
    /// If action is damage, value need to be negative (-10, -50). If it heal, when positive (10, 50)
    /// </summary>
    [Serializable]
    public struct HealthAction
    {
        public float Value;

        public HealthAction(float value)
        {
            Value = value;
        }

        public static implicit operator HealthAction(float value) => new(value);
    }
}
