using System;
using UnityEngine;

namespace ProjectDiorama
{
    public class TriggerCheck : MonoBehaviour
    {
        public Action TriggerEnter;
        public Action TriggerExit;
        void OnTriggerEnter(Collider other)
        {
            TriggerEnter?.Invoke();
        }

        void OnTriggerExit(Collider other)
        {
            TriggerExit?.Invoke();
        }
    }
}
