using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Barrier : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Open()
    {
        _animator.SetTrigger(BarrierAnimatorData.Params.Open);
    }
}

