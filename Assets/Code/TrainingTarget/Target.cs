using System;
using UnityEngine;

namespace Code.TrainingTarget
{
    public class Target
    {
        public event Action<object, bool>Death;
        private float _health = 100;
        
        public void HealthRemove(float value)
        {
            _health -= value;
            if (_health <= 0)
            {
                _health = 0;
                Death?.Invoke(this, true);
            }
        }

        public void Refresh()
        {
            _health = 100;
            Death?.Invoke(this, false);
        }
    }
}
