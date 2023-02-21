using UnityEngine;

namespace ProjectDiorama
{
    public class ConnectionPointInput : ConnectionPoint
    {
        public override void Init()
        {
            RaycastDirection = 1;
        }

        public override void Tick()
        {
            CheckConnectionPoint();
        }
    }
}
