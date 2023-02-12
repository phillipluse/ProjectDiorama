using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ProjectDiorama
{
    public class GridToggle : MonoBehaviour
    {
        [SerializeField] Toggle _toggle;
        
        bool _isGridVisualOn;
        bool _isInitialized;
        void Awake()
        {
            _isGridVisualOn = GameWorld.IsGridVisible;
            if (_isGridVisualOn != _toggle.isOn)
            {
                _toggle.isOn = _isGridVisualOn;
                return;
            }

            _isInitialized = true;
        }

        public void OnToggle()
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
                return;
            }
            _isGridVisualOn = !_isGridVisualOn;
            if (_isGridVisualOn)  GameWorld.ShowGrid();
            else GameWorld.HideGrid();
            
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
