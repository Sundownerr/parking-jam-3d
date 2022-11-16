using System;
using UnityEngine;


namespace Game
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Car[] _cars;
        [SerializeField] private CarFinishTrigger _carFinishTrigger;
        private int totalCarsFinished;

        private void Start()
        {
            _carFinishTrigger.CarFinished += OnCarFinished;
        }

        private void OnDestroy()
        {
            _carFinishTrigger.CarFinished -= OnCarFinished;
        }

        public event Action Finished;

        private void OnCarFinished(GameObject car)
        {
            Destroy(car, 2f);
            totalCarsFinished++;

            if (totalCarsFinished >= _cars.Length)
            {
                Finished?.Invoke();
            }
        }
    }
}