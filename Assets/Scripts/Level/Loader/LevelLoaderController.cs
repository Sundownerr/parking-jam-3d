using UnityEngine;

namespace Game
{
    public class LevelLoaderController : MonoBehaviour
    {
        private const string LevelIndexKey = "levelIndex";
        [SerializeField] private LevelLoader _levelLoader;
        [SerializeField] private StartUI _startUI;
        [SerializeField] private FinishUI _finishUI;
        [SerializeField] private InGameUI _inGameUI;
        private int levelIndex;

        public void Start()
        {
            levelIndex = PlayerPrefs.GetInt(LevelIndexKey, 0);

            _startUI.PlayButton.onClick.AddListener(() => { LoadCurrentLevel(); });
            
            _inGameUI.RestartButton.onClick.AddListener(() => { LoadCurrentLevel(); });
            
            _finishUI.RestartButton.onClick.AddListener(() => { LoadCurrentLevel(); });
            
            _finishUI.HomeButton.onClick.AddListener(() =>
            {
                IncreaseLevelIndex();
                _levelLoader.UnloadCurrentLevel();
            });
            _inGameUI.HomeButton.onClick.AddListener(() => { _levelLoader.UnloadCurrentLevel(); });
        }

        private void LoadCurrentLevel()
        {
            _levelLoader.Load(levelIndex);
        }
        
        private void IncreaseLevelIndex()
        {
            levelIndex++;

            if (!_levelLoader.CanLoadLevel(levelIndex))
            {
                levelIndex = 0;
            }

            PlayerPrefs.SetInt(LevelIndexKey, levelIndex);
        }
    }
}