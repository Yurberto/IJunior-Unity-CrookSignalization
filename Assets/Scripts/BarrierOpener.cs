using UnityEngine;

public class BarrierOpener : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private RaycastHandler _raycastHandler;
    [SerializeField] private Camera _playerCamera;

    private void Awake()
    {
        _raycaster = new Raycaster(_playerCamera);
    }

    private void OnEnable()
    {
        _inputHandler.OpenKeyClicked += _raycaster.CastRay;
        _raycaster.ObjectHitted += _raycastHandler.HandleRay;
        _raycastHandler.BarrierHitted += OpenBarrier;
    }

    private void OnDisable()
    {
        _raycastHandler.BarrierHitted -= OpenBarrier;
        _raycaster.ObjectHitted -= _raycastHandler.HandleRay;
        _inputHandler.OpenKeyClicked -= _raycaster.CastRay;
    }

    private void OpenBarrier(Barrier barrier)
    {
        barrier.Open();
    }
}
