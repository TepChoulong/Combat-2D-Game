using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAl : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2d;

    //Attacking System
    public LayerMask PlayerLayer;
    public float AttackRadius;
    public Transform Attackpoint;
    public int enemy_attack_damage = 15;
    //Attacking Time System
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    //Following System
    [SerializeField] Transform Target;
    [SerializeField] float argoRange;
    public float Following_Speed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, Target.position);

        Debug.Log(distToPlayer);
        Debug.Log("This is animation float test " + Mathf.Abs(rb2d.velocity.x));

        if (distToPlayer < argoRange)
        {
            FollowingTarget();
        }
        else
        {
            NotFollowingTarget();
        }

        if (distToPlayer >= 1)
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }   
        }
    }

    void FollowingTarget()
    {   
        //Play Waliking Animation

        if (transform.position.x < Target.position.x)
        {
            //enmey is to the left side of the player, so move right
            rb2d.velocity = new Vector2(Following_Speed * Time.deltaTime, 0);

            //Not Flip Enemy
            transform.localScale = new Vector2(1,1);

        }
        else
        {
            //enmey is to the right side of the player, so move left
            rb2d.velocity = new Vector2(-Following_Speed * Time.deltaTime, 0);

            //Flip Enemy
            transform.localScale = new Vector2(-1,1);
        }
    }

    void NotFollowingTarget()
    {
        rb2d.velocity = new Vector2(0, 0);
    }

    void Attack()
    {
        //Play Fight Animation
        //animator.SetTrigger("IsAttack");

        //Check Enemies
        Collider2D[] hitplayer = Physics2D.OverlapCircleAll(Attackpoint.position, AttackRadius, PlayerLayer);

        //Take Damage
        foreach (Collider2D player in hitplayer)
        {
            Debug.Log("We hit " + player.name);

            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            playerHealth.TakeDamage(enemy_attack_damage);
        }
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(Attackpoint.position, AttackRadius);
    }
}
