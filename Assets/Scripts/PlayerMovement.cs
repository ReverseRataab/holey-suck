using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    public bool suckActive;
    private float accel = 15000f;
    private float nextAbility;
    private float stamina = Upgraded.suckStamina;
    private float movementSpeed = Upgraded.movementSpeed;
    private bool overHeat = false;
    public bool abilityActive;
    private GameObject radius;
    private Rigidbody2D rb;
    private Slider staminaBar;

    void Awake()
    {
        Application.targetFrameRate = 60;
        staminaBar = GameObject.Find("Game UI").GetComponent<Slider>();
        rb = GetComponent<Rigidbody2D>();
        radius = GameObject.FindGameObjectWithTag("Radius");
        radius.transform.localScale = new Vector2(Upgraded.suckRadius, Upgraded.suckRadius);
        suckActive = false;
        radius.SetActive(suckActive);

        //setInterval of On and Off switch of the sucktion
        StartCoroutine(SuckSwitch());
    }

    // Update is called once per second or something
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !overHeat)
        {
            suckActive = true;
        }
        else if (Keyboard.current.spaceKey.wasReleasedThisFrame || overHeat)
        {
            suckActive = false;

        }
        radius.SetActive(suckActive);
    }
    void FixedUpdate()
    {
        if (moveInput.x != 0 || moveInput.y != 0)
        {
            if (rb.linearVelocity.magnitude > movementSpeed)
            {
                rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, movementSpeed);
            }
            rb.linearDamping = 0;
            rb.AddForce(accel * Time.fixedDeltaTime * moveInput);
        }
        else
        {
            //Friction for better movement
            rb.linearDamping = 3;
        }
    }

    // Player Movement that uses the InputSystem
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    void OnAbility(InputValue value)
    {
        if (Time.time > nextAbility && Ability.currAbility != null)
        {
            if (Ability.currAbility == "BlackHole")
            {
                StartCoroutine(BlackHole());
            }
            else if (Ability.currAbility == "Nova")
            {
                StartCoroutine(Nova());
            }
            nextAbility = Time.time + Ability.abilityCooldown;
        }
    }

    //Suction On and Off function
    IEnumerator SuckSwitch()
    {
        while (true)
        {
            if (suckActive && !overHeat)
            {
                staminaBar.value -= 0.1f;
                stamina -= 0.1f;
            }
            else if (stamina < Upgraded.suckStamina)
            {
                staminaBar.value += Upgraded.staminaRegen;
                stamina += Upgraded.staminaRegen;
            }
            if (stamina <= 0)
            {
                overHeat = true;
            }
            else if (stamina >= Upgraded.suckStamina)
            {
                overHeat = false;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    private IEnumerator BlackHole()
    {
        abilityActive = true;
        movementSpeed *= Ability.blackHoleMult;
        yield return new WaitForSeconds(Ability.abilityDuration);
        movementSpeed = Upgraded.movementSpeed;
        abilityActive = false;
    }
    private IEnumerator Nova()
    {
        movementSpeed = 0;
        yield return new WaitForSeconds(0.5f);
        abilityActive = true;
        accel = 0;
        yield return new WaitForSeconds(1f);
        accel = 15000;
        movementSpeed = Upgraded.movementSpeed;
        abilityActive = false;
    }

    private void OnCollisionEnter2D(Collision2D item)
    {
        {
            //Check if its a Resource and add the points
            if (item.gameObject.GetComponent<ResourceMovement>())
            {
                item.gameObject.SetActive(false);
                Currency.AddMoney(1f * Upgraded.resourceMultiplier);
            }
        }
    }
}
