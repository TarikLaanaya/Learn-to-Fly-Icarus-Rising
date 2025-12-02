using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    private int currencyAmount = 0;
    private const string currencyKey = "PlayerCurrency";
    [HideInInspector] public int tempAddedAmount = 0;

    void Awake()
    {
        currencyAmount = PlayerPrefs.GetInt(currencyKey, 0); // If the key doesnt exist default to 0
    }

    public void AddCurrency(int amount)
    {
        currencyAmount += amount;
        PlayerPrefs.SetInt(currencyKey, currencyAmount);
        PlayerPrefs.Save();

        tempAddedAmount = amount;
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
}
