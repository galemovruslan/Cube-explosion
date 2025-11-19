using System;
using UnityEngine;

public class Input : MonoBehaviour
{
    private const int ActionButtomIndex = 0;

    public event Action <Vector3> Click;

    private void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(ActionButtomIndex))
        {
            Vector3 screenPosition = UnityEngine.Input.mousePosition;
            Click?.Invoke(screenPosition);
        }
    }
}

