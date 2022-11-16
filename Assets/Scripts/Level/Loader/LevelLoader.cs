using System;
using UnityEngine;

namespace Game
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Level[] _levels;

        public LevelLoader(Level[] levels)
        {
            _levels = levels;
        }

        public Level LevelInstance { get; private set; }

        public event Action<Level> Loaded;

        public void Load(int index)
        {
            if (LevelInstance != null)
            {
                UnloadCurrentLevel(() => LoadLevel(index));
                return;
            }

            LoadLevel(index);
        }

        public void UnloadCurrentLevel(Action onCompleted = null)
        {
            Destroy(LevelInstance.gameObject);
            Resources.UnloadUnusedAssets().completed += Oncompleted;

            void Oncompleted(AsyncOperation obj)
            {
                Resources.UnloadUnusedAssets().completed -= Oncompleted;
                onCompleted?.Invoke();
            }
        }

        public bool CanLoadLevel(int index)
        {
            return index <= _levels.Length - 1;
        }

        private Level LoadLevel(int index)
        {
            LevelInstance = Instantiate(_levels[index]);

            Loaded?.Invoke(LevelInstance);
            return LevelInstance;
        }
    }
}