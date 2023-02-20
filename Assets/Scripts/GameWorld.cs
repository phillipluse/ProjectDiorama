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

        public static BaseObject ActiveObject => Player.ActiveObject;
        public static int ActiveGridCellSize => ActiveGrid.CellSize;
        public static bool IsObjectBeingPlaced => Player.HasActiveObject;
        public static bool IsPlayerOnGrid => Player.IsPlayerOverGrid;
        public static bool IsGridVisible => ActiveGrid.IsVisualOn();
        public static Vector3 PlayerPosition => Player.Position;
        public static void ShowGrid() => ActiveGrid.ShowVisual();
        public static void HideGrid() => ActiveGrid.HideVisual();
    }
}
