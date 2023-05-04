using System.Linq;
using Game.Base;
using UnityEngine;

namespace Game.Data
{
    public class Cube : InteractableBase<CubeStack>
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        public Material CubeMat => _meshRenderer.materials.FirstOrDefault();
        
        protected override void Interact(CubeStack cubeStack)
        {
            base.Interact(cubeStack);
            cubeStack.Add(this);
        }
    }
}

