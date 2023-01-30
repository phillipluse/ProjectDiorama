using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ProjectDiorama
{
    public class GridCheck
    {
        readonly GridHandler _handler;

        public GridCheck(GridHandler gridHandler)
        {
            _handler = gridHandler;
        }
        
        public bool SingleTileCheck(Vector3 worldPosition, ObjectSettings settings)
        {
            var gridPosition = _handler.GetGridPosition(worldPosition);
            if (!_handler.IsPositionOnGrid(gridPosition)) return false;
            var gridObject = _handler.GetGridObject(worldPosition);

            var neighborPositions = ListPool<GridPositionXZ>.Get();
            var neighbors = ListPool<GridObject>.Get();

            neighborPositions = GetNeighborPositions(gridPosition, neighborPositions);
            neighbors = GetNeighborsObjects(neighborPositions, neighbors);

            if (!gridObject.IsEmpty)
            {
                ReleaseLists();
                return false;
            }
            
            ReleaseLists();
            return true;
            
            void ReleaseLists()
            {
                ListPool<GridPositionXZ>.Release(neighborPositions);
                ListPool<GridObject>.Release(neighbors);
            }
        }
        
        public bool MultipleTileCheck(Vector3 worldPosition, ObjectSettings settings)
        {
            var size = settings.RotatedSize;
            var startPosition = _handler.GetGridPosition(worldPosition);
            
            var tilePositionsToCheck = ListPool<GridPositionXZ>.Get();
            var neighborObjects = ListPool<GridObject>.Get();
            var neighborPositionsToCheck = ListPool<GridPositionXZ>.Get();

            tilePositionsToCheck.GetAllPositions(size, startPosition, settings.Directions);

            if (!ArePositionsOnGrid(tilePositionsToCheck))
            {
                ReleaseLists();
                return false;
            }

            if (!ArePositionsEmpty(tilePositionsToCheck))
            {
                ReleaseLists();
                return false;
            }
            
            ReleaseLists();
            return true;

            void ReleaseLists()
            {
                ListPool<GridPositionXZ>.Release(neighborPositionsToCheck);
                ListPool<GridPositionXZ>.Release(tilePositionsToCheck);
                ListPool<GridObject>.Release(neighborObjects);
            }
        }
        
        bool GridObjectsAreEmpty(List<GridObject> gridObjects)
        {
            foreach (GridObject gridObject in gridObjects)
            {
                if (!gridObject.IsEmpty)
                    return false;
            }

            return true;
        }
        
        bool ArePositionsEmpty(List<GridPositionXZ> positions)
        {
            foreach (GridPositionXZ position in positions)
            {
                var gridObject = _handler.GetGridObject(position);
                if (!gridObject.IsEmpty)
                    return false;
            }

            return true;
        }

        bool ArePositionsOnGrid(List<GridPositionXZ> positions)
        {
            foreach (GridPositionXZ position in positions)
            {
                if (!_handler.IsPositionOnGrid(position))
                    return false;
            }

            return true;
        }
        
        public List<GridPositionXZ> GetNeighborPositions(GridPositionXZ startPositionXZ, List<GridPositionXZ> list)
        {
            list.Add(NorthGridPosition(startPositionXZ));        
            list.Add(EastGridPosition(startPositionXZ));           
            list.Add(SouthGridPosition(startPositionXZ));          
            list.Add(WestGridPosition(startPositionXZ));           
            list.Add(new GridPositionXZ(startPositionXZ.X + 1, startPositionXZ.Z + 1));     //NorthEast
            list.Add(new GridPositionXZ(startPositionXZ.X + 1, startPositionXZ.Z - 1));     //SouthEast
            list.Add(new GridPositionXZ(startPositionXZ.X - 1, startPositionXZ.Z - 1));     //SouthWest
            list.Add(new GridPositionXZ(startPositionXZ.X - 1, startPositionXZ.Z + 1));     //NorthWest
            list = RemovePositionsNotOnGrid(list);
            return list;
        }

        GridPositionXZ NorthGridPosition(GridPositionXZ startPositionXZ)
        {
            return new GridPositionXZ(startPositionXZ.X, startPositionXZ.Z + 1);
        }
        GridPositionXZ SouthGridPosition(GridPositionXZ startPositionXZ)
        {
            return new GridPositionXZ(startPositionXZ.X, startPositionXZ.Z - 1);
        }
        GridPositionXZ EastGridPosition(GridPositionXZ startPositionXZ)
        {
            return new GridPositionXZ(startPositionXZ.X + 1, startPositionXZ.Z);
        }
        GridPositionXZ WestGridPosition(GridPositionXZ startPositionXZ)
        {
            return new GridPositionXZ(startPositionXZ.X - 1, startPositionXZ.Z);
        }
        
        List<GridPositionXZ> RemovePositionsNotOnGrid(List<GridPositionXZ> neighborPositions)
        {
            for (int i = neighborPositions.Count - 1; i >= 0; i--)
            {
                var gp = neighborPositions[i];
                if (!_handler.IsPositionOnGrid(gp))
                {
                    neighborPositions.Remove(gp);
                }
            }

            return neighborPositions;
        }
        
        public List<GridObject> GetNeighborsObjects(List<GridPositionXZ> positions, List<GridObject> list)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                list.Add(_handler.GetGridObject(positions[i]));
            }

            return list;
        }
    }
}