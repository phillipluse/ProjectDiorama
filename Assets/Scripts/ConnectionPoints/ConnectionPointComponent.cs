using System.Collections.Generic;
using UnityEngine;

namespace ProjectDiorama
{
    public class ConnectionPointComponent : MonoBehaviour, IBaseObjectModule
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
            
            foreach (ConnectionPoint connectionPoint in _connectionPoints)
            {
                connectionPoint.Init();
            }
        }

        public void Tick()
        {
            foreach (ConnectionPoint connectionPoint in _connectionPoints)
            {
                connectionPoint.Tick();
            }
            
            if (!_baseObject.CurrentState.IsSnapped()) return;
            if (!IfComponentCanUnSnap()) return; 
            UnSnap();
        }

        public void OnHoverEnter()
        {
        }

        public void OnHoverExit()
        {
        }

        public void OnSelected()
        {
            
        }

        public void OnDeSelect()
        {
            //Check Connections
            UpdateVisual();
        }

        public void OnPlaced()
        {
           UpdateVisual();
        }

        public void OnObjectStateEnter(ObjectState state)
        {
        }

        bool IfComponentCanUnSnap()
        {
            const float maxDistanceBeforeSnapRelease = 0.7f;
            var distanceBetweenComponentAndPlayer = Vector3.Distance(transform.position,
                GameWorld.PlayerPosition);
            return distanceBetweenComponentAndPlayer > maxDistanceBeforeSnapRelease;
        }

        public bool TrySnapBaseObject(ConnectionPoint cp, ConnectionPoint otherCp)
        {
            if (_baseObject is not ISnap s) return false;
            s.Snap(otherCp.transform, cp.transform, transform);
            return true;
        }

        void UnSnap()
        {
            if (_baseObject is ISnap s)
            {
                s.UnSnap();
            }

            foreach (ConnectionPoint connectionPoint in _connectionPoints)
            {
                connectionPoint.UnSnap();
            }
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
                case 1: _renderer.material = connected[0].ConnectionType.IsInlet ? 
                        _inputConnected : _outputConnected; 
                        break;
            }
        }

        bool IsTheConnectingPoint => GameWorld.ActiveObject == _baseObject;
    }
}
