using System;
using UnityEngine;

namespace ProjectDiorama
{
    public class TriggerCheck : MonoBehaviour
    {
        public Action<Collider> TriggerEnter;
        public Action<Collider> TriggerExit;
        public Action<Collider> TriggerStay;
        void OnTriggerEnter(Collider other)
        {
            TriggerEnter?.Invoke(other);
        }

        void OnTriggerExit(Collider other)
        {
            TriggerExit?.Invoke(other);
        }

        void OnTriggerStay(Collider other)
        {
            TriggerStay?.Invoke(other);
        }
    }
}
