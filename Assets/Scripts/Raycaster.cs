using System;
using UnityEngine;

[RequireComponent(typeof(Input))]
public class Raycaster : MonoBehaviour
{
    [SerializeField] private float _range = 1000f;
    [SerializeField] private LayerMask _selectables;

    private Camera _camera;
    private Input _input;
    private RaycastHit _hit;

    public event Action<Explosive> Selected;

    private void Awake()
    {
        _camera = Camera.main;
        _input = GetComponent<Input>(); 
    }

    private void OnEnable()
    {
        _input.Click += Cast;
    }

    private void OnDisable()
    {
        _input.Click -= Cast;
    }

    public void Cast(Vector3 screenPosition)
    {
        Ray ray = _camera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out _hit, _range, _selectables))
        {
            if (_hit.rigidbody.gameObject.TryGetComponent<Explosive>(out var explosive))
            {
                Selected?.Invoke(explosive);
            }
        }
    }
}
