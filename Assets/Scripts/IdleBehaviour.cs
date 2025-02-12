using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    public float StayTime;
    public float VisionRange;

    private float _timer;
    private Transform _player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer = 0.0f;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var playerClose = IsPlayerClose(animator.transform);
        var timeUp = IsTimeUp();
        animator.SetBool("IsChasing", playerClose);
        animator.SetBool("IsPatroling", timeUp);
    }

    private bool IsTimeUp()
    {
        _timer += Time.deltaTime;
        return (_timer > StayTime);
    }

    private bool IsPlayerClose(Transform transform)
    {
        var dist = Vector3.Distance(transform.position, _player.position);
        return (dist < VisionRange);
    }
}
