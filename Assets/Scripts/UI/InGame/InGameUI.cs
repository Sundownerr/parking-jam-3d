using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class InGameUI : MonoBehaviour, IUIElement
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _homeButton;

        public Button RestartButton => _restartButton;
        public Button HomeButton => _homeButton;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}