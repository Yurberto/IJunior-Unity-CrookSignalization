using System;
using UnityEngine;

public class RaycastHandler : MonoBehaviour 
{
    public event Action<Barrier> BarrierHitted;

    public void HandleRay(RaycastHit hit)
    {
        if (hit.collider == null)
            return;

        GameObject hittedObject = hit.collider.gameObject;

        if (hittedObject.TryGetComponent(out Barrier barrier)) 
            BarrierHitted?.Invoke(barrier);
    }
}
