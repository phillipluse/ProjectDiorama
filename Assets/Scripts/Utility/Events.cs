using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectDiorama
{
    public static class Events
    {
        public static Action<GameObject> AnyObjectSelected;
        
        public static void AnyObjectOnSelected(GameObject selectable)
        {
            AnyObjectSelected?.Invoke(selectable);
        }
    }
}
