using UnityEngine;

namespace Code.Settings
{
    [CreateAssetMenu(fileName = "CharacterSettings", menuName = "CharacterSettings", order = 1)]
    public class CharacterSettings : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] float _rotationSensitivity = 2f;
        [SerializeField] float _yRotationLimit = 88f;

        public float Speed => _speed;
        public float RotationSensitivity => _rotationSensitivity;
        public float YRotationLimit => _yRotationLimit;
    }
}
