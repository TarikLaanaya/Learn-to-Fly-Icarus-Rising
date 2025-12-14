using UnityEngine;

public class PlayerGoalHandler : MonoBehaviour
{
    [SerializeField] private float slideInXPosition;
    private float slideOutXPosition;
    private bool hasSlidOut = false;

    void Start()
    {
        slideOutXPosition = transform.position.x;

        if (SceneManager.instance.gameManager.gameWon)
        {
            gameObject.SetActive(false);
        }

        SlideIn();
    }

    void Update()
    {
        if (!hasSlidOut && Input.GetKeyDown(KeyCode.Space))
        {
            SlideOut();
        }
    }

    void SlideIn()
    {
        LeanTween.moveX(gameObject, slideInXPosition, 1f).setEaseOutExpo();
    }

    void SlideOut()
    {
        LeanTween.moveX(gameObject, slideOutXPosition, 1f).setEaseInExpo();
        hasSlidOut = true;
    }
}