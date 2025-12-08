using UnityEditor.SearchService;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image blackoutImage;

    public void NewGameClicked()
    {
        // Clear save data
        SceneManager.instance.gameManager.ClearData();
        SceneManager.instance.currencyManager.ClearData();

        SceneManager.instance.FadeToScene("GameScene", blackoutImage, true);
    }

    public void ContinueClicked()
    {
        int collectedInfo = 0;

        // Check if there is already data to continue from
        collectedInfo += SceneManager.instance.gameManager.GetCurrentWingUpgrade();
        collectedInfo += SceneManager.instance.gameManager.GetCurrentBoostUpgrade();
        collectedInfo += SceneManager.instance.currencyManager.GetCurrency();

        if (collectedInfo > 0)
        {
            SceneManager.instance.FadeToScene("GameScene", blackoutImage, true);
        }
    }
}