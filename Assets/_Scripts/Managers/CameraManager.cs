using Game.Units;
using UnityEngine;

namespace Game.Managers
{
    public class CameraManager : MonoBehaviour
    {
        private Transform _followingTarget;

        private Vector3 _offset;

        private float _lerpTime = 1;

        private void Start()
        {
            var player = GameObject.FindObjectOfType<Player>();

            if (player != null)
            {
                _followingTarget = player.transform;
                _offset = transform.position - _followingTarget.transform.position;
            }
        }

        private void LateUpdate()
        {
            if (_followingTarget == null) return;
            
            Follow();
        }

        private void Follow()
        {
            var pos = Vector3.Lerp(transform.position, new Vector3(0f, _followingTarget.position.y, _followingTarget.position.z) + _offset, _lerpTime * Time.deltaTime);

            transform.position = pos;
        }
    }
}


