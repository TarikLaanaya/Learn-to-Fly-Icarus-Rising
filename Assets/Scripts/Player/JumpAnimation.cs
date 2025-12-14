using UnityEditor.SearchService;
using UnityEngine;

public class JumpAnimation : MonoBehaviour
{
    [SerializeField] SpriteRenderer cutSceneSpriteRenderer;
    [SerializeField] SpriteRenderer playerSpriteRenderer;
    [SerializeField] SpriteRenderer jetPackSpriteRenderer;

    [SerializeField] Sprite[] standingSprites;
    [SerializeField] Sprite[] BuildUpSprites;
    [SerializeField] Sprite[] JumpSprites;
    [SerializeField] Sprite[] FlySprites;
    [SerializeField] Sprite[] BoostSprites;

    private int currentWingUpgrade;
    private int currentBoostUpgrade;

    void Start()
    {
        currentWingUpgrade = SceneManager.instance.gameManager.GetCurrentWingUpgrade();
        currentBoostUpgrade = SceneManager.instance.gameManager.GetCurrentBoostUpgrade();


        playerSpriteRenderer.sprite = FlySprites[currentWingUpgrade];
        jetPackSpriteRenderer.sprite = BoostSprites[currentBoostUpgrade];
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
