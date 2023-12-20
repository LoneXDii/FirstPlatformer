using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class MainHero : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpForce = 0.5f;
    private bool isGrounded = true;
    private int direction = 1;

    private Rigidbody2D rb;
    private SpriteRenderer sprites;
    private Animator anim;
    public static MainHero Instance { get; set; }

    public delegate void ChangeDirectionHandler(bool LeftDirection);
    public static event ChangeDirectionHandler ChangeDirectionEvent;

    public delegate void ShootHandler();
    public static event ShootHandler ShootEvent;
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
        if (isGrounded)
        State = States.run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        if (dir.x < 0) Flip();
        dir.x = Math.Abs(dir.x) * direction;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
    }

    private void Fire()
    {
        State = States.fire;
        ShootEvent();
    }

    private void Jump()
    {
        if (isGrounded)
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
        if (isGrounded)
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
        isGrounded = collider.Length > 0;
    }

    private void Flip()
    {
        
        direction = -direction;
        bool dir = direction < 0;
        ChangeDirectionEvent(dir);
        transform.Rotate(0f, 180f, 0f);
    }
}


public enum States
{
    idle,
    run,
    fire,
    dead
}