using UnityEngine;

public class CameraFollowBehaviour : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    
    void Start()
    {
        
    }

    void Update()
    {
        float yFollowPos;

        if(playerTransform.position.y > 5.1f)
        {
            yFollowPos = playerTransform.position.y;
        }
        else
        {
            yFollowPos = 5.1f;
        }

        transform.position = new Vector2(playerTransform.position.x, yFollowPos);
    }
}
