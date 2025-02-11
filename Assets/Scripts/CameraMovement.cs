using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    public float smoothSpeed = 5f;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

        if (player != null)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}