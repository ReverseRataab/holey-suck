using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public static Slider rectScale;
    void Awake()
    {
        rectScale = GetComponent<Slider>();
        StartCoroutine(ConsumeHunger());
    }
    IEnumerator ConsumeHunger()
    {
        while (rectScale.value > 0)
        {
            rectScale.value -= 0.01f;
            yield return new WaitForSeconds(0.2f);
        }
    }
}