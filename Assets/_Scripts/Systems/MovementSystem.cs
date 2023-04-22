using UnityEngine;

namespace Game.Systems
{
    public class MovementSystem : MonoBehaviour
    {
        [SerializeField]
        private InputSystem inputSystem;

        [SerializeField]
        private float moveSpeed = 1f;
        
        [SerializeField]
        private float swerveSpeed = 0.15f;

        private float _minOffsetX = -0.09f;
        
        private float _maxOffsetX = 0.09f;
        
        public bool CanMove { get; set; }

        private void Update()
        {
            if (CanMove)
                Move();
        }
        
        private void Move()
        {
            var swerveVector = inputSystem.MoveVector * (swerveSpeed * Time.deltaTime);

            var runVector = transform.forward * (Time.deltaTime * moveSpeed);

            var nextPos = swerveVector + runVector;

            transform.Translate(nextPos);

            var currPos = transform.position;
            
            currPos = new Vector3(Mathf.Clamp(currPos.x, _minOffsetX, _maxOffsetX), currPos.y, currPos.z);
            
            transform.position = currPos;
        }
    }
}
