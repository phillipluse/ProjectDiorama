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
            
            GameWorld.Controls.PlayerActions.FiveButton.performed += ctx => TryCreateObject(5);
            GameWorld.Controls.PlayerActions.SixButton.performed += ctx => TryCreateObject(6);
        }

        public void SetInput(ref PlayerFrameInput input)
        {
            if (input.IsOneButtonPressedThisFrame)
            {
                TryCreateObject(1);
                return;
            }
            
            if (input.IsTwoButtonPressedThisFrame)
            {
                TryCreateObject(2);
                return;
            }            
            
            if (input.IsThreeButtonPressedThisFrame)
            {
                TryCreateObject(3);
                return;
            }

            if (input.IsFourButtonPressedThisFrame)
            {
                TryCreateObject(4);
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
