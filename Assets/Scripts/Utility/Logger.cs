using UnityEngine;
using Object = UnityEngine.Object;

namespace ProjectDiorama
{
    public class Logger : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] bool _showLogs;
        [SerializeField] string _prefix;
        [SerializeField] Color _prefixColor;

        string _hexColor;

        void OnValidate()
        {
            _hexColor = "#" + ColorUtility.ToHtmlStringRGB(_prefixColor);
        }

        public void Log(object message, Object sender)
        {
            if (!_showLogs) return;
            Debug.Log($"<color={_hexColor}>{_prefix} </color>: {message}", sender);
        }

        public void Log(object message)
        {
            if (!_showLogs) return;
            Debug.Log($"<color={_hexColor}>{_prefix} </color>: {message}");
        }
    }
}