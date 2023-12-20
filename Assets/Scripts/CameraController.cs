using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float damping;
    private Vector3 pos;
    private float offset;
    private void Awake()
    {
        offset = 7;
        damping = 2f;
        if (!player)
            player = FindObjectOfType<MainHero>().transform;
        MainHero.ChangeDirectionEvent += ChangeDirectionHandler;
    }

    private void Update()
    {
        pos = player.position;
        pos.z = -10;
        pos.x += offset;
        transform.position = Vector3.Lerp(transform.position, pos, damping * Time.deltaTime);
    }

    private void ChangeDirectionHandler(bool LeftDirection)
    {
        if (LeftDirection) offset = -2;
        else offset = 7;
    }
}
