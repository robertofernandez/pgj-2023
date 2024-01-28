using UnityEngine;

public class BatProjectile : MonoBehaviour
{
    public float BAT_RANGE = 1f;
    public float initialSpeed = 1f;
    public Vector2 normalizedDirection;
	public Vector2 initialPosition;
    private Rigidbody2D rb;
	private bool failSent = false;
	public Character sender;
	
	void Update()
	{
		if(!failSent && Vector2.Distance(initialPosition, rb.position) > BAT_RANGE)
		{
			failSent = true;
			sender.failBatHit();
			Destroy(gameObject);
		}
	}

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null && normalizedDirection != null)
        {
            Vector2 initialSpeedVector = new Vector2(normalizedDirection.x * initialSpeed, normalizedDirection.y * initialSpeed);
            Debug.Log("Launching bat projectile with vector: " + initialSpeedVector);
            rb.velocity = initialSpeedVector;
        }
        else
        {
            Debug.LogError("No Rigidbody or normalized direction for bat projectile.");
        }
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        Debug.Log("Bat projectile collides with " + collision.gameObject);
		if(Vector2.Distance(initialPosition, rb.position) > BAT_RANGE)
		{
			if(!failSent)
			{
				failSent = true;
				sender.failBatHit();
				Destroy(gameObject);
			}
			return;
		}

        Character character = collision.gameObject.GetComponent<Character>();
        if (character != null) {
            int direction = 1;
            if (rb.position.x > collision.gameObject.transform.position.x)
            {
                direction = -1;
            }
            character.getHit(12, direction);
        }
        Destroy(gameObject);
    }
}