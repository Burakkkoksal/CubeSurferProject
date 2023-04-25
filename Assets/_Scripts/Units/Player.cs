using Game.Data;
using Game.Systems;
using UnityEngine;

namespace Game.Units
{
    public class Player : MonoBehaviour
    {
        private CubeStack _cubeStack;
        private MovementSystem _movementSystem;

        public CubeStack CubeStack => _cubeStack;
        
        public bool CanMove
        {
            get => _movementSystem.CanMove;
            set => _movementSystem.CanMove = value;
        }

        private void Awake()
        {
            _movementSystem = GetComponent<MovementSystem>();
            _cubeStack = GetComponent<CubeStack>();
        }
        public void SetPlayerPosition(Vector3 position) => transform.position = position;
    }
}

