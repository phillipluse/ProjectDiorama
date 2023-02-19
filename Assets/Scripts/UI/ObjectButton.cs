using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ProjectDiorama
{
    public class ObjectButton : MonoBehaviour
    {
        [SerializeField] Image _icon;
        [SerializeField] TextMeshProUGUI _shortCutNumber;

        ObjectSO _objectSO;
        
        public void OnButtonClick()
        {
            CreateObject();
        }

        public void Init(ObjectSO objectSO, int shortCutNumber)
        {
            _objectSO = objectSO;
            _shortCutNumber.text = $"{shortCutNumber}";
            if (objectSO.Icon == null) return;
            _icon.sprite = _objectSO.Icon;
        }

        void CreateObject()
        {
            Events.CreateObject(_objectSO.Prefab);
        }
    }
}
