using Unity.VisualScripting;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private GameObject towerBlockPrefab;
    [SerializeField] private float distanceBetweenBlocks = 9.16571f;
    private Transform parentTransform;

    void Start()
    {
        parentTransform = transform.parent.gameObject.transform;
        parentTransform.position = new Vector2(parentTransform.position.x, SceneManager.instance.gameManager.towerHeight);

        BuildTower();
    }

    public void BuildTower()
    {
        int towerHeight = SceneManager.instance.gameManager.towerHeight;

        for (int i = 1; i < towerHeight / 9; i++)
        {
            Instantiate(towerBlockPrefab, new Vector2(parentTransform.position.x, transform.position.y - distanceBetweenBlocks * i), Quaternion.identity);
        }
    }
}
