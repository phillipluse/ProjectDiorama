using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectDiorama
{
    public class TurnButton : MonoBehaviour
    {
        public void OnButtonClick()
        {
            UIEvents.TurnButtonPressed();
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
