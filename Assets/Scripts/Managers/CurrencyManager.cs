using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    private int currencyAmount = 0;
    private const string currencyKey = "PlayerCurrency";
    [HideInInspector] public int lastDistance = 0;
    [HideInInspector] public int lastCoinEarned = 0;

    [Range(0, 100)]
    [SerializeField] private float percentageOfDistanceAsCurrency = 10;

    void Awake()
    {
        currencyAmount = PlayerPrefs.GetInt(currencyKey, 0); // If the key doesnt exist default to 0
        percentageOfDistanceAsCurrency /= 100; // Convert to percentage
    }

    public void AddCurrency(int distance)
    {
        int amount = Mathf.FloorToInt(distance * percentageOfDistanceAsCurrency); // Calculate currency to add

        currencyAmount += amount;
        PlayerPrefs.SetInt(currencyKey, currencyAmount);
        PlayerPrefs.Save();

        lastDistance = distance;
        lastCoinEarned = amount;
    }

    public int GetCurrency()
    {
        return currencyAmount;
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteKey(currencyKey);
        currencyAmount = 0;
        PlayerPrefs.Save();
    }

    // Returns true if player has enough money, otherwise skip the operation and return false
    public bool SpendCurrency(int amount)
    {
        if (currencyAmount >= amount)
        {
            currencyAmount -= amount;
            PlayerPrefs.SetInt(currencyKey, currencyAmount);
            PlayerPrefs.Save();
            return true;
        }

        return false;
    }
}
