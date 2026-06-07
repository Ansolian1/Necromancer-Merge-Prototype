using UnityEngine;
using UnityEngine.Pool;

public interface IVfxService
{
    void PlayMergeVfx(Vector3 position);
}

public class UnityVfxService : MonoBehaviour, IVfxService
{
    [SerializeField] private ParticleSystem _mergeVfxPrefab;

    private ObjectPool<ParticleSystem> _pool;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _pool = new ObjectPool<ParticleSystem>(
            createFunc: CreateParticle,
            actionOnGet: OnTakeFromPool,
            actionOnRelease: OnReturnedToPool,
            actionOnDestroy: Destroy,
            defaultCapacity: 10,
            maxSize: 30
        );
    }

    private ParticleSystem CreateParticle()
    {
        ParticleSystem instance = Instantiate(_mergeVfxPrefab, transform);
        return instance;
    }

    private void OnTakeFromPool(ParticleSystem particle)
    {
        particle.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(ParticleSystem particle)
    {
        particle.gameObject.SetActive(false);
    }
    
    public void PlayMergeVfx(Vector3 position)
    {
        ParticleSystem particle = _pool.Get();
        particle.transform.position = position;
        particle.Play();
        StartCoroutine(ReturnToPoolRoutine(particle, particle.main.duration));
    }

    private System.Collections.IEnumerator ReturnToPoolRoutine(ParticleSystem particle, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (particle != null)
        {
            _pool.Release(particle);
        }
    }
}
