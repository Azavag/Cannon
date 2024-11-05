using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RevisedProjectile : MonoBehaviour
{
    private BulletController bulletController;
    private IObjectPool<RevisedProjectile> objectPool;
    // public property to give the pooledProjectile a reference to its ObjectPool
    public IObjectPool<RevisedProjectile> ObjectPool { set => objectPool = value; }

    private void Awake()
    {
        bulletController = GetComponent<BulletController>();
    }

    public void SetProjectile(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
    }

    public void ResetProjectile()
    {
        bulletController.ResetBullet();
    }
   
}
