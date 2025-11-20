using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Explosive : MonoBehaviour, IDivideable
{
    [SerializeField] private float _divisionChance = 1f;

    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;

    public float Scale => transform.localScale.x;
    public Vector3 Position => transform.position;
    public float DivisionChance { get => _divisionChance; }
    public Rigidbody Rigidbody { get => _rigidbody; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Initialize(Color color, float scale, float chance)
    {
        _meshRenderer.material.color = color;
        transform.localScale = Vector3.one * scale;
        _divisionChance = chance;
        _rigidbody.mass = scale;
    }
}
