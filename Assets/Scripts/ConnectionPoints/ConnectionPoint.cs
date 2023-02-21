using UnityEngine;

namespace ProjectDiorama
{
    public abstract class ConnectionPoint : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] protected ConnectionPointComponent connectionPointComponent;

        [Header("Properties")]
        [SerializeField] protected ConnectionType _connectionType;
        [SerializeField] protected LayerMask _connectionPointLayerMask;

        public ConnectionStatus Status { get; private set; }

        protected ConnectionPoint ConnectedPoint;
        protected int RaycastDirection;

        void OnEnable()
        {
            Status = ConnectionStatus.Disconnected;
        }
        
        void OnDisable()
        {
            Status = ConnectionStatus.Disconnected;
        }

        public abstract void Init();

        public abstract void Tick();

        public void Connect(ConnectionPoint connectedPoint)
        {
            Status = ConnectionStatus.Connected;
            ConnectedPoint = connectedPoint;
            connectionPointComponent.UpdateVisual();
        }

        public void Disconnect()
        {
            if (!IsConnected) return;
            Status = ConnectionStatus.Disconnected;
            ConnectedPoint.Disconnect();
            ConnectedPoint = null;
            connectionPointComponent.UpdateVisual();
        }

        public void UnSnap()
        {
            Disconnect();
        }

        protected bool TrySnap(ConnectionPoint connectedPoint)
        {
            return connectionPointComponent.TrySnapBaseObject(this, connectedPoint);
        }

        protected void CheckConnectionPoint()
        {
            if (IsRayCastHit(out RaycastHit hit))
            {
                if (IsConnected) return; //could check that what is hit matches connected...
                if (!hit.transform.gameObject.TryGetComponent(out ConnectionPoint otherCp)) return;
                if (otherCp.IsConnected) return;
                if (!IsCompatibleWith(otherCp)) return;
                if (!AreConnectionsPointsFacingEachOther(this, otherCp)) return;
                if (!TrySnap(otherCp)) return;
                Connect(otherCp);
                otherCp.Connect(this);
                return;
            }

            Disconnect();
        }

        bool AreConnectionsPointsFacingEachOther(ConnectionPoint cp, ConnectionPoint otherCp)
        {
            var dot = Vector3.Dot(cp.transform.forward, otherCp.transform.forward);
            return dot.IsApproximateTo(-1);
        }

        protected bool IsRayCastHit(out RaycastHit hit)
        {
            var maxDistance = 0.3f;
            var t = transform;
            var pos = t.position;
            var fwd = t.forward;
            var ray = new Ray(pos + -fwd * maxDistance / 2, fwd);
            Debug.DrawRay(pos + -fwd * maxDistance / 2, fwd * maxDistance, Color.magenta);
            if (Physics.Raycast(ray, out hit,maxDistance * 2,_connectionPointLayerMask))
            {
                Debug.Log($"hit");
                return true;
            }

            return false;
        }
        
        public bool IsCompatibleWith(ConnectionPoint cp) => _connectionType.IsCompatibleWith(cp.ConnectionType);
        public ConnectionType ConnectionType => _connectionType;
        public bool IsConnected => Status.IsConnected();
    }
}
