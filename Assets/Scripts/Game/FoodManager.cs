using UnityEngine;
using System.Collections.Generic;

public class FoodManager : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private ParticleSystem eatParticles;
    
    private Vector2Int foodPosition;
    
    private void Start()
    {
        SpawnFood(new List<Vector2Int>());
    }
    
    public void SpawnFood(List<Vector2Int> snakePositions)
    {
        Vector2Int newPos;
        do
        {
            newPos = new Vector2Int(
                Random.Range(0, GameManager.Instance.GridWidth),
                Random.Range(0, GameManager.Instance.GridHeight)
            );
        } while (snakePositions.Contains(newPos));
        
        foodPosition = newPos;
        
        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);
        
        GameObject food = Instantiate(foodPrefab, transform);
        food.transform.localPosition = new Vector3(
            foodPosition.x * GameManager.Instance.CellSize,
            foodPosition.y * GameManager.Instance.CellSize, 0);
        
        // Particle effect
        if (eatParticles)
        {
            ParticleSystem ps = Instantiate(eatParticles);
            ps.transform.position = food.transform.position;
            Destroy(ps.gameObject, 1f);
        }
    }
    
    public Vector2Int GetFoodPosition() => foodPosition;
}