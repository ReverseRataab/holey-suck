using System;
using UnityEngine;

public class BossAnchor : MonoBehaviour
{
    GameObject player;
    Vector2 direction;
    float angle;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector2(-Upgraded.bossDistance, 0);
    }
    void FixedUpdate()
    {
        direction = player.transform.position - transform.position;
        angle = MathF.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}