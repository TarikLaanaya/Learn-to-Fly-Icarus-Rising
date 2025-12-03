using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayLastGameStats : MonoBehaviour
{
    [SerializeField] private TMP_Text shopCurrencyUI;
    [SerializeField] private TMP_Text distanceUI;
    [SerializeField] private TMP_Text distanceValueUI;
    [SerializeField] private TMP_Text coinEarnedUI;
    [SerializeField] private TMP_Text coinEarnedValueUI;
    [SerializeField] private Button continueButton;
    private UnityEngine.UI.Image blackoutImage;


    void Start()
    {
        // Make sure the instances exist
        if (SceneManager.instance == null || SceneManager.instance.currencyManager == null)
        {
            Debug.LogWarning("SceneManager or CurrencyManager instance dont exist");
        }
        else
        {
            // Get the distance by reversing the operation done in DistanceChecker (in future refactor to avoid this e.g. store distance aswell as currency)
            distanceValueUI.text = SceneManager.instance.currencyManager.lastDistance.ToString();
            coinEarnedValueUI.text = SceneManager.instance.currencyManager.lastCoinEarned.ToString();

            shopCurrencyUI.text = SceneManager.instance.currencyManager.GetCurrency().ToString();
        }

        blackoutImage = GetComponent<UnityEngine.UI.Image>();

        SmackUIOntoScreen();
    }

    void Update()
    {
        
    }

    void SmackUIOntoScreen()
    {
        LeanTween.scale(distanceUI.gameObject, Vector3.one, 0.5f).setEaseOutBack();
        LeanTween.scale(distanceValueUI.gameObject, Vector3.one, 0.5f).setEaseOutBack().setDelay(1f);
        LeanTween.scale(coinEarnedUI.gameObject, Vector3.one, 0.5f).setEaseOutBack().setDelay(0.4f);
        LeanTween.scale(coinEarnedValueUI.gameObject, Vector3.one, 0.5f).setEaseOutBack().setDelay(1.5f);
        LeanTween.scale(continueButton.gameObject, Vector3.one, 0.5f).setEaseOutBack().setDelay(3f);
    }

    public void OnContinueButtonPressed()
    {
        foreach (Transform child in transform)
        {
            LeanTween.scale(child.gameObject, Vector3.zero, 0.3f).setEaseInBack();
            child.gameObject.SetActive(false);
        }

        StartCoroutine(FadeToShop());
    }

    private IEnumerator FadeToShop()
    {
        float alpha = blackoutImage.color.a;

        // Fade to black
        while (alpha > 0)
        {
            alpha -= 1.2f * Time.deltaTime;
            blackoutImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null; // Wait for next frame
        }

        gameObject.SetActive(false);
    }
}
