using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Moving
{
    public class Rotation
    {
        private float _sensitivity;
        private float _yRotationLimit;
        private Transform _bodyTransform; 
        private Transform _cameraTransform; 
        private Vector2 _rotation = Vector2.zero;

        public Rotation(float sensitivity, float yRotationLimit, Transform bodyTransform, Camera camera)
        {
            _sensitivity = sensitivity;
            _yRotationLimit = yRotationLimit;
            _bodyTransform = bodyTransform;
            _cameraTransform = camera.transform;
        }

        public void Update()
        {
            var xQuat = Quaternion.AngleAxis(_rotation.x, Vector3.up);
            var yQuat = Quaternion.AngleAxis(_rotation.y, Vector3.left);

            _cameraTransform.localRotation = yQuat;
            _bodyTransform.rotation = xQuat;
        }
        
        public void OnRotatePerformed(InputAction.CallbackContext context)
        {
            _rotation.x += context.ReadValue<Vector2>().x * _sensitivity;
            _rotation.y += context.ReadValue<Vector2>().y * _sensitivity;
            _rotation.y = Mathf.Clamp(_rotation.y, -_yRotationLimit, _yRotationLimit);
        }
    }
}
