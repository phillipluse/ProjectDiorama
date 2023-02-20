using UnityEngine;

namespace ProjectDiorama
{
    public class ConnectionPointSnap : MonoBehaviour
    {
        
        public Vector3 DistanceToMoveToConnectionPoint(Vector3 toLocation, Vector3 fromLocation, 
            Vector3 currentLocation)
        {
            var distanceToMove = toLocation - fromLocation;
            return currentLocation + distanceToMove;
        }
    }
}
