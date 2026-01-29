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
        Upgraded.suckRadius += 5;
    }
    public void UpgradePower()
    {
        Upgraded.suckPower += 5;
    }
    public void UpgradeStamina()
    {
        Upgraded.suckStamina += 0.5f;
    }
    public void UpgradeStaminaRegen()
    {
        Upgraded.staminaRegen /= 1.2f;
    }
    public void UpgradeDistance()
    {
        Upgraded.bossDistance += 100;
    }
    public void UpgradeResource()
    {
        Upgraded.resourceMultiplier += 0.1f;
    }
    public void UpgradeSpeed()
    {
        Upgraded.movementSpeed += 2;
    }
    public void UpgradeResourceSpawn()
    {
        Upgraded.resourceSpawn += 20;
    }
}
