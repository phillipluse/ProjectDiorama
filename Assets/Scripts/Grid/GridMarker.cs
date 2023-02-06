using Shapes;
using UnityEngine;

namespace ProjectDiorama
{
    public class GridMarker : MonoBehaviour
    {
        [SerializeField] Rectangle _marker;
        [SerializeField] Color _normalColor;
        [SerializeField] Color _warningColor;

        public void Init(BaseObject baseObject)
        {
            ChangeSize(baseObject.Selectable.FootprintSize());
        }

        public void Show()
        {
            gameObject.SetActive(true);
            SetNormal();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetNormal()
        {
            _marker.Color = _normalColor;
        }

        public void SetWarning()
        {
            _marker.Color = _warningColor;
        }

        void ChangeSize(Vector2 size)
        {
            _marker.Width = size.x;
            _marker.Height = size.y;
        }
    }
}
