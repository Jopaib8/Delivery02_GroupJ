using UnityEngine;

public class VisionGizmo : MonoBehaviour
{
    [SerializeField]
    private float VisionRange;
    [SerializeField]
    private float PlayerDistance;

    private Transform _player;

    public void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Update()
    {
        if (_player != null) PlayerDistance = Vector3.Distance(transform.position, _player.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, VisionRange);
        Gizmos.color = Color.yellow;
        if (_player != null) Gizmos.DrawLine(transform.position, _player.position);
        Gizmos.color = Color.white;
    }
}
