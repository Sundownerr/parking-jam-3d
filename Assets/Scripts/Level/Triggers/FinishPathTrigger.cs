using Cinemachine;
using UnityEngine;

namespace Game
{
    public class FinishPathTrigger : MonoBehaviour
    {
        [SerializeField] private CinemachinePathBase _path;

        public CinemachinePathBase Path => _path;
    }
}