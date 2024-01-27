using UnityEngine;

public class Banana : MonoBehaviour
{
    public float initialSpeed = 1f; // Ajusta la velocidad inicial según tus necesidades

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 initialSpeedVector = new Vector2(initialSpeed, initialSpeed * 2);
            rb.velocity = initialSpeedVector;
        }
        else
        {
            Debug.LogError("No Rigidbody for banana.");
        }
    }
}