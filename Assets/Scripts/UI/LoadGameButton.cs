using UnityEngine;

namespace ProjectDiorama
{
    public class LoadGameButton : MonoBehaviour
    {
        public void OnButtonClick()
        {
            GameManager.Instance.LoadGame();
        }
    }
}
