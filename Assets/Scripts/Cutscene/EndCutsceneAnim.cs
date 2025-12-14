using UnityEngine;

public class EndCutsceneAnim : MonoBehaviour
{
    [SerializeField] private GameObject[] firePrefabs;

    public void PlayFireEffect()
    {
        foreach (GameObject fire in firePrefabs)
        {
            fire.SetActive(true);
        }
    }
}
