using DG.Tweening;
using Game.Data;
using Game.Managers;
using Game.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Sprite restartSprite;
        [SerializeField] private Sprite nextSprite;
        
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text endText;
        [SerializeField] private TMP_Text endButtonText;
        [SerializeField] private Transform endPanel;
        [SerializeField] private Transform scorePanel;
        [SerializeField] private Transform startPanel;
        [SerializeField] private Slider progressSlider;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button[] colorButtons;

        private void OnEnable()
        {
            GameManager.OnScoreChanged += SetScoreText;
            GameManager.OnProgressChanged += SetProgressSlider;
            GameManager.OnTimerUpdate += SetTimer;
            GameManager.OnGameStateChanged += SetEndPanel;
        }
        
        private void OnDisable()
        {
            GameManager.OnScoreChanged -= SetScoreText;
            GameManager.OnProgressChanged -= SetProgressSlider;
            GameManager.OnTimerUpdate -= SetTimer;
            GameManager.OnGameStateChanged -= SetEndPanel;
        }
        
        private void Start()
        {
            pauseButton.onClick.AddListener(() =>
            {
                Time.timeScale = 0;
                pausePanel.gameObject.SetActive(true);
            });
            
            resumeButton.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                pausePanel.gameObject.SetActive(false);
            });
            
            startPanel.gameObject.SetActive(true); 
            startButton.onClick.AddListener(() =>
            {
                startPanel.gameObject.SetActive(false); 
                // Start the game
                GameManager.Instance.PrepareGame();
            });
            
            exitButton.onClick.AddListener(() =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            });

            restartButton.onClick.AddListener(() =>
            {
                GameManager.Instance.LoadCurrentScene();
            });

            foreach (var colorButton in colorButtons)
            {
                colorButton.onClick.AddListener(() =>
                {
                    var color = colorButton.GetComponent<Image>().color;
                    var cubes = FindObjectsOfType<Cube>();
                    
                    var player = FindObjectOfType<Player>();
                    player.TrailRenderer.endColor = color;

                    foreach (var cube in cubes)
                    {
                        cube.CubeMat.color = color;
                    }

                    foreach (var button in colorButtons)
                    {
                        button.interactable = button != colorButton;
                    }
                });
            }
        }
        
        private void SetProgressSlider(float value)
        {
            progressSlider.value = value;
        }
        
        private void SetTimer(float timeInSeconds)
        {
            int timeInSecondsInt = (int)timeInSeconds;
            int minutes = (int) timeInSeconds / 60;
            int seconds = timeInSecondsInt - (minutes * 60);  //Get seconds for display alongside minutes
            timerText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");  //Create the string representation, where both seconds and minutes are at minimum 2 digits
            
            //timerText.SetText(TimeSpan.FromSeconds(timeInSeconds).ToString("mm:ss"));
        }
        
        private void SetEndPanel(GameState oldState, GameState newState)
        {
            if (newState == GameState.Win)
            {
                nextButton.onClick.RemoveAllListeners();
                endText.text = "You won!";
                endText.color = Color.green;
                endButtonText.text = "Next";
                nextButton.GetComponent<Image>().sprite = nextSprite;

                nextButton.onClick.AddListener(() =>
                {
                    GameManager.Instance.LoadNextScene();
                });
            }
            else if (newState == GameState.Lose)
            {
                nextButton.onClick.RemoveAllListeners();
                endText.text = "You lost!";
                endText.color = Color.red;
                endButtonText.text = "Restart";
                nextButton.GetComponent<Image>().sprite = restartSprite;
                
                nextButton.onClick.AddListener(() =>
                {
                    GameManager.Instance.LoadCurrentScene();
                });
            }
            else if (newState == GameState.End)
            {
                endPanel.gameObject.SetActive(true);
            }
        }
        
        private void SetScoreText(int value)
        {
            const float duration = .25f;
            
            scorePanel.transform.DOScale(1.2f, duration).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                scorePanel.transform.DOScale(1f, duration).SetEase(Ease.InOutCubic);
            });
            
            scoreText.SetText(value.ToString());
        }
    }
}
