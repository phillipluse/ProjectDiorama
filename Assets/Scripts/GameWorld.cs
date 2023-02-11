using UnityEngine;

namespace ProjectDiorama
{
    public class GameWorld : MonoBehaviour
    {
        public static GridHandler ActiveGrid { get; private set; }
        public static PlayerControls Controls { get; private set; }

        static Player Player;

        [SerializeField] Player _player;
        

        void Awake()
        {
            Player = _player;
            Controls = new PlayerControls();
            Controls.Enable();
        }

        public static void RegisterGrid(GridHandler handler)
        {
            ActiveGrid = handler;
        }

        public static int ActiveGridCellSize => ActiveGrid.CellSize;
        public static bool IsObjectBeingPlaced => Player.HasActiveObject;
        public static Vector3 PlayerPosition => Player.Position;
        public static bool IsPlayerOnGrid => Player.IsPlayerOverGrid;
    }
}
