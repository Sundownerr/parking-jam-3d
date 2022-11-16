using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Obstacle : MonoBehaviour
    {
        public void HandleCollision()
        {

            transform.DOKill(true);
            transform.DOPunchRotation(Vector3.right * 9f, 0.15f, 5, 0.5f);
        }
    }
}