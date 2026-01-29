using TMPro;
using UnityEngine;

public class BigBoss : MonoBehaviour
{
    GameObject player;
    GameObject textObject;
    TextMeshProUGUI text;
    private GameObject gameUI;
    private GameObject gameOverUI;

    void Awake()
    {
        gameUI = GameObject.Find("Game UI");
        gameOverUI = GameObject.Find("Game Over UI");
        gameOverUI.SetActive(false);
        textObject = GameObject.Find("Boss Distance");
        text = textObject.GetComponent<TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector2(-Upgraded.bossDistance, 0);
    }
    private void FixedUpdate()
    {
        text.text = $"{Mathf.Floor(Vector2.Distance(player.transform.position, transform.position))}";
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 200 * Time.fixedDeltaTime);
        transform.position = new Vector3(transform.position.x, player.transform.position.y);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            gameUI.SetActive(false);
            gameOverUI.SetActive(true);
            other.gameObject.SetActive(false);
        }
    }
}