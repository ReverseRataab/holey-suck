using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void ToMenu()
    {
        SceneManager.LoadScene(1);
    }
}