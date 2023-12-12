using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class MainHero : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpForce = 0.5f;
    private bool isGrounded = false;

    private Rigidbody2D rb;
    private SpriteRenderer sprites;
    private Animator anim;
    public static MainHero Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        sprites = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Run()
    {
        //if (isGrounded)
        State = States.run;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprites.flipX = dir.x < 0;
    }

    private void Fire()
    {
        State = States.fire;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        CheckGround();
    }


    private void Update()
    {
        if(State == States.fire)
        {
            
        }
        //if (isGrounded)
        State = States.idle;

        if (Input.GetButton("Horizontal"))
            Run();

        if (Input.GetButtonDown("Jump"))
            Jump();

        if (Input.GetButtonDown("Fire1"))
            Fire();
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;

        //if (!isGrounded) State = States.jump;
    }
}


public enum States
{
    idle,
    run,
    fire,
    dead
}