using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed = 20f;
    public Rigidbody2D rb;
    void Start()
    {
       rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        IEnemyBase enemy = hitInfo.GetComponent<IEnemyBase>();
        if (enemy is not null) enemy.GetDamage();
        Destroy(gameObject);
    }

}
