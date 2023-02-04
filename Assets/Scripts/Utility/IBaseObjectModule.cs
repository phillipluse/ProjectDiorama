namespace ProjectDiorama
{
    public interface IBaseObjectModule
    {

        /// <summary>
        /// Called at Object creation. Use to set up properties. Should only call once in life span of Object.
        /// </summary>
        /// <param name="baseObject"></param>
        public void Init(BaseObject baseObject);
        
        public void OnHoverEnter();
        public void OnHoverExit();
        public void OnSelected();
        public void OnDeSelect();
        
        public void OnPlaced();

        public void OnMove();
        public void OnRotate(RotationDirection dir);

        public void Tick();
        public void OnObjectStateEnter(ObjectState state);
    }
}