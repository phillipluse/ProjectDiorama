using UnityEngine;

namespace ProjectDiorama
{
    public class PlayerObjectSelectShortcuts
    {
        readonly Player _player;
        ObjectButtonBar _bar;

        public  PlayerObjectSelectShortcuts(Player player)
        {
            _player = player;
            Events.ObjectButtonBarCreatedEvent += OnObjectButtonBarCreated;
        }

        public void SetInput(ref PlayerFrameInput input)
        {
            if (input.IsOneButtonPressedThisFrame)
            {
                TryCreateObject(0);
                return;
            }
            
            if (input.IsTwoButtonPressedThisFrame)
            {
                TryCreateObject(1);
                return;
            }            
            
            if (input.IsThreeButtonPressedThisFrame)
            {
                TryCreateObject(2);
                return;
            }

            if (input.IsFourButtonPressedThisFrame)
            {
                TryCreateObject(3);
                return;
            }
        }
        
        void TryCreateObject(int position)
        {
            if (_bar.TryGetObject(out GameObject go, position))
            {
                _player.CreateObject(go);
            }
        }
        
        void OnObjectButtonBarCreated(ObjectButtonBar bar)
        {
            _bar = bar;
        }
    }
}
