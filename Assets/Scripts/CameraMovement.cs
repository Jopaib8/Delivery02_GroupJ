using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Player;
    private Vector3 offset;
    public float SmoothSpeed = 5f;

    void Start()
    {
        if (Player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                Player = playerObject.transform;
            }
        }
        if (Player != null)
        {
            offset = transform.position - Player.position;
        }
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = Player.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed * Time.deltaTime);
    }
}