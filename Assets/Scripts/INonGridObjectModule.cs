namespace ProjectDiorama
{
    public interface INonGridObjectModule
    {
        public void Init(BaseObject baseObject);
        
        /// <summary> Will only call when base object is active. </summary>
        public void Tick();
    }
}