using UnityEngine;

public class JumpCutscene : MonoBehaviour
{
    private bool jumped = false;
    [SerializeField] private Animator cutsceneAnimator;

    void Start()
    {
        transform.position = new Vector3(0, SceneManager.instance.gameManager.playerStartHeight, 1);
    }

    void Update()
    {
        if (!jumped && Input.GetKeyDown(KeyCode.Space))
        {
            jumped = true;
            cutsceneAnimator.SetBool("Jump", true);
        }
    }
}
