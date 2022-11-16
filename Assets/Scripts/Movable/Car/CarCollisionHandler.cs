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

            var isHitFromSide = Mathf.Abs(e.DotProduct) < 0.5f;

            if (isHitFromSide)
            {
                return;
            }

            var moveDireciton = transform.forward * -e.DotProduct;

            transform.DOLocalMove(transform.localPosition + moveDireciton * 0.3f, 0.2f)
                .SetId(transform)
                .SetUpdate(UpdateType.Fixed);
        }
    }
}