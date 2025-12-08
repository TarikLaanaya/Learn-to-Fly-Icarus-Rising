using UnityEditor.SearchService;
using UnityEngine;

public class JumpAnimation : MonoBehaviour
{
    [SerializeField] SpriteRenderer cutSceneSpriteRenderer;
    [SerializeField] SpriteRenderer playerSpriteRenderer;

    [SerializeField] Sprite[] standingSprites;
    [SerializeField] Sprite[] BuildUpSprites;
    [SerializeField] Sprite[] JumpSprites;
    [SerializeField] Sprite[] FlySprites;

    private int currentWingUpgrade;

    void Start()
    {
        currentWingUpgrade = SceneManager.instance.gameManager.GetCurrentWingUpgrade();

        playerSpriteRenderer.sprite = FlySprites[currentWingUpgrade];
    }

    public void StandFrame()
    {
        cutSceneSpriteRenderer.sprite = standingSprites[currentWingUpgrade];
    }

    public void BuildUpFrame()
    {
        cutSceneSpriteRenderer.sprite = BuildUpSprites[currentWingUpgrade];
    }

    public void JumpFrame()
    {
        cutSceneSpriteRenderer.sprite = JumpSprites[currentWingUpgrade];
    }

    public void FlyFrame()
    {
        cutSceneSpriteRenderer.sprite = FlySprites[currentWingUpgrade];
    }
}
