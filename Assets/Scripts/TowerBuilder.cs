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
        parentTransform.position = new Vector3(parentTransform.position.x, SceneManager.instance.gameManager.towerHeight, parentTransform.position.z);

        BuildTower();
    }

    public void BuildTower()
    {
        int towerHeight = SceneManager.instance.gameManager.towerHeight;

        for (int i = 1; i < towerHeight / 9; i++)
        {
            Instantiate(towerBlockPrefab, new Vector3(-1.788411f, transform.position.y - distanceBetweenBlocks * i, parentTransform.position.z), Quaternion.identity);
        }
    }
}
