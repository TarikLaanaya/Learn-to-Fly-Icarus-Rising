using TMPro;
using UnityEditor.Build;
using UnityEngine;

public class DistanceChecker : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private TMP_Text distanceUI;
    private int distance;
    private bool playerFlying = true;

    void Start()
    {
        distanceUI = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (!playerFlying) return;

        distance = Mathf.FloorToInt(playerTransform.position.x);
        distanceUI.text = distance.ToString() + " M";
    }

    public void StopDistanceCheck()
    {
        playerFlying = false;

        SceneManager.instance.currencyManager.AddCurrency(distance);
    }
}
