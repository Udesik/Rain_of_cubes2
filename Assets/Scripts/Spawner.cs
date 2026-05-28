using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<TObject> : MonoBehaviour where TObject : Component
{
    protected ObjectPool<TObject> Pool;
    
    [SerializeField] private TObject _tObjectPrefab;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 100;

    private int _allSpawnedCount = 0;

    public event Action Changed;

    public int AllSpawnedCount => _allSpawnedCount;
    public int CreatedCount => Pool.CountAll;
    public int ActiveCount => Pool.CountActive;

    private void Awake()
    {
        Pool = new ObjectPool<TObject>(
            createFunc: () => CreateObject(_tObjectPrefab),
            actionOnGet: (obj) => OnGetFromPool(obj),
            actionOnRelease: (obj) => OnReleaseFromPool(obj),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: false,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );
    }

    protected virtual void OnGetFromPool(TObject obj)
    {
        obj.gameObject.SetActive(true);
        _allSpawnedCount++;
        Changed?.Invoke();
    }

    protected virtual void OnReleaseFromPool(TObject obj)
    {
        obj.gameObject.SetActive(false);
        Changed?.Invoke();
    }

    protected TObject CreateObject(TObject tObjectPrefab)
    {
        TObject instance = Instantiate(tObjectPrefab);
        return instance;
    }
}
