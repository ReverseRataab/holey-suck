using Unity.Mathematics;
using UnityEngine;

public class ResourceMovement : MonoBehaviour
{
    GameObject player;
    Transform target;
    float3 distance;
    PlayerMovement plrMovement;
    Rigidbody2D rb;

    void Awake()
    {
        gameObject.AddComponent<Rigidbody2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        plrMovement = player.GetComponent<PlayerMovement>();
    }
    void FixedUpdate()
    {
        distance = Vector2.Distance(target.position, transform.position);
        float distance_res = math.abs(distance.x) + math.abs(distance.y);
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Main movement code
        if (distance_res < Upgraded.suckRadius && plrMovement.suckActive)
        {
            rb.AddForce(Upgraded.suckPower * Time.fixedDeltaTime * transform.up);
            // transform.position = Vector3.MoveTowards(transform.position, target.position, Upgraded.suckPower * Time.fixedDeltaTime);
        }
    }
}