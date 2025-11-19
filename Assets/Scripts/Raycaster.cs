using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private float _range = 1000f;
    [SerializeField] private LayerMask _selectables;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public bool Cast(Vector3 screenPosition, out RaycastHit _hit)
    {
        Ray ray = _camera.ScreenPointToRay(screenPosition);
        return Physics.Raycast(ray, out _hit, _range, _selectables);
    }
}
