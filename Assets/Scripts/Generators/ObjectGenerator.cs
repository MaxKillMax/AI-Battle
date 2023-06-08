using UnityEngine;

namespace AIBattle.Generators
{
    /// <summary>
    /// Allows you to create a prefab on the stage with a specified parent and position
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectGenerator<T> : IGenerator<T> where T : Object
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private Vector3 _position;

        public ObjectGenerator(T prefab, Transform parent, Vector3 position)
        {
            _prefab = prefab;
            _parent = parent;
            _position = position;
        }

        public T Generate() => Object.Instantiate(_prefab, _position, Quaternion.identity, _parent);
    }
}
