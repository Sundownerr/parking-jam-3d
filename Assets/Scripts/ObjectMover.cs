﻿using UnityEngine;

namespace Game
{
    public class ObjectMover : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _carMask;
        [SerializeField] private PlayerInputController _playerInputController;

        private void Start()
        {
            _playerInputController.FingerPressed += PlayerInputControllerOnFingerPressed;
        }

        private void PlayerInputControllerOnFingerPressed(Touch touch)
        {
            var ray = _camera.ScreenPointToRay(touch.position);
            var isHit = Physics.Raycast(ray, out var hitInfo, 1000, _carMask.value);

            if (!isHit)
            {
                return;
            }

            if (!hitInfo.transform.TryGetComponent<Movable>(out var movable))
            {
                return;
            }

            var delta = touch.deltaPosition.normalized;
            var convertedDirection = new Vector3(delta.x, 0, delta.y);

            movable.Move(convertedDirection);
        }
    }
}