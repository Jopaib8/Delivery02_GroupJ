using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    public Transform player;       // Transform del jugador
    private Vector3 offset;        // Offset dinámico basado en la posición inicial
    public float smoothSpeed = 5f; // Velocidad de suavizado del movimiento

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
            // Calculamos el offset basado en la posición inicial de la cámara
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        

        
        Vector3 desiredPosition = player.position + offset;

        
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}