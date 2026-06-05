using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    
    private void Start()
    {
        if (scoreText)
            scoreText.text = "Score: 0";
        if (highScoreText)
            highScoreText.text = $"High Score: {GameManager.Instance.HighScore}";
        if (gameOverPanel)
            gameOverPanel.SetActive(false);
        if (pausePanel)
            pausePanel.SetActive(false);
    }
    
    private void OnEnable()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.OnScoreChanged += UpdateScore;
        GameManager.Instance.OnHighScoreChanged += UpdateHighScore;
        GameManager.Instance.OnGameOver += ShowGameOver;
        GameManager.Instance.OnPaused += ShowPauseMenu;
        GameManager.Instance.OnResumed += HidePauseMenu;
    }
    
    private void OnDisable()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.OnScoreChanged -= UpdateScore;
        GameManager.Instance.OnHighScoreChanged -= UpdateHighScore;
        GameManager.Instance.OnGameOver -= ShowGameOver;
        GameManager.Instance.OnPaused -= ShowPauseMenu;
        GameManager.Instance.OnResumed -= HidePauseMenu;
    }
    
    private void UpdateScore(int score)
    {
        if (scoreText)
            scoreText.text = $"Score: {score}";
    }
    
    private void UpdateHighScore(int highScore)
    {
        if (highScoreText)
            highScoreText.text = $"High Score: {highScore}";
    }
    
    private void ShowGameOver()
    {
        if (gameOverPanel)
            gameOverPanel.SetActive(true);
        if (gameOverText)
            gameOverText.text = $"Game Over!\n\nFinal Score: {GameManager.Instance.Score}\n\nPress R to Restart or ESC to Menu";
    }
    
    private void ShowPauseMenu()
    {
        if (pausePanel)
            pausePanel.SetActive(true);
    }
    
    private void HidePauseMenu()
    {
        if (pausePanel)
            pausePanel.SetActive(false);
    }
}
