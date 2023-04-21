using Game.Base;
using Game.Data;
using UnityEngine;

namespace Game.Units
{
    public class Obstacle : InteractableBase<Player>
    {
        [SerializeField] private int length;
        
        protected override void Interact(Player player)
        {
            base.Interact(player);
            if (player.TryGetComponent<CubeStack>(out var stack))
            {
                stack.Remove(length);
            }
        }
    }
}
