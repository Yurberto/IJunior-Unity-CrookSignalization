using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AlarmTrigger : MonoBehaviour
{
    public event Action CrookEnteredHouse;
    public event Action CrookLeftHouse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Crook>())
            CrookEnteredHouse?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Crook>())
            CrookLeftHouse?.Invoke();
    }
}
