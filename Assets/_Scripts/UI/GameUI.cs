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
        [SerializeField] private Transform scorePanel;
        [SerializeField] private Slider progressSlider;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button restartButton;

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
            
            restartButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            });
        }

        private void OnEnable()
        {
            GameManager.OnScoreChanged += SetScoreText;
            GameManager.OnProgressChanged += SetProgressSlider;
        }
        
        private void OnDisable()
        {
            GameManager.OnScoreChanged -= SetScoreText;
            GameManager.OnProgressChanged -= SetProgressSlider;
        }

        private void SetProgressSlider(float value)
        {
            progressSlider.value = value;
        }
        
        private void SetScoreText(int value)
        {
            scoreText.SetText(value.ToString());
            float duration = .25f;
            
            scorePanel.transform.DOScale(1.2f, duration).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                scorePanel.transform.DOScale(1f, duration).SetEase(Ease.InOutCubic);
            });
        }
    }
}
