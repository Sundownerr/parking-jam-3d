using System;
using UnityEngine;

namespace Game
{
    public class CarFinishTrigger : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _vfx;

        private void OnTriggerEnter(Collider other)
        {
            CarFinished?.Invoke(other.gameObject);
            _vfx.Play(true);
        }

        public event Action<GameObject> CarFinished;
    }
}