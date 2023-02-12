using UnityEngine;
using UnityEngine.UI;

namespace ProjectDiorama
{
    public class ObjectButton : MonoBehaviour
    {
        [SerializeField] Image _icon;

        ObjectSO _objectSO;
        
        public void OnButtonClick()
        {
            CreateObject();
        }

        public void Init(ObjectSO objectSO)
        {
            _objectSO = objectSO;
            _icon.sprite = _objectSO.Icon;
        }

        void CreateObject()
        {
            Events.CreateObject(_objectSO.Prefab);
        }
    }
}
