using UnityEngine;

[RequireComponent(typeof(Collider))]
public class OpenTrigger : MonoBehaviour
{
    [SerializeField] private Barrier _barrier;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Crook _))
            _barrier.Open();
    }
}
