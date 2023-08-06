using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Moving
{
    public class Move
    {
        public event Action<bool> Moving;
        private float _speed;
        private CharacterController _characterController;
        private GameObject _gameObject;
        private Vector3 _inputDir;
        private float _gravity = -9f;
        private Vector3 _velocity;

        public Move(float speed, GameObject gameObject)
        {
            _speed = speed;
            _gameObject = gameObject;
            _characterController = gameObject.GetComponent<CharacterController>();

        }
        public void OnMovePerformed(InputAction.CallbackContext context)
        {
            _inputDir = context.ReadValue<Vector2>();
        }

        public void OnMoveCanceled(InputAction.CallbackContext context)
        {
            _inputDir = context.ReadValue<Vector2>();
        }
        
        public void Update()
        {
            var moveDir = _gameObject.transform.right * _inputDir.x + _gameObject.transform.forward * _inputDir.y;
            _characterController.Move(moveDir * _speed * Time.deltaTime);
            _velocity.y += _gravity * Time.deltaTime;
            _characterController.Move(_velocity);
            Moving?.Invoke(moveDir.x != 0 || moveDir.z != 0);
        }
    }
}
