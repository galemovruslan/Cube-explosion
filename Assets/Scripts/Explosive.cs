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

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
    }

    public void SetScale(float scale)
    {
        transform.localScale = Vector3.one * scale;
    }

    public void RecieveExplosion(float force, Vector3 position, float radius, float upwardModifier)
    {
        _rigidbody.AddExplosionForce(force, position, radius, upwardModifier);
    }

    public void SetChance(float chance)
    {
        _divisionChance = chance;
    }
}
