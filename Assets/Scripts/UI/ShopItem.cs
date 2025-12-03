using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int cost;
    [SerializeField] private TMP_Text costDisplayUI;
    [SerializeField] private TMP_Text shopCurrencyUI;

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

            Destroy(gameObject);
        }
    }
}
