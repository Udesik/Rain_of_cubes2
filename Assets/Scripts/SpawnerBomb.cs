using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBomb : Spawner<Bomb>
{
    public void SpawnBomb(Vector3 position)
    {
        Bomb bomb = Pool.Get();
        bomb.transform.position = position;
        bomb.Initialize(); 
    }

    protected override void OnGetFromPool(Bomb bomb)
    {
        base.OnGetFromPool(bomb);
        bomb.Died += ReleaseBomb;
    }

    protected override void OnReleaseFromPool(Bomb bomb)
    {
        bomb.Died -= ReleaseBomb;
        base.OnReleaseFromPool(bomb);
    }

    private void ReleaseBomb(Bomb bomb)
    {
        Pool.Release(bomb);
    }
}
