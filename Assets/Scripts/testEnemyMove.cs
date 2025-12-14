using UnityEngine;

public class TestEnemyMove : MonoBehaviour
{
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Test script started");
    }
    
    void Update()
    {
        // Simply move right continuously
        rb.linearVelocity = new Vector2(2, 0);
        Debug.Log($"Setting velocity to (2, 0). Current velocity: {rb.linearVelocity}");
    }
}