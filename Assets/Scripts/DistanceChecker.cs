using TMPro;
using UnityEditor.Build;
using UnityEngine;

public class DistanceChecker : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private TMP_Text distanceUI;
    private int distance;
    private bool playerFlying = true;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EndCutsceneHandler endCutsceneHandler;
    private bool endCutsceneStarted = false;

    void Start()
    {
        distanceUI = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (!playerFlying || endCutsceneStarted) return;

        distance = Mathf.FloorToInt(playerTransform.position.x);
        distanceUI.text = distance.ToString() + " M";

        if (distance > 10000)
        {
            playerController.Cutscene();
            endCutsceneHandler.StartCutscene();
            endCutsceneStarted = true;
            SceneManager.instance.gameManager.gameWon = true;
        }
    }

    public void StopDistanceCheck()
    {
        playerFlying = false;

        SceneManager.instance.currencyManager.AddCurrency(distance);
    }
}
