using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField, Range(0, 1)] private float _moveSpeed = 0.3f;

    private void OnEnable()
    {
        _inputHandler.MovementKeyClicked += Move;
    }

    private void OnDisable()
    {
        _inputHandler.MovementKeyClicked -= Move;
    }

    private void Move(Vector3 offset)
    {
        transform.Translate(offset * _moveSpeed);
    }
}
