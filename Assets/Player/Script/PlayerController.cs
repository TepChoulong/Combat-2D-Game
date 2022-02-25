using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    /*
    Note: 
        1. If you want to make your GameObject stop shaking of jitter you should change the interpolate mode in the rigibody2D component to "interpolate"
        (in the inspector of that GameObject).
    */


    //Movement System
    public float speed;
    Rigidbody2D rb2;
    float h_Move;
    float v_Move;
    Animator animator;
    //Jump System
    public float jumpspeed;
    // Grounded System
    public Transform GroundCheck;
    public LayerMask Ground;
    bool IsGrounded;
    //Flip System
    private bool facingRight = true;


    //Attacking System
    public LayerMask EnemiesLayer;
    public float AttackRadius;
    public Transform AttackPoint;
    //Time System
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        h_Move = Input.GetAxis("Horizontal");
        animator.SetFloat("HorizontalMove", Mathf.Abs(h_Move));

        v_Move = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (IsGrounded)
            {
                Jump();
            }
        }
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        
        if (rb2.velocity.y > 0.01)
        {
            animator.SetBool("IsJump", true);
            animator.SetBool("IsFall", false);
        }

        if (rb2.velocity.y < 0.01)
        {
            animator.SetBool("IsJump", false);
            animator.SetBool("IsFall", true);
        }

        if (IsGrounded)
        {
            animator.SetBool("IsJump", false);
            animator.SetBool("IsFall", false);
        }
    }

    void FixedUpdate()
    {
        Movement();
        FlipPlayer();
        IsGroundCheck();

        if (facingRight == false && h_Move >= 0)
        {
            FlipPlayer();
        }
        else if (facingRight == true && h_Move <= 0)
        {
            FlipPlayer();
        }
    }

    //Method

    void Movement()
    {
        rb2.velocity = new Vector2(h_Move * speed * Time.fixedDeltaTime, rb2.velocity.y);
    }

    void Jump()
    {
        rb2.velocity = new Vector2(rb2.velocity.x, jumpspeed * Time.fixedDeltaTime);
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void Attack()
    {
        //Play Fight Animation
        animator.SetTrigger("IsAttack");

        //Check Enemies
        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRadius, EnemiesLayer);

        //Take Damage
        foreach (Collider2D enemies in hitenemies)
        {
            Debug.Log("We hit" + enemies.name);
        }
    }

    void IsGroundCheck()
    {
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, Ground);
    }
}
