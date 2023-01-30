using System.Collections.Generic;

namespace ProjectDiorama
{
    public class PlacedGridObjects
    {
        List<GridObject> _gridObjects;

        public PlacedGridObjects()
        {
            _gridObjects = new List<GridObject>();
        }

        public void Add(GridObject gridObject)
        {
            _gridObjects.Add(gridObject);
        }


        public int Count => _gridObjects.Count;
        public List<GridObject> GridObjects() => _gridObjects;
    }
}