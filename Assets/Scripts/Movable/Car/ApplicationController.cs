using UnityEngine;

namespace Game
{
    public class ApplicationController : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}