using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const float HueMin = 0f;
    private const float HueMax = 1f;

    [SerializeField] private Explosive _prefab;
    [SerializeField] private Transform _parent;
    [SerializeField] private int _initialSpawnedNumber = 4;
    [SerializeField] private float _initialSpawnRange = 30f;
    [SerializeField] private float _initialSpawnHeight = 5.0f;

    private void Start()
    {
        for (int i = 0; i < _initialSpawnedNumber; i++)
        {
            float xCoord = (Random.value - 0.5f) * 2 * _initialSpawnRange;
            float zCoord = (Random.value - 0.5f) * 2 * _initialSpawnRange;
            Vector3 position = new Vector3(xCoord, _initialSpawnHeight, zCoord);
            Spawn(position);
        }
    }

    public Explosive Spawn(Vector3 position)
    {
        Explosive spawned = Instantiate(_prefab, _parent);
        spawned.transform.position = position;
        return spawned;
    }

    public List<Explosive> SpawnOffsprings(Explosive parent, int offspringsNumber, float nextScaleFactor, float nextChanceFactor)
    {
        Vector3 origin = parent.Position;
        float scale = parent.Scale * nextScaleFactor;
        float chance = parent.DivisionChance * nextChanceFactor;

        List<Explosive> offsprings = new List<Explosive>();

        for (int i = 0; i < offspringsNumber; i++)
        {
            Vector3 position = origin + Random.onUnitSphere * scale;
            var spawned = Spawn(position);

            offsprings.Add(spawned);
            IDivideable newExplosive = spawned.GetComponent<IDivideable>();

            Color color = Random.ColorHSV(HueMin, HueMax);
            newExplosive.Initialize(color, scale, chance);
        }

        return offsprings;
    }

    public void Destroy(Explosive explosive)
    {
        explosive.gameObject.SetActive(false);
        GameObject.Destroy(explosive.gameObject);
    }
}