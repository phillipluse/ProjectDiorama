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
            
            GameWorld.Controls.PlayerActions.OneButton.performed   += _ => TryCreateObject(1);
            GameWorld.Controls.PlayerActions.TwoButton.performed   += _ => TryCreateObject(2);
            GameWorld.Controls.PlayerActions.ThreeButton.performed += _ => TryCreateObject(3);
            GameWorld.Controls.PlayerActions.FourButton.performed  += _ => TryCreateObject(4);
            GameWorld.Controls.PlayerActions.FiveButton.performed  += _ => TryCreateObject(5);
            GameWorld.Controls.PlayerActions.SixButton.performed   += _ => TryCreateObject(6);
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
