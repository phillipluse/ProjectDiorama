using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectDiorama
{
    public class GameWorld : MonoBehaviour
    {
        static GridHandler GridHandler;

        [SerializeField] GridHandler _gridHandler;

        void Awake()
        {
            GridHandler = _gridHandler;
        }

        public static GridHandler ActiveGrid => GridHandler;
        public static int ActiveGridCellSize => GridHandler.CellSize;
    }
}
