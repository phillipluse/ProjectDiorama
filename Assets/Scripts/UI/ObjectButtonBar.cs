using System.Collections.Generic;
using UnityEngine;

namespace ProjectDiorama
{
    public class ObjectButtonBar : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] GameObject _buttonPrefab;

        [Header("Properties")]
        [SerializeField] List<ObjectSO> _objects;

        void Awake()
        {
            GenerateButtons();
            Events.ObjectButtonBarCreatedEvent?.Invoke(this);
        }

        void GenerateButtons()
        {
            foreach (var o in _objects)
            {
                var go = Instantiate(_buttonPrefab, transform);
                if (go.TryGetComponent(out ObjectButton button))
                {
                    button.Init(o);
                }
            }
        }

        public bool TryGetObject(out GameObject go, int position)
        {
            if (_objects.Count >= position + 1)
            {
                go = _objects[position].Prefab;
                return true;
            }

            go = null;
            return false;
        }
    }
}
