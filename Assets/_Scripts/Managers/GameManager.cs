using System;
using System.Collections;
using Game.Data;
using Game.Units;
using UnityEngine;

namespace Game.Managers
{
    public enum GameState
    {
        None,
        Ready,
        Started,
        Ended,
    }
    
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        public static event Action<int> OnScoreChanged;
        public static event Action<float> OnProgressChanged;
        public static event Action<float> OnTimerUpdate;
        
        [SerializeField, Range(30, 144)] 
        private int targetFrameRate = 60;
        
        [SerializeField, Range(0f, 10f)] 
        private float gameStartCountdown = 3f;
        
        [SerializeField] private Transform startPoint;
        
        private Vector3 _startPos;
        private Vector3 _endPos;
        private GameState _gameState = GameState.None;
        
        private Player _player;
        
        private int _totalScore;
        private float _totalDistance;
        private float _gameTimer;
        
        private void OnEnable() => Diamond.OnScored += IncreaseScore;
        
        private void OnDisable() => Diamond.OnScored -= IncreaseScore;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            Time.timeScale = 1;
            Application.targetFrameRate = targetFrameRate;
        }

        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();

            PrepareGame();
        }

        private void PrepareGame()
        {
            _startPos = startPoint.position;
            _endPos = GameObject.FindGameObjectWithTag("FinalZone").transform.position;
            _totalDistance = Mathf.Abs(_endPos.z - _startPos.z);
            _player.SetPlayerPosition(_startPos);
            
            SetGameState(GameState.Ready);
            
            StartCoroutine(GameStartCountdown(gameStartCountdown));
        }
        
        private void Update()
        {
            if (_player == null || !_player.CanMove || _gameState != GameState.Started) return;

            var playerPos = _player.transform.position;

            var distFromStart = Mathf.Abs(playerPos.z - startPoint.position.z);
            var progress = distFromStart / _totalDistance;
            _gameTimer += Time.deltaTime;
            
            OnProgressChanged?.Invoke(progress);
            OnTimerUpdate?.Invoke(_gameTimer);
            
            if (distFromStart >= _totalDistance)
            {
                _player.CanMove = false;
            }
        }
        
        private IEnumerator GameStartCountdown(float duration)
        {
            float totalTime = 0;
            
            while (totalTime <= duration)
            {
                totalTime += Time.deltaTime;
                // countdownImage.fillAmount = totalTime / duration;
                // totalTime += Time.deltaTime;
                // var integer = (int)totalTime; /* convert integer to string and assign to text */
                yield return null;
            }

            _player.CanMove = true;
            
            SetGameState(GameState.Started);
        }

        private void IncreaseScore(int value)
        {
            _totalScore += value;
            OnScoreChanged?.Invoke(_totalScore);
        }

        private void SetGameState(GameState gameState)
        {
            _gameState = gameState;
        }
    }
}

