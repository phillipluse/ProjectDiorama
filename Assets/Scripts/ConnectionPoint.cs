using UnityEngine;

namespace ProjectDiorama
{
    public class ConnectionPoint : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] TriggerCheck _triggerCheck;
        [SerializeField] ProductionLoopComponent _productionLoopComponent;

        [Header("Properties")]
        [SerializeField] ConnectionType _connectionType;
        [SerializeField] LayerMask _connectionPointLayerMask;

        public ConnectionStatus Status { get; private set; }

        void OnEnable()
        {
            _triggerCheck.TriggerEnter += OnTriggerEntered;
            _triggerCheck.TriggerExit += OnTriggerExited;
            Status = ConnectionStatus.Disconnected;
        }
        
        void OnDisable()
        {
            _triggerCheck.TriggerEnter -= OnTriggerEntered;
            _triggerCheck.TriggerExit -= OnTriggerExited;
            Status = ConnectionStatus.Disconnected;
        }

        public void Connect()
        {
            Status = ConnectionStatus.Connected;
            _productionLoopComponent.UpdateVisual();
        }

        public void Disconnect()
        {
            Status = ConnectionStatus.Disconnected;
            _productionLoopComponent.UpdateVisual();
        }

        public bool IsCompatibleWith(ConnectionPoint cp) => _connectionType.IsCompatibleWith(cp.ConnectionType);

        void OnTriggerEntered(Collider other)
        {
            if (!_connectionPointLayerMask.Contains(other.gameObject.layer)) return;
            if (other.gameObject.TryGetComponent(out ConnectionPoint otherCp))
            {
                _productionLoopComponent.OnConnectionPointTriggered(this, otherCp);
            }
        }

        void OnTriggerExited(Collider other)
        {
            if (!_connectionPointLayerMask.Contains(other.gameObject.layer)) return;
            if (other.gameObject.TryGetComponent(out ConnectionPoint otherCp))
            {
                _productionLoopComponent.OnConnectionPointUnTriggered(this, otherCp);
            }
        }

        public ConnectionType ConnectionType => _connectionType;
    }
}
