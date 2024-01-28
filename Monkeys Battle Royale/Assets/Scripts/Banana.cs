using UnityEngine;

public class Banana : MonoBehaviour
{
    public float initialSpeed = 1f;
    public Vector2 normalizedDirection;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null && normalizedDirection != null)
        {
            Vector2 initialSpeedVector = new Vector2(normalizedDirection.x * initialSpeed, normalizedDirection.y * initialSpeed * 3);
            Debug.Log("Launching banana with vector: " + initialSpeedVector);
            rb.velocity = initialSpeedVector;
        }
        else
        {
            Debug.LogError("No Rigidbody or normalized direction for banana.");
        }
    }
}