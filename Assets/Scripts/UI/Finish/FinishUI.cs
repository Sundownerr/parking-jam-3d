using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class FinishUI : MonoBehaviour, IUIElement
    {
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _restartButton;

        public Button HomeButton => _homeButton;

        public Button RestartButton => _restartButton;

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