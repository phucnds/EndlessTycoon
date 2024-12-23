using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

namespace EndlessTycoon
{
    [CreateAssetMenu(fileName = "CounterStat", menuName = "CounterStat", order = 0)]
    public class CounterStat : ScriptableObject
    {
        [field: SerializeField] public StatData[] Data { get; private set; }
    }

    [Serializable]
    public class StatData
    {
        public int Income;
        public int Cost;
    }
}