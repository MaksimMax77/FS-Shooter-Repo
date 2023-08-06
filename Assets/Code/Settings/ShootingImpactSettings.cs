using System;
using UnityEngine;

namespace Code.Settings
{
    [CreateAssetMenu(fileName = "ShootingImpactSettings", menuName = "ShootingImpactSettings", order = 2)]
    public class ShootingImpactSettings : ScriptableObject
    {
        [SerializeField] private ImpactContainer[] _impactContainers;
        public ImpactContainer[] ImpactContainers => _impactContainers;
        
        [Serializable]
        public struct ImpactContainer
        {
            public GameObject effectPrefab;
            public LayerMask layerMask;
        }
    }
}
