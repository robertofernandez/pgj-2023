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

    void Start () {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
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
        onGround = Physics2D.OverlapCircle(new Vector2(groundDetector.position.x, groundDetector.position.y), 0.05f, groundMask);

        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();

        if (rb2D != null)
        {
            if (!isCurrent && onGround)
			{
                /*
				rb2D.constraints &= RigidbodyConstraints2D.FreezePositionX;
                rb2D.constraints &= RigidbodyConstraints2D.FreezePositionY;
                rb2D.constraints &= RigidbodyConstraints2D.FreezeRotation;
                */
                rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                rb2D.isKinematic = true;
			} else
            {
                /*
                rb2D.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                rb2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                rb2D.constraints &= RigidbodyConstraints2D.FreezeRotation;
                */
                rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb2D.isKinematic = false;
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
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
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
}
