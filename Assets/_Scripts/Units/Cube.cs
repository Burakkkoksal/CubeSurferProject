using Game.Base;
using Game.Data;

namespace Game.Units
{
    public class Cube : InteractableBase<CubeStack>
    {
        protected override void Interact(CubeStack cubeStack)
        {
            base.Interact(cubeStack);
            cubeStack.Add(this);
        }
    }
}

