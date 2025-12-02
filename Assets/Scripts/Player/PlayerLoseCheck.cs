using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLoseCheck : MonoBehaviour
{
    [SerializeField] private GameObject splashEffect;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private DistanceChecker distanceChecker;
    [SerializeField] private float timeBeforeFade = 4f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            playerController.isAlive = false;
            Instantiate(splashEffect, transform.position, Quaternion.identity);
            playerModel.SetActive(false);

            StartCoroutine(LoseSequence());
            distanceChecker.StopDistanceCheck();
        }
    }

    private IEnumerator LoseSequence()
    {
        yield return new WaitForSeconds(timeBeforeFade);

        SceneManager.instance.FadeToScene("ShopScene");
    }
}