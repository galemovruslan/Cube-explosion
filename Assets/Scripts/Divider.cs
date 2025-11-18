using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner), typeof(Selector))]
public class Divider : MonoBehaviour
{
    private const float HueMin = 0f;
    private const float HueMax = 1f;
    private const float NextScaleFactor = 0.5f;
    private const float NextChanceFactor = 0.5f;

    [SerializeField] private int _offspringsMinNumber = 2;
    [SerializeField] private int _offspringsMaxNumber = 6;

    private Selector _selector;
    private Spawner _spawner;

    public event Action<GameObject, IEnumerable<GameObject>> Divided;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
        _selector = GetComponent<Selector>();
    }

    private void OnEnable()
    {
        _selector.Selected += OnSelect;
    }

    private void OnDisable()
    {
        _selector.Selected -= OnSelect;
    }

    private void OnSelect(GameObject selected)
    {
        if (selected.TryGetComponent<IDivideable>(out var divideable) && ShouldDivide(divideable))
        {
            Vector3 position = selected.transform.position;
            float scale = selected.transform.localScale.x;

            List<GameObject> offsprings = SpawnOffsprings(position, scale * NextScaleFactor, divideable.DivisionChance * NextChanceFactor);
            Divided?.Invoke(selected, offsprings);
        }

        selected.SetActive(false);
        GameObject.Destroy(selected);
    }

    private bool ShouldDivide(IDivideable divideable)
    {
        return UnityEngine.Random.Range(0f, 1f) <= divideable.DivisionChance;
    }

    private List<GameObject> SpawnOffsprings(Vector3 origin, float scale, float chance)
    {
        int offspringsNumber = UnityEngine.Random.Range(_offspringsMinNumber, _offspringsMaxNumber + 1);
        List<GameObject> offsprings = new List<GameObject>();

        for (int i = 0; i < offspringsNumber; i++)
        {
            Vector3 position = origin + UnityEngine.Random.onUnitSphere * scale;
            var spawned = _spawner.Spawn(position);

            offsprings.Add(spawned);
            IDivideable newExplosive = spawned.GetComponent<IDivideable>();

            Color color = UnityEngine.Random.ColorHSV(HueMin, HueMax);
            newExplosive.SetScale(scale);
            newExplosive.SetColor(color);
            newExplosive.SetChance(chance);
        }

        return offsprings;
    }
}
