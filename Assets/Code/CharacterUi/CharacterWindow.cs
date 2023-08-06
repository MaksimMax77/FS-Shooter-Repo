using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.CharacterUi
{
    public class CharacterWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text _maxBulletsCountText; 
        [SerializeField] private TMP_Text _currentBulletsCountText;
        [SerializeField] Image _healthbarImage;
        
        public void HealthUpdate(float value)
        {
            _healthbarImage.fillAmount = value;
        }

        public void OnBulletsCountUpdate(int maxBullets, int currentCountBullets)
        {
            _maxBulletsCountText.text = maxBullets.ToString();
            _currentBulletsCountText.text = currentCountBullets.ToString();
        }
    }
}
