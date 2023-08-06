using System;
using Code.Shoot;
using UnityEngine;

namespace Code.TrainingTarget
{
    public class TargetView : MonoBehaviour, IHit
    {
        public event Action<float> Hited;
        [SerializeField] private Animator _animator;
        [SerializeField] private string _deathTransitionName;
        [SerializeField] private string _hitTransitionName;
      
        public void Hit(float value)
        {
            _animator.SetTrigger(_hitTransitionName);
            Hited?.Invoke(value);
        }

        public void SetDeath(object o, bool isDeath)
        {
            _animator.SetBool(_deathTransitionName, isDeath);
            _animator.SetBool(_hitTransitionName, false);
        }
    }
}
