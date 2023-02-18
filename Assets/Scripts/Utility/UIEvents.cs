using System;
using UnityEngine;

namespace ProjectDiorama
{
    public static class UIEvents
    {
        public static Action TurnButtonPressedEvent;

        public static void TurnButtonPressed()
        {
            TurnButtonPressedEvent?.Invoke();
        }
    }
}
