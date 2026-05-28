using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer), typeof(Rigidbody), typeof(Exploder))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 700f;

    private Exploder _exploder;
    private Renderer _renderer;
    private Color _initialColor;

    private float _minTimeToDie = 2f;
    private float _maxTimeToDie = 5f;

    public event Action<Bomb> Died;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _exploder = GetComponent<Exploder>();
        _initialColor = _renderer.material.color;
    }

    public void Initialize()
    {
        _renderer.material.color = _initialColor;
        float lifetime = Random.Range(_minTimeToDie, _maxTimeToDie);
        StartCoroutine(FadeRoutine(lifetime));
    }

    private IEnumerator FadeRoutine(float duration)
    {
        float elapsedTime = 0f;
    
        Color startColor = _renderer.material.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            Color newColor = new Color(startColor.r, startColor.g, startColor.b, alpha);
            _renderer.material.color = newColor;

            if (_renderer.material.HasProperty("_BaseColor"))
            {
                _renderer.material.SetColor("_BaseColor", newColor);
            }

            yield return null;
        }

        _exploder.Explode(transform.position);
        Died?.Invoke(this);
    }
}
