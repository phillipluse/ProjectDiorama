using System;
using UnityEngine;

namespace ProjectDiorama
{
    public class BuildingObject : MonoBehaviour, IBaseObjectModule, IObjectSettings
    {
        [Header("References")]
        [SerializeField] ObjectVisual _visual;
        
        [Header("Properties")]
        [SerializeField] DirectionOffsetSettings _directionOffsetSettings;

        ObjectSettings _settings;

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
            _settings = new ObjectSettings(size, _directionOffsetSettings);
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
            var offset = _settings.GetObjectOffset(dir);
            transform.localPosition = new Vector3(offset.x, transform.localPosition.y, offset.z);
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

        public ObjectSettings GetSettings() => _settings;
    }

    public interface IObjectSettings
    {
        public ObjectSettings GetSettings();
    }
}
