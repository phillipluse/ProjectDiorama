using System;

namespace ProjectDiorama
{
    public static class Events
    {
        public static Action<BaseObject> AnyObjectInitializedEvent;
        public static Action<BaseObject> AnyObjectSelectedEvent;
        public static Action<BaseObject> AnyObjectPlacedEvent;
        
        public static void AnyObjectSelected(BaseObject baseObject)
        {
            AnyObjectSelectedEvent?.Invoke(baseObject);
        }

        public static void AnyObjectInitialized(BaseObject baseObject)
        {
            AnyObjectInitializedEvent?.Invoke(baseObject);
        }
        
        public static void AnyObjectPlaced(BaseObject baseObject)
        {
            AnyObjectPlacedEvent?.Invoke(baseObject);
        }
    }
}
