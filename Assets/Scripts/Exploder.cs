using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 700f;
    [SerializeField] private int _maxCollidersCount = 20;

    private Collider[] _hitColliders;

    private void Awake()
    {
        _hitColliders = new Collider[_maxCollidersCount];
    }

    public void Explode(Vector3 position)
    {
        int numColliders = Physics.OverlapSphereNonAlloc(position, _explosionRadius, _hitColliders);

        for (int i = 0; i < numColliders; i++)
        {
            if (_hitColliders[i].TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.AddExplosionForce(_explosionForce, position, _explosionRadius);
            }
        }
    }
}
