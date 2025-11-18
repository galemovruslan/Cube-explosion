using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Divider))]
public class Exploder : MonoBehaviour
{
    [SerializeField] private float _forceFactor = 10f;
    [SerializeField] private float _radiusFactor = 3f;
    [SerializeField] private float _upwardModifier = 1f;

    private Divider _divider;

    private void Awake()
    {
        _divider = GetComponent<Divider>();
    }

    private void OnEnable()
    {
        _divider.Divided += OnDivided;
    }

    private void OnDisable()
    {
        _divider.Divided -= OnDivided;
    }

    public void OnDivided(GameObject parent, IEnumerable<GameObject> offsprings)
    {
        if (parent.TryGetComponent<Explosive>(out var parentExplosive) == false)
        {
            return;
        }

        List<Explosive> explosives = new List<Explosive>();

        foreach (var offspring in offsprings)
        {
            if (offspring.TryGetComponent<Explosive>(out var explosive))
            {
                explosives.Add(explosive);
            }
        }

        Explode(parentExplosive, explosives);
    }

    public void Explode(Explosive source, IEnumerable<Explosive> affected)
    {
        Vector3 center = source.Position;
        float radius = source.Scale * _radiusFactor;
        float force = _forceFactor;

        foreach (var item in affected)
        {
            item.RecieveExplosion(force, center, radius, _upwardModifier);
        }
    }
}
