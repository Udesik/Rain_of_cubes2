using TMPro;
using UnityEngine;
using System;

[System.Serializable] 
public class SpawnerDisplay<TObject> : MonoBehaviour where TObject : Component
{
    [SerializeField] private Spawner<TObject> _spawner;
    [SerializeField] private TextMeshProUGUI _textAllSpawnedObjects;
    [SerializeField] private TextMeshProUGUI _textObjectsCreated;
    [SerializeField] private TextMeshProUGUI _textObjectsActive;

    private void OnEnable()
    {
        if (_spawner != null)
        {
            _spawner.Changed += UpdateUI;
        }
    }

    private void OnDisable()
    {
        if (_spawner != null)
        {
            _spawner.Changed -= UpdateUI;
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_spawner == null) return;

        if (_textAllSpawnedObjects != null) 
            _textAllSpawnedObjects.text = "Заспавнено за всё время: " + _spawner.AllSpawnedCount;
            
        if (_textObjectsCreated != null) 
            _textObjectsCreated.text = "Созданных объектов: " + _spawner.CreatedCount;
            
        if (_textObjectsActive != null) 
            _textObjectsActive.text = "Активных объектов: " + _spawner.ActiveCount;
    }
}