using System;
using Game.Systems;
using UnityEngine;

namespace Game.Units
{
    public class Player : MonoBehaviour
    {
        private MovementSystem _movementSystem;
        
        public bool CanMove
        {
            get => _movementSystem.CanMove;
            set => _movementSystem.CanMove = value;
        }

        private void Awake()
        {
            _movementSystem = GetComponent<MovementSystem>();
        }
        public void SetPlayerPosition(Vector3 position) => transform.position = position;
    }
}

