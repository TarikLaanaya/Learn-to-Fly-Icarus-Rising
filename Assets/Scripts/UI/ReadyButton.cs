using UnityEngine;

public class ReadyButton : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image blackoutImage;

    public void OnClicked()
    {
        blackoutImage.gameObject.SetActive(true);
        blackoutImage.color = new Color(0f, 0f, 0f, 0f);
        SceneManager.instance.musicManager.StopMusic();
        SceneManager.instance.FadeToScene("GameScene", blackoutImage, true);
    }
}
