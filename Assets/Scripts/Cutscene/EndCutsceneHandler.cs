using UnityEngine;
using System.Collections;

public class EndCutsceneHandler : MonoBehaviour
{
    [SerializeField] private GameObject cutsceneObject;
    [SerializeField] private GameObject quoteBox;
    [SerializeField] private Parallax[] parallaxLayers;
    [SerializeField] private UnityEngine.UI.Image blackoutImage;
    [SerializeField] private float fadeSpeed = 0.5f;

    public void StartCutscene()
    {
        StartCoroutine(Fade());

        foreach (Parallax layer in parallaxLayers)
        {
            layer.endCutscene = true;
        }
    }

    IEnumerator CutsceneRoutine()
    {
        quoteBox.SetActive(true);

        yield return new WaitForSeconds(8);

        cutsceneObject.SetActive(true);
        quoteBox.SetActive(false);

        yield return new WaitForSeconds(10);

        SceneManager.instance.FadeToScene("MainMenu", blackoutImage, true);
    }

    private IEnumerator Fade()
    {
        float alpha = blackoutImage.color.a;

        // Fade to black
        while (alpha < 1f)
        {
            alpha += fadeSpeed * Time.deltaTime;
            blackoutImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null; // Wait for next frame
        }

        SceneManager.instance.musicManager.StartMusic();
        yield return new WaitForSeconds(1f);

        StartCoroutine(CutsceneRoutine());

        // Fade to black
        while (alpha > 0f)
        {
            alpha -= fadeSpeed * Time.deltaTime;
            blackoutImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null; // Wait for next frame
        }
    }
}