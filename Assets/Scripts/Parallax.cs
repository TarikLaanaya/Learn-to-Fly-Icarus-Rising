using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private bool followY = true;
    [SerializeField] private bool moveToPlayerX = false;

    [Range(0f, 1f)]
    public float speed = 0.2f;

    private Material mat;
    private float xOffset;
    private float yOffset;
    public bool endCutscene = false;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float playerVelocityX = playerRb.linearVelocity.x;
        xOffset += playerVelocityX * Time.deltaTime * speed;

        if(followY)
        {
            float playerVelocityY = playerRb.linearVelocity.y;
            yOffset += playerVelocityY * Time.deltaTime * speed;
        }

        if(endCutscene)
        {
            yOffset = 0;
            xOffset += 10 * Time.deltaTime * speed;
        }
        
        mat.mainTextureOffset = new Vector2(xOffset, yOffset);

        if (moveToPlayerX)
        {
            transform.position = new Vector3(playerRb.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}