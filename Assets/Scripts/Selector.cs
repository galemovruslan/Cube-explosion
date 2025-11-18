using System;
using UnityEngine;

public class Selector : MonoBehaviour
{
    private const int ActionButtomIndex = 0;

    [SerializeField] LayerMask _selectables;

    private RaycastHit _hit;
    private Camera _camera;

    public event Action<GameObject> Selected;

    private void OnEnable()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(ActionButtomIndex))
        {
            Vector3 screenPosition = Input.mousePosition;
            Ray ray = _camera.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out _hit, 1000, _selectables))
            {
                Selected?.Invoke(_hit.rigidbody.gameObject);
            }
        }
    }
}
