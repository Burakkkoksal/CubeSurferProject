using System;
using System.Collections;
using Game.Units;
using UnityEngine;

namespace Game.Managers
{
    public enum GameState : byte
    {
        None,
        Ready,
        Started,
        Win,
        Lose,
        End,
    }
    
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        public static event Action<int> OnScoreChanged;
        public static event Action<float> OnProgressChanged;
        public static event Action<float> OnTimerUpdate;
        public static event Action<GameState, GameState> OnGameStateChanged;
        
        [SerializeField, Range(30, 144)] 
        private int targetFrameRate = 60;
        
        [SerializeField, Range(0f, 10f)] 
        private float gameStartCountdown = 3f;
        
        [SerializeField] 
        private Transform startPoint;
        
        private Vector3 _startPos;
        private Vector3 _endPos;
        private GameState _gameState = GameState.None;
        
        private Player _player;
        private EndBonusManager _endBonusManager;

        private int _totalScore;
        private float _totalDistance;
        private float _gameTimer;
        
        public EndBonusManager EndBonusManager => _endBonusManager;
        
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
            _endBonusManager = GameObject.FindObjectOfType<EndBonusManager>();

            PrepareGame();
        }

        private void Update()
        {
            if (_player == null || !_player.CanMove || _gameState != GameState.Started) return;

            var playerPos = _player.transform.position;

            var distFromStart = Mathf.Abs(playerPos.z - startPoint.position.z);
            var progress = distFromStart / _totalDistance;
            _gameTimer += Time.deltaTime;
            
            if (distFromStart >= _totalDistance)
            {
                SetGameState(GameState.Win);
            }
            
            OnProgressChanged?.Invoke(progress);
            OnTimerUpdate?.Invoke(_gameTimer);
        }
        
        private void PrepareGame()
        {
            _startPos = startPoint.position;
            _endPos = GameObject.FindGameObjectWithTag("FinalZone").transform.position;
            _totalDistance = Mathf.Abs(_endPos.z - _startPos.z);
            _player.SetPlayerPosition(_startPos);
            
            _endBonusManager.CreateBonuses(_endPos);
            
            SetGameState(GameState.Ready);
            
            StartCoroutine(GameStartCountdown(gameStartCountdown));
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

        public void MultiplyScore(int value)
        {
            IncreaseScore(_totalScore * value);
        }
        private void IncreaseScore(int value)
        {
            _totalScore += value;
            OnScoreChanged?.Invoke(_totalScore);
        }

        public void SetGameState(GameState newState)
        {
            Debug.Log(newState);
            
            var oldState = _gameState;

            if ((oldState == GameState.Lose || oldState == GameState.Win) && newState != GameState.End)
            {
                SetGameState(GameState.End);
                return;
            }
            
            _gameState = newState;
            
            OnGameStateChanged?.Invoke(oldState, newState);
            
            if (newState == GameState.Lose)
                SetGameState(GameState.End);
        }
    }
}

