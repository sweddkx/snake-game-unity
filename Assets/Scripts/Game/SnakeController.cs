using UnityEngine;
using System.Collections.Generic;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private GameObject segmentPrefab;
    [SerializeField] private float moveSpeed = 0.15f;
    [SerializeField] private AudioClip eatSound;
    [SerializeField] private AudioClip dieSound;
    
    private List<Vector2Int> bodyPositions = new List<Vector2Int>();
    private Vector2Int direction = Vector2Int.right;
    private Vector2Int nextDirection = Vector2Int.right;
    private float moveTimer = 0f;
    private List<GameObject> segments = new List<GameObject>();
    private AudioSource audioSource;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InitializeSnake();
    }
    
    private void InitializeSnake()
    {
        bodyPositions.Clear();
        segments.ForEach(s => Destroy(s));
        segments.Clear();
        
        // Create initial snake (3 segments)
        for (int i = 2; i >= 0; i--)
        {
            Vector2Int pos = new Vector2Int(5 + i, 10);
            bodyPositions.Add(pos);
            CreateSegment(pos, i == 0);
        }
    }
    
    private void CreateSegment(Vector2Int pos, bool isHead)
    {
        GameObject segment = Instantiate(segmentPrefab, transform);
        segment.transform.localPosition = new Vector3(pos.x * GameManager.Instance.CellSize, 
                                                      pos.y * GameManager.Instance.CellSize, 0);
        
        SpriteRenderer sr = segment.GetComponent<SpriteRenderer>();
        if (isHead)
            sr.color = Color.green;
        
        segments.Add(segment);
    }
    
    private void Update()
    {
        HandleInput();
        
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveSpeed)
        {
            MoveSnake();
            moveTimer = 0f;
        }
    }
    
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            if (direction != Vector2Int.down) nextDirection = Vector2Int.up;
        
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            if (direction != Vector2Int.up) nextDirection = Vector2Int.down;
        
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            if (direction != Vector2Int.right) nextDirection = Vector2Int.left;
        
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            if (direction != Vector2Int.left) nextDirection = Vector2Int.right;
    }
    
    private void MoveSnake()
    {
        direction = nextDirection;
        Vector2Int newHead = bodyPositions[0] + direction;
        
        // Check bounds
        if (newHead.x < 0 || newHead.x >= GameManager.Instance.GridWidth ||
            newHead.y < 0 || newHead.y >= GameManager.Instance.GridHeight)
        {
            Die();
            return;
        }
        
        // Check self collision
        if (bodyPositions.Contains(newHead))
        {
            Die();
            return;
        }
        
        bodyPositions.Insert(0, newHead);
        CreateSegment(newHead, true);
        
        // Check food collision
        FoodManager foodManager = FindObjectOfType<FoodManager>();
        if (foodManager && foodManager.GetFoodPosition() == newHead)
        {
            PlaySound(eatSound);
            GameManager.Instance.AddScore(10);
            foodManager.SpawnFood(bodyPositions);
        }
        else
        {
            // Remove tail
            bodyPositions.RemoveAt(bodyPositions.Count - 1);
            Destroy(segments[segments.Count - 1]);
            segments.RemoveAt(segments.Count - 1);
        }
        
        UpdateSegmentColors();
    }
    
    private void UpdateSegmentColors()
    {
        for (int i = 0; i < segments.Count; i++)
        {
            SpriteRenderer sr = segments[i].GetComponent<SpriteRenderer>();
            if (i == 0)
                sr.color = Color.green; // Head
            else
                sr.color = new Color(0.2f, 0.8f, 0.2f); // Body
        }
    }
    
    private void Die()
    {
        PlaySound(dieSound);
        GameManager.Instance.GameOver();
    }
    
    private void PlaySound(AudioClip clip)
    {
        if (clip && audioSource)
            audioSource.PlayOneShot(clip);
    }
    
    public List<Vector2Int> GetBodyPositions() => bodyPositions;
}
