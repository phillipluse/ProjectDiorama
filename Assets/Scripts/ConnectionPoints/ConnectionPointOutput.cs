namespace ProjectDiorama
{
    public class ConnectionPointOutput : ConnectionPoint
    {
        public override void Init()
        {
            RaycastDirection = -1;
        }

        public override void Tick()
        {
            CheckConnectionPoint();
        }
    }
}
