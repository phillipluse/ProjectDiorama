using System;
using UnityEngine;

namespace ProjectDiorama
{
    public class ConnectionPoint : MonoBehaviour
    {
        [SerializeField] TriggerCheck _triggerCheck;

        void OnEnable()
        {
            _triggerCheck.TriggerEnter += OnTriggerEntered;
            _triggerCheck.TriggerExit += OnTriggerExited;
        }

        void OnTriggerExited()
        {
            Debug.Log($"{gameObject.name}: exit");
        }

        void OnTriggerEntered()
        {
            Debug.Log($"{gameObject.name}: enter");
        }
    }
}
