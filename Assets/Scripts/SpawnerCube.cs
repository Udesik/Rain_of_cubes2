using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCube : Spawner<Cube>
{
    [SerializeField] private SpawnerBomb _spawnerBomb;
    [SerializeField] private float _spawnDelay = 0.5f;
    [SerializeField] private float _spawnRangeX = 10f;
    [SerializeField] private int _spawnHigh = 22;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        var wait = new WaitForSeconds(_spawnDelay);

        while (true)
        {
            Pool.Get();
            yield return wait;
        }
    }

    protected override void OnGetFromPool(Cube cube)
    {
        float x = Random.Range(-_spawnRangeX, _spawnRangeX);
        float z = Random.Range(-_spawnRangeX, _spawnRangeX);
        cube.transform.position = new Vector3(x, _spawnHigh, z);

        cube.Initialize();
        base.OnGetFromPool(cube);
        cube.Died += ReleaseCube;
    }

    protected override void OnReleaseFromPool(Cube cube)
    {
        cube.Died -= ReleaseCube;
        base.OnReleaseFromPool(cube);
    }

    private void ReleaseCube(Cube cube)
    {
        Vector3 position = cube.transform.position;
        Pool.Release(cube);
        
        _spawnerBomb.SpawnBomb(position);
    }
}
