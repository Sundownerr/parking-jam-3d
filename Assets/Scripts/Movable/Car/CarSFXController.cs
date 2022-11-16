using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Game
{
    public class CarSFXController : MonoBehaviour
    {
        [SerializeField] private AudioSource _collisionSource;
        [SerializeField] private AudioSource _collisionReactionSource;
        [SerializeField] private AudioSource _engineAudioSource;
        [SerializeField] private Vector2 _collisionPitchRange;
        [SerializeField] private Vector2 _reactionPitchRange;
        private float _enginePitch;
        private Coroutine _waitRoutine;

        public void StartEngineSFX()
        {
            _engineAudioSource.Play();
            DOVirtual.Float(0, 2, 2f, value => { _engineAudioSource.pitch = value; })
                .SetId(gameObject.name);
        }

        public void StopEngineSFX()
        {
            DOTween.Kill(gameObject.name);
            _engineAudioSource.Stop();
        }

        public void PlayCollisionSFX()
        {
            _collisionSource.pitch = Random.Range(_collisionPitchRange.x, _collisionPitchRange.y);
            _collisionSource.Play();
        }

        public void PlayOtherCarCollisionReaction()
        {
            if (_waitRoutine != null)
            {
                StopCoroutine(_waitRoutine);
            }

            _waitRoutine = StartCoroutine(Wait(Random.Range(0.1f, 0.3f), () =>
            {
                _collisionReactionSource.pitch = Random.Range(_reactionPitchRange.x, _reactionPitchRange.y);
                _collisionReactionSource.Play();
            }));
        }

        private IEnumerator Wait(float time, Action onComplete)
        {
            yield return new WaitForSeconds(time);
            onComplete?.Invoke();
        }
    }
}