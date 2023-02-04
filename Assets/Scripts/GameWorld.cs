using UnityEngine;

namespace ProjectDiorama
{
    public class GameWorld : MonoBehaviour
    {
        static GridHandler GridHandler;
        static Player Player;

        [SerializeField] GridHandler _gridHandler;
        [SerializeField] Player _player;
        

        void Awake()
        {
            GridHandler = _gridHandler;
            Player = _player;
        }

        public static GridHandler ActiveGrid => GridHandler;
        public static int ActiveGridCellSize => GridHandler.CellSize;
        public static bool IsObjectBeingPlaced => Player.HasActiveObject;
    }
}
