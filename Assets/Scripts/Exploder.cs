using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _forceFactor = 10f;
    [SerializeField] private float _radiusFactor = 3f;
    [SerializeField] private float _upwardModifier = 1f;

    public void Explode(Explosive source, IEnumerable<Explosive> affected)
    {
        Vector3 center = source.Position;
        float radius = source.Scale * _radiusFactor;
        float force = _forceFactor;

        foreach (var item in affected)
        {
            item.Rigidbody.AddExplosionForce(force, center, radius, _upwardModifier);
        }
    }
}
