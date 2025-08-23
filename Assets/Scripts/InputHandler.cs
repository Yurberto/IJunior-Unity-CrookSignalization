using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    public event Action<Vector3> MovementKeyClicked;
    public event Action OpenKeyClicked;

    private void Update()
    {
        Vector3 inputDirection = new Vector3(Input.GetAxis(Horizontal), 0.0f, Input.GetAxis(Vertical));

        if (inputDirection != Vector3.zero) 
            MovementKeyClicked?.Invoke(inputDirection);

        if (Input.GetKeyDown(KeyCode.Mouse0))
            OpenKeyClicked?.Invoke();
    }
}
