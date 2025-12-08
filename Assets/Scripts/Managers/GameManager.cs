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
    private int currentWingUpgrade;
    private int currentBoostUpgrade;

    void Awake()
    {
        towerHeight = towerStartHeight;
        playerStartHeight = towerHeight + playerHeightFromTowerDifference;

        // If the key doesnt exist default to 0
        currentWingUpgrade = PlayerPrefs.GetInt(wingUpgradeKey, 0);
        currentBoostUpgrade = PlayerPrefs.GetInt(boostUpgradeKey, 0);
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

    public int GetCurrentWingUpgrade()
    {
        return currentWingUpgrade;
    }

    public int GetCurrentBoostUpgrade()
    {
        return currentBoostUpgrade;
    }


    public void ClearData()
    {
        PlayerPrefs.DeleteKey(wingUpgradeKey);
        PlayerPrefs.DeleteKey(boostUpgradeKey);

        currentWingUpgrade = 0;
        currentBoostUpgrade = 0;

        PlayerPrefs.Save();
    }
}