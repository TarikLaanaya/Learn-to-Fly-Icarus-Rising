using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int cost;
    [SerializeField] private TMP_Text costDisplayUI;
    [SerializeField] private TMP_Text shopCurrencyUI;
    [SerializeField] private bool wingUpgrade;
    [SerializeField] private int upgradeAmount;

    void Start()
    {
        if(wingUpgrade && SceneManager.instance.gameManager.GetCurrentWingUpgrade() >= upgradeAmount)
        {
            Destroy(gameObject);
        }
        else if (!wingUpgrade && SceneManager.instance.gameManager.GetCurrentBoostUpgrade() >= upgradeAmount)
        {
            Destroy(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DisplayCost();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HideCost();
    }

    private void DisplayCost()
    {
        costDisplayUI.text = cost.ToString();
    }

    private void HideCost()
    {
        costDisplayUI.text = " ";
    }

    public void OnClicked()
    {
        if (SceneManager.instance.currencyManager.SpendCurrency(cost))
        {
            // Update currency UI
            shopCurrencyUI.text = SceneManager.instance.currencyManager.GetCurrency().ToString();

            if (wingUpgrade)
            {
                SaveWingUpgrade();
            }
            else
            {
                SaveBoostUpgrade();
            }

            Destroy(gameObject);
        }
    }

    void SaveWingUpgrade()
    {
        SceneManager.instance.gameManager.SetWingUpgrade(upgradeAmount);
    }

    void SaveBoostUpgrade()
    {
        SceneManager.instance.gameManager.SetBoostUpgrade(upgradeAmount);
    }
}
