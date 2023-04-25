using UnityEngine;

namespace Game.Systems
{
    public class InputSystem : MonoBehaviour
    {
        private float _lastMousePosX = 0;
        private Vector3 _moveVector = Vector3.zero;
        private const float _tolerance = 3f;

        public Vector3 MoveVector => _moveVector;
        
        private void Update()
        {
            var mousePosX = Input.mousePosition.x;
            
            if (Input.GetMouseButton(0))
            {
                if (mousePosX > _lastMousePosX + _tolerance)
                {
                    _moveVector = new Vector3(1, 0, 0);
                }
                else if (mousePosX < _lastMousePosX - _tolerance)
                {
                    _moveVector = new Vector3(-1, 0, 0);
                }
                else
                {
                    _moveVector = Vector3.zero; 
                }
            }
            else
            {
                _moveVector = Vector3.zero; 
            }
            
            _lastMousePosX = mousePosX;
        }
    }
}

