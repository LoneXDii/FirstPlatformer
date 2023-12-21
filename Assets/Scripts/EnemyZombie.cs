using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.U2D;

public class EnemyZombie : MonoBehaviour, IEnemyBase
{
    [SerializeField] private int health = 10;
    [SerializeField] public float speed = 2;
    private int direction = 1;

    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    void Start()
    {
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = transform.right;
        dir.x = Math.Abs(dir.x) * direction;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
    }

    public void GetDamage()
    {
        health -= 1;
        Debug.Log(health);
        if (health <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == MainHero.Instance.gameObject)
        {
            MainHero.Instance.GetDamage();
        }
        else if (collision.gameObject.tag != "Floor")
        {
            Flip();
        }
    }
    private void Flip()
    {
        direction = -direction;
        bool dir = direction < 0;
        transform.Rotate(0f, 180f, 0f);
    }
}
