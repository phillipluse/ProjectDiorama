using UnityEngine;

namespace ProjectDiorama
{
    public static class LayerMaskExtensions
    {
        public static bool Contains(this LayerMask layerMask, int layer)
        {
            var thisLayerMask = 1 << layer;
            return (layerMask & thisLayerMask) != 0;
        }
    }
}