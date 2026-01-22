using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUpgrade : MonoBehaviour
{
    private GameObject menu;
    private GameObject upgrades;
    void Start()
    {
        menu = GameObject.Find("Menu");
        upgrades = GameObject.Find("Upgrades");
        upgrades.SetActive(false);
    }
    public void Play()
    {
        SceneManager.LoadScene(0);
    }
    public void GoToUpgrades()
    {
        menu.SetActive(false);
        upgrades.SetActive(true);
    }
    public void GoBack()
    {
        menu.SetActive(true);
        upgrades.SetActive(false);
    }
    public void UpgradeRadius()
    {
        Upgraded.suckRadius += 20;
    }
    public void UpgradePower()
    {
        Upgraded.suckPower += 2;
    }
    public void UpgradeSpeed()
    {
        Upgraded.movementSpeed += 5;
    }
    public void UpgradeResourceSpawn()
    {
        Upgraded.resourceSpawn += 20;
    }
}
