using UnityEngine;

public class Currency : MonoBehaviour
{
    static float money;
    public static void AddMoney(float amount)
    {
        money += amount;
    }
    public static void SubtractMoney(float amount)
    {
        money -= amount;
    }
    public static float GetMoney()
    {
        return Mathf.Floor(money);
    }
}