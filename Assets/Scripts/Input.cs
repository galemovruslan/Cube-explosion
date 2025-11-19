using System;
using UnityEngine;

[RequireComponent(typeof(Raycaster))]
public class Input : MonoBehaviour
{
    private const int ActionButtomIndex = 0;

    private Raycaster _raycaster;

    public event Action<Explosive> Selected;

    private void Awake()
    {
        _raycaster = GetComponent<Raycaster>();
    }

    private void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(ActionButtomIndex))
        {
            Vector3 screenPosition = UnityEngine.Input.mousePosition;

            if (_raycaster.Cast(screenPosition, out var hit))
            {
                if (hit.rigidbody.gameObject.TryGetComponent<Explosive>(out var explosive))
                {
                    Selected?.Invoke(explosive);
                }
            }
        }
    }
}

