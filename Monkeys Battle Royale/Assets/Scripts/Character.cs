using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    private Rigidbody2D body;
    public Animator animator;
    public Transform groundDetector;
    public LayerMask groundMask;
    public float maxVelocityY = 10;
    public float maxVelocityX = 10;
    public float baseJumpVelocity = 8;
    public float jumpVelocityMultiplier = 0.4f;
    public float xVelocityIncreaseRate = 0.4f;
    public float xVelocityDecreaseRate = 0.8f;
    public float yVelocityIncreaseRate = 0.4f;
    public float inhibitTime = 20;
    public float currentVelocityX = 0;
    public float currentVelocityY = 0;
    public bool onGround;
    public float xInput = 0;
    public float yInput = 0;
    public float inhibit = 0;
    public float inhibitCounter = 0;

    private bool isCurrent = false;

    private bool underAttack = false;

    public float velocityThreshold = 0.01f;
    public float inactivityTime = 2.0f;  // Adjust as needed
    private float inactiveTime;

    public bool alive = true;

    public int teamNumber;
    public int characterNumber;

    public CharactersManager manager;

    public static float MAX_HEALTH = 20f;

    public float healthValue = MAX_HEALTH;

    private HealthLevel healthLevel;


    public void setId(int team, int player, CharactersManager charManager)
    {
        teamNumber = team;
        characterNumber = player;
        manager = charManager;
    }

    public void die()
    {
        healthLevel.setHealth(0f);

        if( alive)
        {
            alive = false;
            if (body != null)
            {
                transform.position = new Vector2(characterNumber + teamNumber * 4, 20);
                body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                body.isKinematic = true;
            }
            manager.characterDies(teamNumber, characterNumber);
        }
    }

    void Start () {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        healthLevel = transform.Find("HealthBar").Find("HealthLevel").GetComponent<HealthLevel>();
		body.gravityScale = 2;
        onGround = false;
        inhibit = 0;
        //transform.localScale.Set (-1, 1, 1);
        //transform.Rotate(new Vector3 (0, 0, 90));
        //transform.lossyScale.Scale(new Vector3 (-1, 1, 1));
    }

    void Update() {
        animator.SetBool ("impulsing", Mathf.Abs(xInput) > 0);

        if (xInput > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        } else if (xInput < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        animator.SetBool ("onGround", onGround);
        //animator.SetFloat ("speedX", Mathf.Abs (body.velocity.x));
    }

    public void setCurrent(bool value)
    {
        isCurrent = value;
    }

    void FixedUpdate () {
        if(!alive)
        {
            return;
        }
        onGround = Physics2D.OverlapCircle(new Vector2(groundDetector.position.x, groundDetector.position.y), 0.05f, groundMask);

        if(isCurrent)
        {
            underAttack = false;
        }
        if (body != null)
        {
            if (underAttack && !isCurrent)
            {
                if (body.velocity.magnitude < velocityThreshold && Mathf.Abs(body.angularVelocity) < velocityThreshold)
                {
                    if (Time.time - inactiveTime > inactivityTime)
                    {
                        underAttack = false;
                        body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                        body.isKinematic = true;
                    }
                }
                else
                {
                    inactiveTime = Time.time;
                }
            }
            if (!isCurrent && onGround && !underAttack)
			{
                body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                body.isKinematic = true;
			} else
            {

                body.constraints = RigidbodyConstraints2D.FreezeRotation;
                body.isKinematic = false;
            }
        }
        else
        {
            Debug.LogError("El GameObject no tiene un componente Rigidbody2D.");
        }

        if(!isCurrent)
        {
            return;
        }
        if(inhibitCounter > 0){
            inhibitCounter = inhibitCounter - 1;
        }
        if(inhibitCounter < 1 && inhibit != 0){
            inhibit = 0;
        }
        
            //FIXME REMOVE
//        if(inhibit != 0) {
  //          return;
    //    }

        xInput = Input.GetAxis("Horizontal");
        if((xInput > 0 && inhibit > 0) || (xInput < 0 && inhibit < 0)){
            xInput = 0;
        }
        yInput = Input.GetAxis("Vertical");
        float velocityY = body.velocity.y;
        if (xInput > 0 && currentVelocityX < maxVelocityX && !(inhibit > 0)) {
            if (currentVelocityX < 0) {
                currentVelocityX = currentVelocityX + Mathf.Max(xVelocityIncreaseRate, xVelocityDecreaseRate);
            } else {
                currentVelocityX = currentVelocityX + xVelocityIncreaseRate;
            }
        } else if (xInput < 0 && currentVelocityX > (-1 * maxVelocityX)  && !(inhibit < 0)) {
            if (currentVelocityX > 0) {
                currentVelocityX = currentVelocityX - Mathf.Max(xVelocityIncreaseRate, xVelocityDecreaseRate);
            } else {
                currentVelocityX = currentVelocityX - xVelocityIncreaseRate;
            }
        } else if (xInput == 0 && currentVelocityX > 0) {
            currentVelocityX = Mathf.Max(0, currentVelocityX - xVelocityDecreaseRate);
        } else if (xInput == 0 && currentVelocityX < 0) {
            currentVelocityX = Mathf.Min(0, currentVelocityX + xVelocityDecreaseRate);
        }

        if (onGround && yInput > 0) {
            velocityY = baseJumpVelocity + Mathf.Abs(currentVelocityX) * jumpVelocityMultiplier;
        }

        body.velocity = new Vector2(currentVelocityX , velocityY);
    }

    public void getHit(float power, int direction)
    {
        if(!alive)
        {
            return;
        }

        underAttack = true;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        body.isKinematic = false;
        Debug.Log("Getting hit by and object with power: " + power);
        if (body != null)
        {
            Vector2 initialSpeedVector = new Vector2(power * direction, power);
            body.velocity = initialSpeedVector;
        }
        else
        {
            Debug.LogError("No Rigidbody for banana.");
        }
        healthValue -=power;
        if(healthValue < 1)
        {
            die();
        } else {
            healthLevel.setHealth(100 * healthValue / MAX_HEALTH);
        }
        Debug.Log("Health is " + healthValue);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!alive)
        {
            return;
        }

        if(!isCurrent)
        {
            return;
        }
        float distanceX = body.Distance(collision.collider).normal.x;
        float distanceY = body.Distance(collision.collider).normal.y;
        if (Mathf.Abs(distanceX) > Mathf.Abs(distanceY)) {
            //currentVelocityX = 0;
            currentVelocityX = (-1) * currentVelocityX;
            body.velocity = new Vector2(0 , currentVelocityY);
            inhibitCounter = inhibitTime;
            if(distanceX > 0){
                //currentVelocityX = -1;
                inhibit = 1;
            } else {
                //currentVelocityX = 1;
                inhibit = -1;
            }
            string pointsText = "|";
            foreach (ContactPoint2D contact in collision.contacts){
                pointsText = pointsText + contact.point + "|";
            }
            //Debug.Log("collision: " + pointsText);
            //Debug.Log("body position: " + body.position);
            //Debug.Log("distance normal: " + body.Distance(collision.collider).normal);
            //Debug.Log("distance normalX: " + body.Distance(collision.collider).normal.x);
            //Debug.Log("distance normalY: " + body.Distance(collision.collider).normal.y);
        }
    }

	public void failBatHit()
	{
		Debug.Log("I failed!");
	}

}
