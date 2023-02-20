using System.Collections.Generic;
using UnityEngine;

namespace ProjectDiorama
{
    public class ProductionLoopComponent : MonoBehaviour, INonGridObjectModule
    {
        [Header("References")]
        [SerializeField] List<ConnectionPoint> _connectionPoints;
        [SerializeField] MeshRenderer _renderer;

        [Header("Properties")]
        [SerializeField] Material _nonConnected;
        [SerializeField] Material _bothConnected;
        [SerializeField] Material _inputConnected;
        [SerializeField] Material _outputConnected;

        BaseObject _baseObject;
        
        public void Init(BaseObject baseObject)
        {
            _baseObject = baseObject;
        }

        public void Tick()
        {
            if (!_baseObject.CurrentState.IsSnapped()) return;
            const float maxDistanceBeforeSnapRelease = 0.7f;
            var distanceBetweenItemAndPlayer = Vector3.Distance(transform.position, GameWorld.PlayerPosition);
            if (distanceBetweenItemAndPlayer > maxDistanceBeforeSnapRelease)
            {
                if (_baseObject is ISnap s)
                {
                    s.UnSnap();
                }
            }
        }

        public void OnConnectionPointTriggered(ConnectionPoint cp, ConnectionPoint otherCp)
        {
            if (!IsTheConnectingPoint)
            {
                return;
            }
            
            //Check if source type is correct
            if (otherCp.Status.IsConnected()) return;
            if (!cp.IsCompatibleWith(otherCp)) return;
            
            if (_baseObject is ISnap s)
            {
                s.Snap(DistanceToMove(otherCp.transform.position, cp.transform.position, 
                    transform.position));
            }
            
            cp.Connect();
            otherCp.Connect();
        }

        public void OnConnectionPointUnTriggered(ConnectionPoint cp, ConnectionPoint otherCp)
        {
            if (!IsTheConnectingPoint)
            {
                return;
            }
            
            if (_baseObject is ISnap s)
            {
               s.UnSnap();
            }

            cp.Disconnect();
            otherCp.Disconnect();
        }

        Vector3 DistanceToMove(Vector3 toWorldPosition, Vector3 fromWorldPosition, Vector3 currentWorldPosition)
        {
            var distanceToMove = toWorldPosition - fromWorldPosition;
            return currentWorldPosition + distanceToMove;
        }

        public void UpdateVisual()
        {
            var connected = new List<ConnectionPoint>();

            foreach (ConnectionPoint connectionPoint in _connectionPoints)
            {
                if(connectionPoint.Status.IsConnected()) connected.Add(connectionPoint);
            }

            var count = connected.Count;

            switch (count)
            {
                case 0: _renderer.material = _nonConnected; break;
                case 2: _renderer.material = _bothConnected; break;
                case 1:
                    _renderer.material = connected[0].ConnectionType.IsInlet ? _inputConnected : _outputConnected; 
                    break;
            }
        }

        bool IsTheConnectingPoint => GameWorld.ActiveObject == _baseObject;
    }
}
