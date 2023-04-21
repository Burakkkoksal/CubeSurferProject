using Game.Systems;
using UnityEngine;

namespace Game.Units
{
    public class Player : MonoBehaviour
    {
        private MovementSystem _movementSystem;
        
        private void Start()
        {
            _movementSystem = GetComponent<MovementSystem>();
            _movementSystem.CanMove = true;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "FinalZone")
            {
                Debug.Log($"OnTriggerEnter{other.gameObject.name}");
            }
        }
    }
}

