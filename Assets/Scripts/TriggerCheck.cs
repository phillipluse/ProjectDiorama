using System;
using UnityEngine;

namespace ProjectDiorama
{
    public class TriggerCheck : MonoBehaviour
    {
        public Action<Collider> TriggerEnter;
        public Action<Collider> TriggerExit;
        void OnTriggerEnter(Collider other)
        {
            TriggerEnter?.Invoke(other);
        }

        void OnTriggerExit(Collider other)
        {
            TriggerExit?.Invoke(other);
        }
    }
}
