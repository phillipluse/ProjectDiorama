using UnityEngine;

namespace ProjectDiorama
{
    public class QuitButton : MonoBehaviour
    {
        public void OnButtonClick()
        {
            Application.Quit();
        }
    }
}
