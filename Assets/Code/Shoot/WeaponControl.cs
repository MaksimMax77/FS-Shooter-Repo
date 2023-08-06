using System;
using Code.Settings;
using Code.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Shoot
{
    public class WeaponControl
    {
        public event Action<bool> Shoot;
        public event Action Reload;
        private Camera _camera;
        private Transform _characterTransform;
        private Timer _timer;
        private ShootImpactControl _shootImpactControl;
        private bool _isShoot;
        private Weapon _weapon;

        public WeaponControl(Camera camera, Transform characterTransform, ShootingImpactSettings.ImpactContainer[] containers, Weapon weapon)
        {
            _camera = camera;
            _timer = new Timer(weapon.Cooldown);
            _characterTransform = characterTransform;
            _shootImpactControl = new ShootImpactControl(containers);
            _weapon = weapon;
        }

        public void Update()
        {
            _timer.UpdateTimer();

            _weapon.SetFlash(_isShoot && _weapon.CurrentBulletsCount > 0);
            
            if (_isShoot && _timer.available && _weapon.CurrentBulletsCount > 0)
            {
                RayCast();
                _timer.TimerZero();
                _weapon.Shoot();
            }
        }

        
        public void OnShootPerformed(InputAction.CallbackContext context)
        {
            _isShoot = true;
            Shoot?.Invoke(true);
        }
        
        public void OnShootCanceled(InputAction.CallbackContext context)
        {
            _isShoot = false;
            Shoot?.Invoke(false);
        }

        public void OnReloadPerformed(InputAction.CallbackContext context)
        {
            _weapon.Reload();
            Reload?.Invoke();
        }
        
        private void RayCast()
        {
            var ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out var hit))
            {
                _shootImpactControl.Impact(hit.point, _characterTransform.position, hit.collider.gameObject.layer);
                var hitObj = hit.collider.gameObject.GetComponent<IHit>();
                if (hitObj == null)
                {
                    return;
                }
                hitObj.Hit(_weapon.Damage);
            }
        }
    }
}
