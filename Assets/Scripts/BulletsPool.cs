using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class BulletsPool : MonoBehaviour
{
    private IObjectPool<RevisedProjectile> bulletsPool;
    [SerializeField]
    private RevisedProjectile bulletPrefab;
    [SerializeField]
    private bool collectionCheck = true;
    // extra options to control the pool capacity and maximum size
    [SerializeField]
    private int defaultPoolCapacity = 20;
    [SerializeField]
    private int maxPoolSize = 100;

    private void Awake()
    {
        bulletsPool = new ObjectPool<RevisedProjectile>(CreateProjectile,
        OnTakeFromPool, OnReturnedToPool, OnDestroyPooledObject,
        collectionCheck, defaultPoolCapacity, maxPoolSize);
    }
    private void OnDisable()
    {
        //bulletsPool.Clear();
    }

    // invoked when creating an item to populate the object pool
    private RevisedProjectile CreateProjectile()
    {
        RevisedProjectile projectileInstance = Instantiate(bulletPrefab);
        projectileInstance.ObjectPool = bulletsPool;
        return projectileInstance;        
    }

    // invoked when retrieving the next item from the object pool
    private void OnTakeFromPool(RevisedProjectile pooledObject)
    {

    }

    // invoked when returning an item to the object pool
    private void OnReturnedToPool(RevisedProjectile pooledObject)
    {
        pooledObject.ResetProjectile();
    }

    // invoked when we exceed the maximum number of pooled items (i.e.
    //destroy the pooled object)
    private void OnDestroyPooledObject(RevisedProjectile pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    public RevisedProjectile SpawnFromPool(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        RevisedProjectile projectile = bulletsPool.Get();
        projectile.SetProjectile(spawnPosition, spawnRotation);

        StartCoroutine(ReturnProjectileToPool(projectile, 4f));
        return projectile;
    }
    private IEnumerator ReturnProjectileToPool(RevisedProjectile projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        bulletsPool.Release(projectile);
    }
}
