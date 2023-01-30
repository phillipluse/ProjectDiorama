namespace ProjectDiorama
{
    public interface IBaseObjectModule
    {
        /// <summary>
        /// Called at Object creation. Called prior to Init.
        /// </summary>
        /// <param name="b"></param>
        public void SetUp();

        /// <summary>
        /// Called at Object creation. Use to set up properties. Should only call once in life span of Object.
        /// </summary>
        /// <param name="baseTile"></param>
        public void Init();
        
        /// <summary>
        /// Use to start or continue logic and function of tile.
        /// </summary>
        public void OnPlaced();

        public void OnMove();
        public void OnRotate(RotationDirection dir);

        public void Tick();
        public void OnObjectStateEnter(ObjectState state);
    }
}