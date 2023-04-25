using Game.Base;
using UnityEngine;

namespace Game.Units
{
    public class Obstacle : InteractableBase<Player>
    {
        [SerializeField] private int length;
        
        protected override void Interact(Player player)
        {
            base.Interact(player);
            var stack = player.CubeStack;
            stack.Remove(length);
        }
    }
}

