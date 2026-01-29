using Unity.Mathematics;
using UnityEngine;

public class ResourceMovement : MonoBehaviour
{
    GameObject player;
    Transform target;
    float3 distance;
    PlayerMovement plrMovement;
    Vector3 veloc = Vector3.zero;
    private float suckPower = Upgraded.suckPower;
    // Rigidbody2D rb;

    void Awake()
    {
        // gameObject.AddComponent<Rigidbody2D>();
        // rb = gameObject.GetComponent<Rigidbody2D>();
        // rb.gravityScale = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        plrMovement = player.GetComponent<PlayerMovement>();
    }
    void FixedUpdate()
    {
        distance = Vector2.Distance(target.position, transform.position);
        float distance_res = math.abs(distance.x) + math.abs(distance.y);
        // Vector2 direction = target.position - transform.position;
        // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        // transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (plrMovement.abilityActive && Ability.currAbility == "Nova")
        {
            PlayerNova(distance_res);
        }

        // Main movement code
        if (distance_res < Upgraded.suckRadius && plrMovement.suckActive)
        {
            // if (rb.linearVelocity.magnitude > Upgraded.suckPower)
            // {
            //     rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, Upgraded.suckPower);
            // }
            if (plrMovement.abilityActive)
            {
                if (Ability.currAbility == "BlackHole")
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.position, suckPower * Ability.blackHoleMult * Time.fixedDeltaTime);
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, suckPower * Ability.blackHoleMult * Time.fixedDeltaTime);
            }
            // rb.AddForce(10 * Time.fixedDeltaTime * transform.up);
        }
    }
    void PlayerNova(float distance_res)
    {
        if (distance_res < Ability.novaRange)
        {
            gameObject.SetActive(false);
            Currency.AddMoney(1f * Upgraded.resourceMultiplier);
        }
    }

}