using UnityEngine;

public class JumpCutscene : MonoBehaviour
{
    private bool jumped = false;
    [SerializeField] private Animator cutsceneAnimator;
    [SerializeField] private Transform playerTransform;

    void Start()
    {
        transform.position = new Vector3(0, SceneManager.instance.gameManager.playerStartHeight, playerTransform.position.z);
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
