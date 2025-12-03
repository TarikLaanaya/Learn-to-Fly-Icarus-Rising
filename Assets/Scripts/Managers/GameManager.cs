using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int towerStartHeight = 46;
    [HideInInspector] public int towerHeight;
    [HideInInspector] public float playerStartHeight;

    void Awake()
    {
        towerHeight = towerStartHeight;
        playerStartHeight = towerHeight + 3.7f;
    }
}
