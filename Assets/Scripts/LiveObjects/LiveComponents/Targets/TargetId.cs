using System;
using System.Linq;
using UnityEngine;

namespace AIBattle.LiveObjects.LiveComponents.Targets
{
    /// <summary>
    /// Allows you to set and define a target
    /// </summary>
    [Serializable]
    public struct TargetId
    {
        [SerializeField] private uint[] _value;

        public TargetId(params uint[] value)
        {
            _value = value;

            if (_value != null)
                _value = _value.OrderBy((value) => value).ToArray();
        }

        public bool IsSingle(out uint first)
        {
            first = _value[0];
            return _value.Length == 1;
        }

        public bool Contains(TargetId targetId)
        {
            if (targetId._value == null || _value == null)
                return false;

            for (int x = 0; x < _value.Length; x++)
            {
                uint value = _value[x];

                if (value < targetId._value[0])
                    break;
                else if (value > targetId._value[^1])
                    return false;

                for (int i = targetId._value.Length - 1; i >= 0; i--)
                {
                    if (targetId._value[i] == value)
                        return true;
                }
            }

            return false;
        }

        public static implicit operator TargetId(uint value) => new(value);

        public static explicit operator TargetId(int value) => new(Convert.ToUInt32(value));

        public static implicit operator uint[](TargetId id) => id._value;
    }
}
