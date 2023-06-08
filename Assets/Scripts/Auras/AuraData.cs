using System;
using AIBattle.Generators;
using AIBattle.LiveObjects;
using UnityEngine;

namespace AIBattle.Auras
{
    [CreateAssetMenu(fileName = nameof(Aura), menuName = nameof(Aura), order = 51)]
    public class AuraData : ScriptableObject
    {
        [SerializeField] private Aura _prefab;
        [SerializeField] private Vector3 _scale = Vector3.one;

        public Aura Create(Transform parent, Vector3 position, Func<LiveObject, bool> condition = null)
        {
            Aura aura = new ObjectGenerator<Aura>(_prefab, parent, position).Generate();
            aura.SetScale(_scale);
            aura.SetCondition(condition);
            return aura;
        }
    }
}