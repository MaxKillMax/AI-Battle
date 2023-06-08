using System;
using UnityEngine;

namespace AIBattle.LiveObjects
{
    [Serializable]
    public struct LiveObjectSpawnData
    {
        public LiveObjectData Data;
        public Transform Parent;
        public Vector3 Position;
        public Quaternion Rotation;
    }
}
