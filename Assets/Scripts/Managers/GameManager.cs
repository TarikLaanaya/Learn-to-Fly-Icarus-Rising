using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int towerStartHeight = 46;
    [HideInInspector] public int towerHeight;
    [HideInInspector] public float playerStartHeight;
    [SerializeField] private float playerHeightFromTowerDifference = 3.7f;

    // --- Upgrades Save Data --- //
    private const string wingUpgradeKey = "WingUpgrade";
    private const string boostUpgradeKey = "BoostUpgrade";
    private const string fuelUpgradeKey = "FuelUpgrade";
    private const string towerUpgradeKey = "TowerUpgrade";
    private int currentWingUpgrade;
    private int currentBoostUpgrade;
    private int currentFuelUpgrade;
    public int currentTowerUpgrade;

    public bool gameWon = false;

    void Awake()
    {
        // If the key doesnt exist default to 0
        currentWingUpgrade = PlayerPrefs.GetInt(wingUpgradeKey, 0);
        currentBoostUpgrade = PlayerPrefs.GetInt(boostUpgradeKey, 0);
        currentFuelUpgrade = PlayerPrefs.GetInt(fuelUpgradeKey, 0);
        currentTowerUpgrade = PlayerPrefs.GetInt(towerUpgradeKey, 0);

        towerHeight = towerStartHeight * (currentTowerUpgrade + 1);
        playerStartHeight = towerHeight + playerHeightFromTowerDifference;
    }

    public void SetWingUpgrade(int upgradeAmount)
    {
        if (currentWingUpgrade > upgradeAmount) return;

        PlayerPrefs.SetInt(wingUpgradeKey, upgradeAmount);
        PlayerPrefs.Save();
        currentWingUpgrade = upgradeAmount;
    }

    public void SetBoostUpgrade(int upgradeAmount)
    {
        if (currentBoostUpgrade > upgradeAmount) return;
        
        PlayerPrefs.SetInt(boostUpgradeKey, upgradeAmount);
        PlayerPrefs.Save();
        currentBoostUpgrade = upgradeAmount;
    }

    public void SetCurrentFuelUpgrade(int upgradeIndex)
    {
        if (currentFuelUpgrade > upgradeIndex) return;

        PlayerPrefs.SetInt(fuelUpgradeKey, upgradeIndex);
        PlayerPrefs.Save();
        currentFuelUpgrade = upgradeIndex;
    }

    public void SetTowerUpgrade(int upgradeAmount)
    {
        if (currentTowerUpgrade > upgradeAmount) return;

        PlayerPrefs.SetInt(towerUpgradeKey, upgradeAmount);
        PlayerPrefs.Save();
        currentTowerUpgrade = upgradeAmount;
        towerHeight = towerStartHeight * (currentTowerUpgrade + 1);
        playerStartHeight = towerHeight + playerHeightFromTowerDifference;
    }

    public int GetCurrentWingUpgrade()
    {
        return currentWingUpgrade;
    }

    public int GetCurrentBoostUpgrade()
    {
        return currentBoostUpgrade;
    }

    public int GetCurrentFuelUpgrade()
    {
        return currentFuelUpgrade;
    }

    public int GetCurrentTowerUpgrade()
    {
        return currentTowerUpgrade;
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteKey(wingUpgradeKey);
        PlayerPrefs.DeleteKey(boostUpgradeKey);
        PlayerPrefs.DeleteKey(fuelUpgradeKey);
        PlayerPrefs.DeleteKey(towerUpgradeKey);

        currentWingUpgrade = 0;
        currentBoostUpgrade = 0;
        currentFuelUpgrade = 0;
        currentTowerUpgrade = 0;

        towerHeight = towerStartHeight * (currentTowerUpgrade + 1);
        playerStartHeight = towerHeight + playerHeightFromTowerDifference;

        PlayerPrefs.Save();
    }
}