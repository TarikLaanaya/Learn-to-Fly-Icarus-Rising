using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private float fadeSpeed = .1f;
    public static SceneManager instance { get; private set; }
    public CurrencyManager currencyManager;
    public GameManager gameManager;
    public MusicManager musicManager;

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
    }

    public void FadeToScene(string sceneName, UnityEngine.UI.Image blackoutImage, bool fadeInAndOut)
    {
        StartCoroutine(FadeToSceneCoroutine(sceneName, blackoutImage, fadeInAndOut)); // Move to the coroutine
    }

    private IEnumerator FadeToSceneCoroutine(string sceneName, UnityEngine.UI.Image blackoutImage, bool fadeInAndOut)
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

        yield return null; // Wait one frame for the scene to load

        if (fadeInAndOut)
        {
            blackoutImage = GameObject.Find("Blackout").GetComponent<UnityEngine.UI.Image>();

            if (blackoutImage == null) yield break; // Exit if blackoutImage is not found

            // Fade blackout away
            while (alpha > 0f)
            {
                alpha -= fadeSpeed * Time.deltaTime;
                blackoutImage.color = new Color(0f, 0f, 0f, alpha);
                yield return null; // Wait for next frame
            }
        }
    }
}