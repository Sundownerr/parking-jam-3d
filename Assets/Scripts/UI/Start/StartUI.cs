using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class StartUI : MonoBehaviour, IUIElement
    {
        [SerializeField] private Button _playButton;

        public Button PlayButton => _playButton;
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