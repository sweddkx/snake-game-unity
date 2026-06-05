using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private int gridWidth = 20;
    [SerializeField] private int gridHeight = 20;
    [SerializeField] private float cellSize = 1f;
    
    private int score = 0;
    private int highScore = 0;
    private bool isGameOver = false;
    private bool isPaused = false;
    
    public event Action<int> OnScoreChanged;
    public event Action<int> OnHighScoreChanged;
    public event Action OnGameOver;
    public event Action OnPaused;
    public event Action OnResumed;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    
    public void AddScore(int points)
    {
        if (isGameOver) return;
        
        score += points;
        OnScoreChanged?.Invoke(score);
        
        if (score > highScore)
        {
            highScore = score;
            OnHighScoreChanged?.Invoke(highScore);
            SaveHighScore();
        }
    }
    
    public void GameOver()
    {
        isGameOver = true;
        SaveHighScore();
        OnGameOver?.Invoke();
    }
    
    public void TogglePause()
    {
        if (isGameOver) return;
        
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        
        if (isPaused)
            OnPaused?.Invoke();
        else
            OnResumed?.Invoke();
    }
    
    public bool IsGameOver => isGameOver;
    public bool IsPaused => isPaused;
    public int Score => score;
    public int HighScore => highScore;
    public int GridWidth => gridWidth;
    public int GridHeight => gridHeight;
    public float CellSize => cellSize;
    
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
    
    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}
