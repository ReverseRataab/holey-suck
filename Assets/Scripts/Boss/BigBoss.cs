using TMPro;
using UnityEngine;

public class BigBoss : MonoBehaviour
{
    GameObject player;
    GameObject textObject;
    TextMeshProUGUI text;

    void Awake()
    {
        textObject = GameObject.Find("Boss Distance");
        text = textObject.GetComponent<TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector2(-Upgraded.bossDistance, 0);
    }
    private void FixedUpdate()
    {
        text.text = $"{Mathf.Floor(Vector2.Distance(player.transform.position, transform.position))}";
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 10 * Time.fixedDeltaTime);
        transform.position = new Vector3(transform.position.x, player.transform.position.y);
    }
}