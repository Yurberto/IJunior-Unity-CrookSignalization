using System;
using UnityEngine;

public class Raycaster
{
    private Camera _camera;

    private RaycastHit _hit;
    private Ray _ray;

    private float _maxDistance;

    public event Action<RaycastHit> ObjectHitted;

    public Raycaster(Camera camera, float maxDistance = 10000)
    {
        _camera = camera;
        _maxDistance = maxDistance;
    }

    public void CastRay()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, _maxDistance))
            ObjectHitted?.Invoke(_hit);
    }
}
