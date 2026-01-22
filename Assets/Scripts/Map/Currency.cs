using UnityEngine;

public class Currency : MonoBehaviour
{
    static int money;
    public static void AddMoney(int amount)
    {
        money += amount;
    }
    public static void SubtractMoney(int amount)
    {
        money -= amount;
    }
    public static int GetMoney()
    {
        return money;
    }
}