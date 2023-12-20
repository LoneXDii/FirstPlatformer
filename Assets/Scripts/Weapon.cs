using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject Bullet;

    void Start()
    {
        MainHero.ShootEvent += Shoot;
    }
    void Update()
    {

    }

    private void Shoot()
    {
        Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
    }
}
