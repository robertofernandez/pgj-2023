using UnityEngine;

public class Banana : MonoBehaviour
{
    public float initialSpeed = 1f;
    public Vector2 normalizedDirection;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null && normalizedDirection != null)
        {
            Vector2 initialSpeedVector = new Vector2(normalizedDirection.x * initialSpeed, normalizedDirection.y * initialSpeed);
            Debug.Log("Launching banana with vector: " + initialSpeedVector);
            rb.velocity = initialSpeedVector;
        }
        else
        {
            Debug.LogError("No Rigidbody or normalized direction for banana.");
        }
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        Debug.Log("Banana collides with " + collision.gameObject);
        Character character = collision.gameObject.GetComponent<Character>();
        if (character != null) {
            int direction = 1;
            if (rb.position.x > collision.gameObject.transform.position.x)
            {
                direction = -1;
            }
            character.getHit(6, direction);
        }
    }
}