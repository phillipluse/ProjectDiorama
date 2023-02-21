namespace ProjectDiorama
{
    public interface IBaseObjectModule
    {
        /// <summary> Called at Object creation. Use to set up properties. Should only call once in life span of Object.</summary>
        /// <param name="baseObject"></param>
        public void Init(BaseObject baseObject);
        /// <summary> Called every frame the base object is active / selected. </summary>
        public void Tick();
        /// <summary> Called the frame the mouse touches the base object. </summary>
        public void OnHoverEnter();
        /// <summary> Called the frame the mouse stops touching the base object. </summary>
        public void OnHoverExit();
        /// <summary> Called the frame the base object is becomes active / is selected. </summary>
        public void OnSelected();
        /// <summary> Called the frame the base object selection is cancelled but not through placement. </summary>
        public void OnDeSelect();
        /// <summary> Called the frame the base object is placed. </summary>
        public void OnPlaced();
        /// <summary> Called the frame the base object state is changed.</summary>
        /// <param name="state"></param>
        public void OnObjectStateEnter(ObjectState state);
    }
}