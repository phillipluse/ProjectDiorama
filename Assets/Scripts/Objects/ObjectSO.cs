using UnityEngine;

namespace ProjectDiorama
{
    [CreateAssetMenu(fileName = "newObject", menuName = "Scriptable Objects / ProjectDiorama / New Object")]
    public class ObjectSO : ScriptableObject
    {
        [SerializeField] GameObject _prefab;
        [SerializeField] Sprite _icon;

        public GameObject Prefab => _prefab;
        public Sprite Icon => _icon;
    }
}
