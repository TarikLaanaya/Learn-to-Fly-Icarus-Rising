using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = .1f;
    public static SceneManager instance { get; private set; }
    public CurrencyManager currencyManager;
    private UnityEngine.UI.Image blackoutImage;

    void Awake()
    {
        // Only allow one instance of SceneManager to exist
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Dont destroy this game object when changing scenes
        }
        else
        {
            Destroy(gameObject);
        }

        blackoutImage = GameObject.Find("Blackout").GetComponent<UnityEngine.UI.Image>();
    }

    public void FadeToScene(string sceneName)
    {
        if (blackoutImage != null)
        {
            StartCoroutine(FadeRoutine(sceneName));
        }
    }

    private IEnumerator FadeRoutine(string sceneName)
    {
        float alpha = blackoutImage.color.a;

        // Fade to black
        while (alpha < 1f)
        {
            alpha += fadeSpeed * Time.deltaTime;
            blackoutImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null; // Wait for next frame
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
