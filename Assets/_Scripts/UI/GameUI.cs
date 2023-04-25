using System;
using DG.Tweening;
using Game.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text endText;
        [SerializeField] private Transform endPanel;
        [SerializeField] private Transform scorePanel;
        [SerializeField] private Slider progressSlider;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button[] restartButtons;

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
            
            exitButton.onClick.AddListener(() =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            });

            foreach (var restartButton in restartButtons)
            {
                restartButton.onClick.AddListener(() =>
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
                endText.text = "You won!";
                endText.color = Color.green;
            }
            else if (newState == GameState.Lose)
            {
                endText.text = "You lost!";
                endText.color = Color.red;
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
