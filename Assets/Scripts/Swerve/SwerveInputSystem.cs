using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveInputSystem : MonoBehaviour
{
    private float _lastFrameFingerPositionX; ///

    private float _moveFactoryX; /// 

    public float MoveFactoryX { get => _moveFactoryX; } ///

    private void Update() /////////
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePposition = Input.mousePosition;
            _moveFactoryX = currentMousePposition.x - _lastFrameFingerPositionX;
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactoryX = 0f;
        }
    }
}
