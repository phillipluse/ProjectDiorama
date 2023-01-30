using System.Collections.Generic;
using UnityEngine;

namespace ProjectDiorama
{
    public static class ListExtensions
    {
        public static void GetAllPositions(this List<GridPositionXZ> list, Vector2Int size,
            GridPositionXZ startPositionXZ, Vector2Int dir)
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    list.Add(new GridPositionXZ(startPositionXZ.X + x * dir.x, startPositionXZ.Z + y * dir.y));
                }
            }
        }
        
        public static IList<T> RemoveExistingValues<T>(this IList<T> listOfValues, IList<T> listToCheckAgainst)
        {
            for (var i = listOfValues.Count - 1; i >= 0; i--)
            {
                var value = listOfValues[i];
                if (listToCheckAgainst.Contains(listOfValues[i]))
                    listOfValues.Remove(value);
            }

            return listOfValues;
        }
    }
}