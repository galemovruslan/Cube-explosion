using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner), typeof(Raycaster), typeof(Exploder))]
public class Divider : MonoBehaviour
{
    private const float NextScaleFactor = 0.5f;
    private const float NextChanceFactor = 0.5f;

    [SerializeField] private int _offspringsMinNumber = 2;
    [SerializeField] private int _offspringsMaxNumber = 6;

    private Spawner _spawner;
    private Exploder _exploder;
    private Raycaster _raycaster;

    private void Awake()
    {
        _raycaster = GetComponent<Raycaster>();
        _spawner = GetComponent<Spawner>();
        _exploder = GetComponent<Exploder>();
    }

    private void OnEnable()
    {
        _raycaster.Selected += OnSelect;
    }

    private void OnDisable()
    {
        _raycaster.Selected -= OnSelect;
    }

    private void OnSelect(Explosive selected)
    {
        if (selected.TryGetComponent<IDivideable>(out var divideable) && ShouldDivide(divideable))
        {
            Vector3 position = selected.transform.position;
            float scale = selected.transform.localScale.x;

            int offspringsNumber = Random.Range(_offspringsMinNumber, _offspringsMaxNumber + 1);
            List<Explosive> offsprings = _spawner.SpawnOffsprings(selected, offspringsNumber, NextScaleFactor, NextChanceFactor);
            _exploder.Explode(selected, offsprings);
        }

        _spawner.Destroy(selected);
    }

    private bool ShouldDivide(IDivideable divideable)
    {
        return Random.Range(0f, 1f) <= divideable.DivisionChance;
    }
}
