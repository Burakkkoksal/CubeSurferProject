using UnityEngine;

namespace Game.Data
{
    public class GamePart : MonoBehaviour
    {
        [SerializeField] private Transform endPoint;

        public Vector3 EndPos => endPoint.position;
    }
}
