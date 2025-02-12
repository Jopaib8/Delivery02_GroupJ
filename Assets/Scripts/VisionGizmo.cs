using UnityEngine;

public class VisionGizmo : MonoBehaviour
{
    [SerializeField]
    private float visionRange;
    [SerializeField]
    private float playerDistance;
    private Transform _player;

    public void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (_player != null) playerDistance = Vector3.Distance(transform.position, _player.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.color = Color.yellow;
        if (_player != null) Gizmos.DrawLine(transform.position, _player.position);
        Gizmos.color = Color.white;
    }
}