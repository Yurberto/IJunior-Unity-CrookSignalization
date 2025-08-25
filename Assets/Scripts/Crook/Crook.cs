using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Mover), typeof(Collider))]
public class Crook : MonoBehaviour
{
    [SerializeField] private Transform _waypointsParent;

    private Transform[] _waypoints;
    private Mover _mover;
    private int _currentWaypoint;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _waypoints = new Transform[_waypointsParent.childCount];
    }

    private void Start()
    {
        for (int i = 0; i < _waypointsParent.childCount; i++)
            _waypoints[i] = _waypointsParent.GetChild(i);
    }

    private void Update()
    {
        if (transform.position == _waypoints[_currentWaypoint].position)
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;

        _mover.Move(_waypoints[_currentWaypoint].position);
    }
}
