using Game.Base;

namespace Game.Data
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

