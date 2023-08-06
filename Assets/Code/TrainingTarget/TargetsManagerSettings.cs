using System.Collections.Generic;
using UnityEngine;

namespace Code.TrainingTarget
{
    public class TargetsManagerSettings : MonoBehaviour
    { 
        [SerializeField] private List<TargetView> _views = new(); 
        [SerializeField] private float _respawnTime;

        public List<TargetView> Views => _views;
        public float RespawnTime => _respawnTime;

    }
}
