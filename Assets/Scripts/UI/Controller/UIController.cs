using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private StartUI _startUI;
        [SerializeField] private FinishUI _finishUI;
        [SerializeField] private InGameUI _inGameUI;
        [SerializeField] private LevelLoader _levelLoader;

        private void Start()
        {
            _inGameUI.Hide();
            _finishUI.Hide();

            _levelLoader.Loaded += LevelLoaderOnLoaded;

            _startUI.PlayButton.onClick.AddListener(() =>
            {
                _startUI.Hide();
                _inGameUI.Show();
            });

            _finishUI.HomeButton.onClick.AddListener(() =>
            {
                _startUI.Show();
                _finishUI.Hide();
            });

            _finishUI.RestartButton.onClick.AddListener(() =>
            {
                _finishUI.Hide();
                _inGameUI.Show();
            });

            _inGameUI.HomeButton.onClick.AddListener(() =>
            {
                _inGameUI.Hide();
                _startUI.Show();
            });
        }

        private void LevelLoaderOnLoaded(Level level)
        {
            level.Finished += OnFinished;

            void OnFinished()
            {
                level.Finished -= OnFinished;

                StartCoroutine(Wait(2f, () =>
                {
                    _inGameUI.Hide();
                    _finishUI.Show();
                }));
            }
        }

        private IEnumerator Wait(float time, Action onComplete)
        {
            yield return new WaitForSeconds(time);
            onComplete?.Invoke();
        }
    }
}