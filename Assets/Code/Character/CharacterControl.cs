using System;
using Code.Animations;
using Code.CharacterUi;
using Code.Moving;
using Code.Settings;
using Code.Shoot;
using Code.Update;
using UnityEngine;
using Zenject;

namespace Code.Character
{
    public class CharacterControl: IDisposable, IUpdate
    {
        private UpdateManager _updateManager;
        private InputControls _inputControls;
        private Move _move;
        private AnimationsControl _animationsControl;
        private Rotation _rotation;
        private WeaponControl _shooting;
        private Weapon _weapon;
        private CharacterWindow _characterWindow;

        [Inject]
        public CharacterControl(Camera unitCamera, GameObject unitObject, UpdateManager updateManager, 
            CharacterSettings characterSettings, ShootingImpactSettings shootingImpactSettings, Weapon weapon, CharacterWindow characterWindow)
        {
            _updateManager = updateManager;
            _weapon = weapon;
            _characterWindow = characterWindow;
            _updateManager.AddUpdate(this);
            _inputControls = new InputControls();
            _move = new Move(characterSettings.Speed, unitObject);
            _animationsControl = unitObject.GetComponent<AnimationsControl>();
            _rotation = new Rotation(characterSettings.RotationSensitivity, characterSettings.YRotationLimit,unitObject.transform, unitCamera);
            _shooting = new WeaponControl(unitCamera, unitObject.transform, shootingImpactSettings.ImpactContainers, _weapon);
          
            _weapon.BulletsCountUpdate += _characterWindow.OnBulletsCountUpdate;
            _weapon.Reload();

            _inputControls.ActionMap.Move.performed += _move.OnMovePerformed;
            _inputControls.ActionMap.Move.canceled += _move.OnMoveCanceled;
            _move.Moving += _animationsControl.MoveSet;
            _inputControls.ActionMap.Rotation.performed += _rotation.OnRotatePerformed;
            _inputControls.ActionMap.Shoot.performed += _shooting.OnShootPerformed;
            _inputControls.ActionMap.Shoot.canceled += _shooting.OnShootCanceled;
            _shooting.Shoot += _animationsControl.ShootSet;
            _inputControls.ActionMap.Reload.performed += _shooting.OnReloadPerformed;
            _shooting.Reload +=_animationsControl.ReloadSet;
         
        }

        public void Update()
        {
            _inputControls.Enable();
            _move.Update();
            _rotation.Update();
            _shooting.Update();
        }
        
        public void Dispose()
        {
            _updateManager.RemoveUpdate(this);
            _inputControls.Disable();
            _inputControls.ActionMap.Move.performed -= _move.OnMovePerformed;
            _inputControls.ActionMap.Move.canceled -= _move.OnMoveCanceled;
            _move.Moving -= _animationsControl.MoveSet;
            _inputControls.ActionMap.Rotation.performed -= _rotation.OnRotatePerformed;
            _inputControls.ActionMap.Shoot.performed -= _shooting.OnShootPerformed;
            _inputControls.ActionMap.Shoot.canceled -= _shooting.OnShootCanceled;
            _shooting.Shoot -= _animationsControl.ShootSet;
            _inputControls.ActionMap.Reload.performed -= _shooting.OnReloadPerformed;
            _shooting.Reload -=_animationsControl.ReloadSet;
            _weapon.BulletsCountUpdate -= _characterWindow.OnBulletsCountUpdate;
        }
    }
}
