using UnityEngine;

namespace ProjectDiorama
{
    public class GameWorld : MonoBehaviour
    {
        public static GridHandler ActiveGrid { get; private set; }
        public static PlayerControls Controls { get; private set; }

        static Player Player;

        [SerializeField] GridHandler _gridHandler;
        [SerializeField] Player _player;
        

        void Awake()
        {
            ActiveGrid = _gridHandler;
            Player = _player;
            Controls = new PlayerControls();
            Controls.Enable();
        }

        public static int ActiveGridCellSize => ActiveGrid.CellSize;
        public static bool IsObjectBeingPlaced => Player.HasActiveObject;
        public static Vector3 PlayerPosition => Player.Position;
    }
}
