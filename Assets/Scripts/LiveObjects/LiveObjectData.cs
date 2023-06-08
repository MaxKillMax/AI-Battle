using System.Collections.Generic;
using System.Linq;
using AIBattle.LiveObjects.LiveComponents;
using UnityEngine;

namespace AIBattle.LiveObjects
{
    [CreateAssetMenu(fileName = nameof(LiveObjectData), menuName = nameof(LiveObjectData), order = 51)]
    public class LiveObjectData : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private LiveObject _prefab;
        [SerializeField] private List<LiveComponentData> _liveComponentDatas;

        private void OnValidate()
        {
            _liveComponentDatas = _liveComponentDatas.OrderBy((d) => (d == null) ? int.MinValue : d.InitOrder).ToList();
        }

        public LiveObject Create(Transform parent, Vector3 position, Quaternion rotation)
        {
            LiveObject liveObject = Instantiate(_prefab, position, rotation, parent);
            liveObject.name = _title;

            for (int i = 0; i < _liveComponentDatas.Count; i++)
            {
                LiveComponent liveComponent = _liveComponentDatas[i].Create(liveObject);
                liveObject.TryAddLiveComponent(liveComponent);
            }

            return liveObject;
        }
    }
}
