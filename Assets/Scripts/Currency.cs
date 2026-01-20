using UnityEngine;

public class Currency : MonoBehaviour
{
    int money;
    public void AddMoney(int amount)
    {
        money += amount;
    }
}