using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _forceFactor = 10f;
    [SerializeField] private float _radiusFactor = 3f;
    [SerializeField] private float _upwardModifier = 1f;

    private Raycaster _raycaster;

    private void Awake()
    {
        _raycaster = GetComponent<Raycaster>();
    }

    public void ExplodeNear(Explosive source)
    {
        Vector3 center = source.Position;
        float nearRadius = _radiusFactor / source.Scale;

        List<Explosive> affected = _raycaster.GetNear(center, nearRadius);

        if (affected.Count > 0)
        {
            float explodionRadius = _radiusFactor / source.Scale;
            float force = _forceFactor / source.Scale;

            ApplyForce(affected, center, explodionRadius, force);
        }
    }

    public void ExplodeOffsprings(Explosive source, IEnumerable<Explosive> affected)
    {
        Vector3 center = source.Position;
        float radius = source.Scale * _radiusFactor;
        float force = _forceFactor;

        ApplyForce(affected, center, radius, force);
    }

    private void ApplyForce(IEnumerable<Explosive> affected, Vector3 center, float radius, float force)
    {
        foreach (var item in affected)
        {
            item.Rigidbody.AddExplosionForce(force, center, radius, _upwardModifier);
        }
    }
}
