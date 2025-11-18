using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
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

    public GameObject Spawn(Vector3 position)
    {
        GameObject spawned = Instantiate(_prefab, _parent);
        spawned.transform.position = position;
        return spawned;
    }
}