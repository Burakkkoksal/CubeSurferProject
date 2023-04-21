using System;
using Game.Base;
using UnityEngine;

namespace Game.Units
{
    public class Diamond : InteractableBase<Player>
    {
        public static event Action<int> OnScored;
        
        [SerializeField] private int _amount;

        protected override void Interact(Player player)
        {
            base.Interact(player);
            
            OnScored?.Invoke(_amount);
            
            gameObject.SetActive(false);
        }
    }
}
