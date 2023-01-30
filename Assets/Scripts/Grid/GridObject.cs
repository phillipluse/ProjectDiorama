namespace ProjectDiorama
{
    public class GridObject
    {
        public GridObjectState GridObjectState { get; private set; }

        public GridObject(GridObjectState state)
        {
            GridObjectState = state;
        }

        public void SetTileType(GridObjectState t) => GridObjectState = t;
        public bool IsEmpty => GridObjectState == GridObjectState.Empty;
        public bool IsNull => GridObjectState == GridObjectState.None;
    }
}