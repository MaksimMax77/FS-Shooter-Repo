using UnityEngine;

namespace Code.Animations
{
    public class AnimationsControl : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _shootTransitionName;
        [SerializeField] private string _moveTransitionName;
        [SerializeField] private string _reloadTransitionName;

        public void ShootSet(bool value)
        {
            _animator.SetBool(_shootTransitionName, value);
        }

        public void MoveSet(bool value)
        {
            _animator.SetBool(_moveTransitionName, value);
        }

        public void ReloadSet()
        {
            _animator.SetTrigger(_reloadTransitionName);
        }
    }
}
