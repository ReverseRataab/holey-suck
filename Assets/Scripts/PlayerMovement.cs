using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    public bool suckActive;
    private readonly float accel = 15000f;
    private GameObject radius;
    private Rigidbody2D rb;
    private GameObject gameUI;
    private GameObject gameOverUI;

    void Awake()
    {
        Application.targetFrameRate = 60;
        gameUI = GameObject.Find("Game UI");
        gameOverUI = GameObject.Find("Game Over UI");
        gameOverUI.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        radius = GameObject.FindGameObjectWithTag("Radius");
        radius.transform.localScale = new Vector2(Upgraded.suckRadius, Upgraded.suckRadius);
        suckActive = true;

        //setInterval of On and Off switch of the sucktion
        StartCoroutine(SuckSwitch());
    }

    // Update is called once per second or something
    void FixedUpdate()
    {
        // rb.MovePosition((Vector2)transform.position + moveInput * speed); <- Possible upgrade for more accurate movement
        if (moveInput.x != 0 || moveInput.y != 0)
        {
            if (rb.linearVelocity.magnitude > Upgraded.movementSpeed)
            {
                rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, Upgraded.movementSpeed);
            }
            rb.linearDamping = 0;
            // if (math.sign(moveInput.x) == -1 && math.sign(rb.linearVelocityX) == 1 || math.sign(moveInput.x) == 1 && math.sign(rb.linearVelocityX) == -1)
            // {
            //     rb.linearVelocityX /= 1.05f;
            // }
            // if (math.sign(moveInput.y) == -1 && math.sign(rb.linearVelocityY) == 1 || math.sign(moveInput.y) == 1 && math.sign(rb.linearVelocityY) == -1)
            // {
            //     rb.linearVelocityY /= 1.05f;
            // }
            rb.AddForce(accel * Time.fixedDeltaTime * moveInput);
        }
        else
        {
            //Friction for better movement
            rb.linearDamping = 5;
        }
    }

    // Player Movement that uses the InputSystem
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    //Suction On and Off function
    IEnumerator SuckSwitch()
    {
        while (true)
        {
            suckActive = !suckActive;
            radius.SetActive(suckActive);
            yield return new WaitForSeconds(4);
        }
    }

    void OnTriggerEnter2D(Collider2D item)
    {
        //Check if its a Resource and add the points
        if (item.GetComponent<ResourceMovement>())
        {
            Currency.AddMoney(1);
            item.gameObject.SetActive(false);
        }
        else if (item.GetComponent<BigBoss>())
        {
            gameUI.SetActive(false);
            gameOverUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
