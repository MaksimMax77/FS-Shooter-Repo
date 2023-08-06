using System;
using UnityEngine;

namespace Code.Shoot
{
    public class Weapon : MonoBehaviour
    {
        public event Action<int, int> BulletsCountUpdate;
        [SerializeField] private int _maxBulletsCount;
        [SerializeField] private GameObject _flash;
        [SerializeField] private GameObject _casePrefab; 
        [SerializeField] private Transform _caseCreationPos; 
        [SerializeField] private float _cooldown;
        [SerializeField] private int _damage;
        private int _currentBulletsCount;

        public int CurrentBulletsCount => _currentBulletsCount;
        public int Damage => _damage;
        
        public float Cooldown => _cooldown;
        public void SetFlash(bool value)
        {
            _flash.gameObject.SetActive(value);
        }
   
        public void Shoot()
        {
            Instantiate(_casePrefab, _caseCreationPos.transform.position, Quaternion.identity);
            BulletDecrement();
        }
    
        public void Reload()
        {
            _currentBulletsCount = _maxBulletsCount;
            BulletsCountUpdate?.Invoke(_maxBulletsCount, _currentBulletsCount);
        }
    
        private void BulletDecrement()
        {
            --_currentBulletsCount;
            if(_currentBulletsCount <= 0)
            {
                _currentBulletsCount = 0;
            }
            BulletsCountUpdate?.Invoke(_maxBulletsCount, _currentBulletsCount);
        }
    }
}
