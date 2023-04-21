using System;
using Game.Systems;
using Game.Units;
using UnityEngine;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static event Action<int> OnScoreChanged;
        public static event Action<float> OnProgressChanged;
        
        [SerializeField, Range(30, 144)] 
        private int targetFrameRate = 60;

        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
    
        private MovementSystem _playerMoveSystem;
        private int _totalScore;
        private float _totalDistance;

        private void OnEnable()
        {
            Diamond.OnScored += IncreaseScore;
        }
        private void OnDisable()
        {
            Diamond.OnScored -= IncreaseScore;
        }
        private void Start()
        {
            Time.timeScale = 1;
            Application.targetFrameRate = targetFrameRate;
            _playerMoveSystem = GameObject.FindObjectOfType<MovementSystem>();
            _totalDistance = Mathf.Abs(endPoint.position.z - startPoint.position.z);
        }

        private void Update()
        {
            if (_playerMoveSystem == null || !_playerMoveSystem.CanMove) return;

            var playerPos = _playerMoveSystem.transform.position;

            var distFromStart = Mathf.Abs(playerPos.z - startPoint.position.z);
            var progress = distFromStart / _totalDistance;
            
            OnProgressChanged?.Invoke(progress);

            if (distFromStart >= _totalDistance)
            {
                _playerMoveSystem.CanMove = false;
            }
        }

        private void IncreaseScore(int value)
        {
            _totalScore += value;
            OnScoreChanged?.Invoke(_totalScore);
        }
    }
}

