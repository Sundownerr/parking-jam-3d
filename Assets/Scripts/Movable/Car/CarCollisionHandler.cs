using System;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class CarCollisionHandler : MonoBehaviour
    {
        public void HandleCollision(CollisionEvent e)
        {
            DOTween.Kill(transform, true);

            var rotation = transform.InverseTransformDirection(e.Direction);
            (rotation.x, rotation.z) = (-rotation.z, rotation.x);

            transform.DOPunchRotation(rotation * 7f, 0.3f, 8).SetId(transform);

            if (CheckIsHitFromSides(e.DotProduct))
            {
                return;
            }

            var moveDireciton = transform.forward * MathF.Sign(-e.DotProduct);

            transform.DOLocalMove(transform.localPosition + moveDireciton * 0.3f, 0.2f)
                .SetId(transform)
                .SetUpdate(UpdateType.Fixed);
        }

        private bool CheckIsHitFromSides(float dotProduct)
        {
            const float treshold = 0.9f;

            if (dotProduct < 0)
            {
                return dotProduct > -treshold;
            }

            return dotProduct < treshold;
        }
    }
}