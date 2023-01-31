using System;
using System.Collections;
using UnityEngine;

namespace ProjectDiorama
{
    public class BuildingObject : MonoBehaviour, IBaseObjectModule, IObjectSettings
    {
        [Header("References")]
        [SerializeField] ObjectVisual _visual;

        ObjectSettings _settings;
        Vector3 _targetLocalPosition;
        bool _isMoveCRRunning;

        public void SetUp()
        {
            // var collider = gameObject.GetComponent<Collider>();
            // var bounds = collider.bounds;
            // var size = bounds.size;
            // var footprint = new Vector2(size.x, size.z);
            // _settings = new ObjectSettings(footprint, _directionOffsetSettings);
        }

        public void Init()
        {
            var collider = gameObject.GetComponent<Collider>();
            var bounds = collider.bounds;
            var size = bounds.size;
            _settings = new ObjectSettings(size);
        }

        public void OnPlaced()
        {
        }

        public void OnMove()
        {
        }

        public void OnRotate(RotationDirection dir)
        {
            _settings.SettingsUpdate(dir);
            
            _targetLocalPosition = _settings.Offset;
            if (_isMoveCRRunning) return;
            StartCoroutine(MoveCR());
        }

        public void Tick()
        {
        }

        public void OnObjectStateEnter(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Normal: _visual.SetToNormal(); break;
                case ObjectState.Warning: _visual.SetToWarning(); break;
                default: throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
        
        IEnumerator MoveCR()
        {
            _isMoveCRRunning = true;
            
            while (transform.localPosition != _targetLocalPosition)
            {
                var factor = 20.0f;
                transform.localPosition = Vector3.Lerp(transform.localPosition, _targetLocalPosition, Time.deltaTime * factor);
                Debug.Log("IsMoving");
                yield return null;
            }

            _isMoveCRRunning = false;
        }


        public ObjectSettings GetSettings() => _settings;
    }

    public interface IObjectSettings
    {
        public ObjectSettings GetSettings();
    }
}
